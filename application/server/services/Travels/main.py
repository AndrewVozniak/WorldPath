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
    try:
        travels_cursor = db['Travels'].find({})
        travels = []
        routes = []
        places = []

        for travel in travels_cursor:
            travel.pop('_id')
            travels.append(travel)

            travel_id = travel['id']
            place_cursor = db['Places'].find({'travel_id': travel_id})

            for place in place_cursor:
                place.pop('_id')
                places.append(place)

                place_id = place['place_id']
                route_cursor = db['Routes'].find({'travel_place_id': place_id})

                for route in route_cursor:
                    route.pop('_id')
                    routes.append(route)

        if len(places) != 0:
            travels.append({'places': places})

        if len(routes) != 0:
            travels.append({'routes': routes})

        return jsonify(travels)

    except Exception as e:
        return str(e)


# {
#     "title": "Travel 1",
#     "description": "Some description",
#     "type": "private",
#     "updated_at": "01.09.2023",
#     "created_at": "01.09.2023",
#
#     "places": {
#         "travel_id": 1,
#         "place_id": 1,
#         "updated_at": "01.09.2023",
#         "created_at": "01.09.2023"
#     },
#
#     "routes": {
#         "travel_id": 1,
#         "route_id": 3,
#         "updated_at": "01.09.2023",
#         "created_at": "01.09.2023"
#     }
# }

@app.route('/travels', methods=['POST'])
def add_travel():
    try:
        info = request.get_json()

        travels_collection = db['Travels']
        places_collection = db['Places']
        routes_collection = db['Routes']

        travel_info = {
            'id': travels_collection.count_documents({}) + 1,
            'title': info['title'],
            'description': info['description'],
            'type': info['type'],
            'updated_at': info['updated_at'],
            'created_at': info['created_at']
        }

        places_info = {
            'id': places_collection.count_documents({}) + 1,
            'travel_id': travel_info['id'],
            'place_id': info['places']['place_id'],
            'updated_at': info['updated_at'],
            'created_at': info['created_at']
        }

        routes_info = {
            'id': routes_collection.count_documents({}) + 1,
            'travel_id': travel_info['id'],
            'route_id': info['routes']['route_id'],
            'updated_at': info['updated_at'],
            'created_at': info['created_at']
        }

        if travels_collection.find_one({"title": travel_info['title']}):
            return jsonify({'error': "Travel with this title already exists"})

        if travels_collection.find_one({"id": travel_info['id']}):
            return jsonify({'error': "Travel with this id already exists"})

        if places_collection.find_one({"id": places_info['id']}):
            return jsonify({'error': "Place with this id already exists"})

        if routes_collection.find_one({"id": routes_info['id']}):
            return jsonify({'error': "Route with this id already exists"})

        routes_collection.insert_one(routes_info)
        travels_collection.insert_one(travel_info)
        places_collection.insert_one(places_info)

        return jsonify({'message': 'Travel added successfully'})

    except Exception as e:
        print(e)
        return jsonify({'error': str(e)})


@app.route('/travel/<int:travel_id>', methods=['GET'])
def get_travel(travel_id):
    try:
        travel_collection = db['Travels']

        travel = travel_collection.find_one({'id': travel_id})
        return jsonify(travel)
    except Exception as e:
        return str(e)


if __name__ == '__main__':
    app.run(host='127.0.0.1', port=3004, debug=True, threaded=False)
