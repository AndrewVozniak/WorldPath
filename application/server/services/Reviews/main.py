import datetime

from bson import ObjectId
from flask import jsonify, Flask, request
from flask_cors import CORS
from database import db

app = Flask(__name__)

CORS(allow_headers='Content-Type')
CORS(app, resources={r"/*": {"origins": "*"}})


@app.route('/reviews', methods=['GET'])
async def get_all_reviews():
    collection = db['Reviews']

    reviews_cursor = collection.find()

    reviews = []

    for review in reviews_cursor:
        review['id'] = str(review['_id'])
        review.pop('_id')
        reviews.append(review)

    return jsonify(reviews)


@app.route('/reviews/<count>', methods=['GET'])
async def get_latest_reviews(count):
    collection = db['Reviews']

    reviews_cursor = collection.find().sort("created_at", -1).limit(int(count))

    reviews = []

    for review in reviews_cursor:
        review['id'] = str(review['_id'])
        review.pop('_id')
        reviews.append(review)

    return jsonify(reviews)


@app.route('/reviews', methods=['POST'])
async def create_review():
    collection = db['Reviews']
    user_id = request.headers.get('Userid')

    data = request.get_json()

    if user_id is None:
        return jsonify({'error': 'user_id is required'}), 400

    if 'rating' not in data:
        return jsonify({'error': 'rating is required'}), 400

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


if __name__ == '__main__':
    app.run(host='0.0.0.0', port=3007, debug=True, threaded=True)
