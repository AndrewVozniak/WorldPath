import datetime
import json

from bson import ObjectId
from flask import jsonify, Flask, request
from flask_cors import CORS
from database import db

import rpcClient

app = Flask(__name__)

CORS(allow_headers='Content-Type')
CORS(app, resources={r"/*": {"origins": "*"}})

try:
    user_rpc_client = rpcClient.RpcClient('rabbitmq-user')
except Exception as e:
    print(f"Error connecting to rabbitmq-user: {e}")
    user_rpc_client = None


@app.route('/reviews/', methods=['GET'])
async def get_all_reviews():
    collection = db['Reviews']

    reviews_cursor = collection.find()

    reviews = []

    for review in reviews_cursor:
        review['id'] = str(review['_id'])
        review['user_id'] = str(review['user_id'])
        review.pop('_id')
        reviews.append(review)

    return jsonify(reviews)


@app.route('/reviews/<count>/', methods=['GET'])
async def get_latest_reviews(count):
    collection = db['Reviews']
    reviews_cursor = collection.find().sort("created_at", -1).limit(int(count))

    reviews = []

    for review in reviews_cursor:
        review['id'] = str(review['_id'])
        review['user_id'] = str(review['user_id'])
        review.pop('_id')

        reviews.append(review)

    # get all users ids
    users_ids = []

    for review in reviews:
        users_ids.append(review['user_id'])

    # get all users from user service
    users = user_rpc_client.call(json.dumps(users_ids), queue='get_users_base_info')

    users = json.loads(users.decode())

    for review in reviews:
        for user in users:
            if review['user_id'] == user['id']:
                review['user'] = user

    return jsonify(reviews)


@app.route('/reviews/', methods=['POST'])
async def create_review():
    collection = db['Reviews']
    user_id = request.headers.get('Userid')

    data = request.get_json()

    if user_id is None:
        return jsonify({'error': 'user_id is required'}), 400

    if 'rating' not in data:
        return jsonify({'error': 'rating is required'}), 400

    try:
        data['rating'] = float(data['rating'])
    except ValueError:
        return jsonify({'error': 'Rating must be a decimal'}), 400

    if data['rating'] < 1 or data['rating'] > 5:
        return jsonify({'error': 'Rating must be between 1 and 5'}), 400

    if 'text' not in data:
        return jsonify({'error': 'text is required'}), 400

    review = {
        "user_id": ObjectId(user_id),
        "rating": data['rating'],
        "text": data['text'],
        "updated_at": datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S"),
        "created_at": datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    }

    result = collection.insert_one(review)

    return jsonify({'id': str(result.inserted_id)})


@app.route('/reviews/<id>/', methods=['PUT'])
async def update_review(id):
    collection = db['Reviews']

    user_id = request.headers.get('Userid')

    if user_id is None:
        return jsonify({'error': 'No user id provided'}), 400

    user_id = ObjectId(user_id)

    review = collection.find_one({'_id': ObjectId(id)})
    rating = request.json.get('rating')

    if review is None:
        return jsonify({'error': 'Review not found'}), 404

    if review['user_id'] != user_id:
        return jsonify({'error': 'Review not found'}), 404

    if rating is not None:
        try:
            rating = float(rating)
        except ValueError:
            return jsonify({'error': 'Rating must be a decimal'}), 400

        if rating < 1 or rating > 5:
            return jsonify({'error': 'Rating must be between 1 and 5'}), 400

    updated_info = {
        "rating": request.json.get('rating', review['rating']),
        "text": request.json.get('text', review['text']),
        "updated_at": datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    }

    collection.update_one({'_id': ObjectId(id)}, {'$set': updated_info})

    return jsonify({'message': 'Review updated successfully'})


if __name__ == '__main__':
    app.run(host='0.0.0.0', port=3007, debug=True, threaded=True)
