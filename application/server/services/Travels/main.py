import datetime
import time

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


@app.route('/travels', methods=['POST'])
def add_travel():
    travels_collection = db['Travels']
    places_collection = db['Places']
    routes_collection = db['Routes']

    data = request.get_json()

    places = data['places']
    routes = data['routes']

    travel_id = travels_collection.count_documents({}) + 1

    travel_info = {
        "id": travel_id,
        "title": data['title'],
        "description": data['description'],
        "type": data['type'],
        "user_id": request.headers.get('Userid'),
        "updated_at": datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S"),
        "created_at": datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    }

    if travel_info['title'] is None or travel_info['description'] is None or travel_info['type'] is None:
        return jsonify({"message": "Title, description and type are required"}), 400

    if travels_collection.find_one({"title": travel_info['title']}) is not None:
        return jsonify({"message": "Title already exists"}), 400

    travels_collection.insert_one(travel_info)

    for place in places:
        place['id'] = places_collection.count_documents({}) + 1
        place['place_id'] = place['place_id']
        place['travel_id'] = travel_id
        place['updated_at'] = datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        place['created_at'] = datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        places_collection.insert_one(place)

    for route in routes:
        route['id'] = routes_collection.count_documents({}) + 1
        route['route_id'] = route['route_id']
        route['travel_id'] = travel_id
        route['updated_at'] = datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        route['created_at'] = datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        routes_collection.insert_one(route)

    return jsonify({"message": "Travel added successfully"})


if __name__ == '__main__':
    app.run(host='127.0.0.1', port=3004, debug=True, threaded=False)
