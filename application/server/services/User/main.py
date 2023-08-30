from flask import request, jsonify, Flask
from flask_cors import CORS

from pymongo.mongo_client import MongoClient

uri = "mongodb+srv://worldpath:uoSwHCbCc86dcHX3@cluster0.dumeyxp.mongodb.net/?retryWrites=true&w=majority"
client = MongoClient(uri)
db = client['Users']

try:
    client.admin.command('ping')
    print("Pinged your deployment. You successfully connected to MongoDB!")

except Exception as e:
    print(e)

app = Flask(__name__)

CORS(allow_headers='Content-Type')
CORS(app, resources={r"/*": {"origins": "*"}})


@app.route('/user/<int:user_id>', methods=['GET'])
def get_user_by_id(user_id):
    collection = db['Users']

    user = collection.find_one({'id': user_id})

    if user is None:
        return jsonify({'error': 'The user with this id does not exist.'})

    return jsonify({
        'id': user['id'],
        'name': user['name'],
        'email': user['email'],
        'profile_photo_path': user['profile_photo_path'],
        'is_admin': user['is_admin']
    })


@app.route('/user', methods=['GET'])
def get_user_by_token():
    collection = db['Users']

    print(request.headers)

    token = request.headers.get('Authorization')

    # Find user by token
    user = collection.find_one({'auth_token': token})

    if user is None:
        return jsonify({'error': 'The user with this token does not exist.'})

    return jsonify({
        'id': user['id'],
        'name': user['name'],
        'email': user['email'],
        'profile_photo_path': user['profile_photo_path'],
        'is_admin': user['is_admin']
    })


@app.route('/sign_in_by_username', methods=['POST'])
def sign_in_by_username():
    username = request.json.get('username')
    password = request.json.get('password')

    collection = db['Users']

    # Find user by username and password
    user = collection.find_one({'name': username, 'password': password})

    if user is None:
        return jsonify({'error': 'The user with this credentials does not exist.'})

    return jsonify({'username': user['name'], 'token': user['auth_token']})


@app.route('/sign_in_by_email', methods=['POST'])
def sign_in_by_email():
    collection = db['Users']

    email = request.json.get('email')
    password = request.json.get('password')

    # Find user by email and password
    user = collection.find_one({'email': email, 'password': password})

    if user is None:
        return jsonify({'error': 'The user with this credentials does not exist.'})

    return jsonify({'username': user['name'], 'token': user['auth_token']})


@app.route('/get_all_users', methods=['GET'])
def get_all_users():
    collection = db['Users']

    users_cursor = collection.find({})

    users_list = []

    for user in users_cursor:
        user.pop('_id', None) # Remove _id field from user
        users_list.append(user)

    if not users_list:
        return jsonify({'error': 'No users found.'})

    return jsonify(users_list)


@app.route('/validate_token', methods=['POST'])
def validate_token():
    token = request.headers.get('Authorization')

    # Find user by token
    user = next((item for item in USERS if item["auth_token"] == token), None)

    if user is None:
        return jsonify({'error': 'The user with this token does not exist.'})

    return jsonify({'user_id': user['id'], 'token_valid': True})


if __name__ == '__main__':
    app.run(host='127.0.0.1', port=3008, debug=True, threaded=False)
