import datetime
from bson import ObjectId
from flask import request, jsonify, Flask
from flask_cors import CORS

from pymongo.mongo_client import MongoClient

uri = "mongodb+srv://worldpath:uoSwHCbCc86dcHX3@cluster0.dumeyxp.mongodb.net/?retryWrites=true&w=majority"
client = MongoClient(uri)
db = client['Travels']

try:
    client.admin.command('ping')
    print("Pinged your deployment. You successfully connected to MongoDB!")
except Exception as e:
    print(e)

app = Flask(__name__)

CORS(allow_headers='Content-Type')
CORS(app, resources={r"/*": {"origins": "*"}})


@app.route('/travels', methods=['GET'])
def get_travels():
    travels_collection = db['Travels']
    places_collection = db['Places']
    routes_collection = db['Routes']

    travels = []

    for travel in travels_collection.find():
        places = []
        routes = []

        for place in places_collection.find({"travel_id": travel['id']}):
            places.append({
                "place_id": place['place_id']
            })

        for route in routes_collection.find({"travel_id": travel['id']}):
            routes.append({
                "route_id": route['route_id']
            })

        travels.append({
            "id": travel['id'],
            "title": travel['title'],
            "description": travel['description'],
            "type": travel['type'],
            "places": places,
            "routes": routes,
            "updated_at": travel['updated_at'],
            "created_at": travel['created_at']
        })

    return jsonify(travels)


@app.route('/travel', methods=['POST'])
def add_travel():
    travels_collection = db['Travels']
    places_collection = db['Places']
    routes_collection = db['Routes']

    data = request.get_json()

    places = data['places']
    routes = data['routes']

    travel_info = {
        "title": data['title'],
        "description": data['description'],
        "type": data['type'],
        "user_id": ObjectId(request.headers.get('Userid')),
        "updated_at": datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S"),
        "created_at": datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    }

    if travel_info['title'] is None or travel_info['description'] is None or travel_info['type'] is None:
        return jsonify({"message": "Title, description and type are required"}), 400

    if travels_collection.find_one({"title": travel_info['title']}) is not None:
        return jsonify({"message": "Title already exists"}), 400

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


@app.route('/travel/<travel_id>', methods=['PUT'])
def edit_travel(travel_id):
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

    if travel['user_id'] != ObjectId(request.headers.get('Userid')) and travel['type'] != 'shared':
        return jsonify({"message": "You don't have permission to edit this travel"}), 403

    db['Travels'].update_one({"_id": ObjectId(travel_id)}, {"$set": travel_info})

    return jsonify({"message": "Travel updated successfully"})


if __name__ == '__main__':
    app.run(host='127.0.0.1', port=3004, debug=True, threaded=False)
