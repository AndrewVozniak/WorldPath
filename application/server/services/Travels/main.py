import datetime

from bson import ObjectId
from flask import request, jsonify, Flask
from flask_cors import CORS
from database import db

app = Flask(__name__)

CORS(allow_headers='Content-Type')
CORS(app, resources={r"/*": {"origins": "*"}})


@app.route('/travel_service/travels/', methods=['GET'])
async def get_travels():
    travels_collection = db['Travels']
    places_collection = db['Places']
    routes_collection = db['Routes']

    travels = []

    for travel in travels_collection.find({}):
        places = []
        routes = []

        for place in places_collection.find({"travel_id": travel['_id']}):
            places.append({
                "place_id": str(place['place_id'])
            })

        for route in routes_collection.find({"travel_id": travel['_id']}):
            routes.append({
                "route_id": str(route['route_id'])
            })

        travels.append({
            "id": str(travel['_id']),
            "title": travel['title'],
            "description": travel['description'],
            "type": travel['type'],
            "places": places,
            "routes": routes,
            "updated_at": travel['updated_at'],
            "created_at": travel['created_at']
        })

    return jsonify(travels)


@app.route('/travel_service/user/travels/', methods=['GET'])
async def get_travels_by_user_id():
    user_id = request.headers.get('Userid')

    if user_id is None:
        return jsonify({"message": "User id is required"}), 400

    travels_collection = db['Travels']
    places_collection = db['Places']
    routes_collection = db['Routes']

    travels = []

    for travel in travels_collection.find({"user_id": ObjectId(user_id)}):
        places = []
        routes = []

        for place in places_collection.find({"travel_id": travel['_id']}):
            places.append({
                "place_id": str(place['place_id'])
            })

        for route in routes_collection.find({"travel_id": travel['_id']}):
            routes.append({
                "route_id": str(route['route_id'])
            })

        travels.append({
            "id": str(travel['_id']),
            "title": travel['title'],
            "description": travel['description'],
            "type": travel['type'],
            "places": places,
            "routes": routes,
            "updated_at": travel['updated_at'],
            "created_at": travel['created_at']
        })

    return jsonify(travels)


@app.route('/travel_service/travel', methods=['POST'])
async def add_travel():
    travels_collection = db['Travels']
    places_collection = db['Places']
    routes_collection = db['Routes']

    data = request.get_json()

    places = data['places'] if 'places' in data else []
    routes = data['routes'] if 'routes' in data else []

    user_id = request.headers.get('Userid')

    # Basic validations
    if data['title'] is None or data['description'] is None or data['type'] is None:
        return jsonify({"message": "Title, description and type are required"}), 400

    if travels_collection.find_one({"title": data['title']}) is not None:
        return jsonify({"message": "Title already exists"}), 400

    if user_id is None:
        return jsonify({"message": "User id is required"}), 400

    if data['type'] != 'public' and data['type'] != 'private':
        return jsonify({"message": "Type must be public or private"}), 400

    # Prepare data to insert
    travel_info = {
        "title": data['title'],
        "description": data['description'],
        "type": data['type'],
        "user_id": ObjectId(user_id),
        "updated_at": datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S"),
        "created_at": datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    }

    travel_id = travels_collection.insert_one(travel_info).inserted_id

    for place in places:
        place['place_id'] = place['place_id']
        place['travel_id'] = travel_id
        place['updated_at'] = datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        place['created_at'] = datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        places_collection.insert_one(place)

    for route in routes:
        route['route_id'] = route['route_id']
        route['travel_id'] = travel_id
        route['updated_at'] = datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        route['created_at'] = datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        routes_collection.insert_one(route)

    return jsonify({"message": "Travel added successfully"})


@app.route('/travel_service/travel/<travel_id>', methods=['PUT'])
async def edit_travel(travel_id):
    travel = db['Travels'].find_one({"_id": ObjectId(travel_id)})

    if travel is None:
        return jsonify({"message": "Travel not found"}), 404

    travel_info = {
        "title": request.json.get('title', travel['title']),
        "description": request.json.get('description', travel['description']),
        "type": request.json.get('type', travel['type']),
        "updated_at": datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S"),
    }

    founded_travel = db['Travels'].find_one({"title": travel_info['title']})

    if founded_travel is not None and founded_travel['_id'] != ObjectId(travel_id):
        return jsonify({"message": "Title already exists"}), 400

    if travel['user_id'] != ObjectId(request.headers.get('Userid')):
        return jsonify({"message": "You don't have permission to edit this travel"}), 403

    db['Travels'].update_one({"_id": ObjectId(travel_id)}, {"$set": travel_info})

    return jsonify({"message": "Travel updated successfully"})


@app.route('/travel_service/travel/<travel_id>', methods=['DELETE'])
async def delete_travel(travel_id):
    travel = db['Travels'].find_one({"_id": ObjectId(travel_id)})

    if travel is None:
        return jsonify({"message": "Travel not found"}), 404

    if travel['user_id'] != ObjectId(request.headers.get('Userid')):
        return jsonify({"message": "You don't have permission to delete this travel"}), 403

    db['Travels'].delete_one({"_id": ObjectId(travel_id)})
    db['Places'].delete_many({"travel_id": ObjectId(travel_id)})
    db['Routes'].delete_many({"travel_id": ObjectId(travel_id)})

    return jsonify({"message": "Travel deleted successfully"})


