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
        travels = db.travels.find()
        return jsonify([travel for travel in travels])
    except Exception as e:
        return str(e)


@app.route('/travels', methods=['POST'])
def add_travel():
    try:
        info = request.get_json()
        travels_collection = db['Travels']
        places_collection = db['Places']
        routes_collection = db['Routes']

        travel_info = {
            'id': info['id'],
            'title': info['title'],
            'description': info['description'],
            'type': info['type'],
            'updated_at': info['updated_at'],
            'created_at': info['created_at']
        }

        travels_collection.insert_one(travel_info)

        places_info = {
            'id': info['places']['id'],
            'travel_id': info['places']['travel_id'],
            'place_id': info['places']['place_id'],
            'updated_at': info['places']['updated_at'],
            'created_at': info['places']['created_at']
        }

        places_collection.insert_one(places_info)

        routes_info = {
            'id': info['places']['routes']['id'],
            'travel_place_id': info['places']['routes']['travel_place_id'],
            'route_id': info['places']['routes']['route_id'],
            'updated_at': info['places']['routes']['updated_at'],
            'created_at': info['places']['routes']['created_at']
        }

        routes_collection.insert_one(routes_info)

        return jsonify({'message': 'Travel added successfully'})

    except Exception as e:
        return str(e)


@app.route('/travel/<int:travel_id>', methods=['GET'])
def get_travel(travel_id):
    try:
        travel = db.travels.find_one({'id': travel_id})
        return jsonify(travel)
    except Exception as e:
        return str(e)


if __name__ == '__main__':
    app.run(host='127.0.0.1', port=3004, debug=True, threaded=False)