@app.route('/travel_service/travel/<travel_id>/comments', methods=['POST'])
async def add_comment(travel_id):
    data = request.get_json()

    user_id = request.headers.get('Userid')

    # Get text from data safely
    text = data.get('text')

    # Basic validations
    if not text:
        return jsonify({"message": "Text is required"}), 400

    if user_id is None:
        return jsonify({"message": "User id is required"}), 400

    # Get travel_id from data safely
    if db['Travels'].find_one({"_id": ObjectId(travel_id)}) is None:
        return jsonify({"message": "Travel not found"}), 404

    # Prepare data to insert
    comment_info = {
        "user_id": ObjectId(user_id),
        "travel_id": ObjectId(travel_id),
        "text": text,
        "updated_at": datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S"),
        "created_at": datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    }

    db['Comments'].insert_one(comment_info)

    return jsonify({"message": "Comment added successfully"})


@app.route('/travel_service/like', methods=['POST'])
async def add_like():
    likes_collection = db['Likes']

    data = request.get_json()

    user_id = request.headers.get('Userid')

    # Get travel_id from data safely
    travel_id = data.get('travel_id')

    # Basic validations
    if not travel_id:
        return jsonify({"message": "Travel id is required"}), 400

    if user_id is None:
        return jsonify({"message": "User id is required"}), 400

    if likes_collection.find_one({"user_id": ObjectId(user_id), "travel_id": ObjectId(travel_id)}) is not None:
        return jsonify({"message": "You already liked this travel"}), 400

    # Prepare data to insert
    like_info = {
        "user_id": ObjectId(user_id),
        "travel_id": ObjectId(travel_id),
        "updated_at": datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S"),
        "created_at": datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    }

    likes_collection.insert_one(like_info)

    return jsonify({"message": "Like added successfully"})


@app.route('/travel_service/like/<like_id>', methods=['DELETE'])
async def delete_like(like_id):
    likes_collection = db['Likes']

    user_id = request.headers.get('Userid')

    like = likes_collection.find_one({"_id": ObjectId(like_id)})

    if like is None:
        return jsonify({"message": "Like not found"}), 404

    if like['user_id'] != ObjectId(user_id):
        return jsonify({"message": "You don't have permission to delete this like"}), 403

    likes_collection.delete_one({"_id": ObjectId(like_id)})

    return jsonify({"message": "Like deleted successfully"})


@app.route('/travel_service/travels/liked', methods=['GET'])
async def get_liked_travels():
    likes_collection = db['Likes']
    travels_collection = db['Travels']
    places_collection = db['Places']
    routes_collection = db['Routes']

    user_id = request.headers.get('Userid')

    if user_id is None:
        return jsonify({"message": "User id is required"}), 400

    likes = []

    for like in likes_collection.find({"user_id": ObjectId(user_id)}):
        travel = travels_collection.find_one({"_id": like['travel_id']})

        places = []
        routes = []

        for place in places_collection.find({"travel_id": travel['_id']}):
            places.append({
                "place_id": str(place['place_id'])
            })

        for route in routes_collection.find({"travel_id": travel['_id']}):
            routes.append({
                "route_id": str(route['route_id'])
            })

        likes.append({
            "id": str(travel['_id']),
            "title": travel['title'],
            "description": travel['description'],
            "type": travel['type'],
            "places": places,
            "routes": routes,
            "updated_at": travel['updated_at'],
            "created_at": travel['created_at']
        })

    return jsonify(likes)


@app.route('/travel_service/travel/<travel_id>', methods=['GET'])
async def get_travel(travel_id):
    travels_collection = db['Travels']
    places_collection = db['Places']
    routes_collection = db['Routes']

    travel = travels_collection.find_one({"_id": ObjectId(travel_id)})

    if travel is None:
        return jsonify({"message": "Travel not found"}), 404

    places = []
    routes = []

    for place in places_collection.find({"travel_id": travel['_id']}, {"place_id": 1}):
        places.append({
            "place_id": str(place['place_id'])
        })

    for route in routes_collection.find({"travel_id": travel['_id']}, {"route_id": 1}):
        routes.append({
            "route_id": str(route['route_id'])
        })

    return jsonify({
        "id": str(travel['_id']),
        "title": travel['title'],
        "description": travel['description'],
        "type": travel['type'],
        "places": places,
        "routes": routes,
        "updated_at": travel['updated_at'],
        "created_at": travel['created_at']
    })


@app.route('/travel_service/travels/<travel_id>/photos', methods=['POST'])
async def add_photo(travel_id):
    collection = db['Photos']

    data = request.get_json()
    path = data.get('path')

    if not path:
        return jsonify({"message": "Path is required"}), 400

    photo_info = {
        "travel_id": ObjectId(travel_id),
        "path": path,
        "updated_at": datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S"),
        "created_at": datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    }

    collection.insert_one(photo_info)

    return jsonify({"message": "Photo added successfully"})


@app.route('/travel_service/travels/<travel_id>/photos', methods=['GET'])
async def get_photos(travel_id):
    collection = db['Photos']

    photos = []

    for photo in collection.find({"travel_id": ObjectId(travel_id)}):
        photos.append({
            "id": str(photo['_id']),
            "travel_id": str(photo['travel_id']),
            "path": photo['path'],
            "updated_at": photo['updated_at'],
            "created_at": photo['created_at']
        })

    return jsonify(photos)


@app.route('/travel_service/travels/photos/<photo_id>', methods=['DELETE'])
async def delete_photo(photo_id):
    collection = db['Photos']

    photo = collection.find_one({"_id": ObjectId(photo_id)})

    if photo is None:
        return jsonify({"message": "Photo not found"}), 404

    collection.delete_one({"_id": ObjectId(photo_id)})

    return jsonify({"message": "Photo deleted successfully"})


if __name__ == '__main__':
    app.run(host='0.0.0.0', port=3004, debug=True, threaded=False)
