import datetime
import hashlib
import uuid

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


def generate_auth_token(users):
    # Generate auth token 32 characters long
    token = uuid.uuid4().hex + uuid.uuid4().hex

    # Check if token already exists
    if any(user['auth_token'] == token for user in users):
        generate_auth_token(users)

    return token


def hash_password(password):
    # Hash password
    hashed_password = hashlib.sha256(password.encode()).hexdigest()

    return hashed_password


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

    token = request.headers.get('Authorization')

    if token is None:
        return jsonify({'error': 'No token provided.'})

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


@app.route('/create_user', methods=['POST'])
def create_user():
    collection = db['Users']

    users = get_all_users_helper()

    name = request.json.get('name')
    email = request.json.get('email')
    password = hash_password(request.json.get('password'))

    # Check if name is not empty
    if name is None:
        return jsonify({'error': 'Name is empty.'})

    # Check if email is not empty
    if email is None:
        return jsonify({'error': 'Email is empty.'})

    # Check if password is not empty
    if password is None:
        return jsonify({'error': 'Password is empty.'})

    user_document = {
        'id': len(users) + 1,
        'name': name,
        'email': email,
        'password': password,
        'auth_token': generate_auth_token(users),
        'email_verified_at': None,
        'profile_photo_path': None,
        'is_banned': False,
        'is_warned': False,
        'is_muted': False,
        'is_verified': False,
        'is_admin': False,
        'updated_at': datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S"),
        'created_at': datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    }

    # Check if user with this name already exists
    if any(user['name'] == name for user in users):
        return jsonify({'error': 'The user with this name already exists.'})

    # Check if user with this email already exists
    if any(user['email'] == email for user in users):
        return jsonify({'error': 'The user with this email already exists.'})

    # Insert user into database
    collection.insert_one(user_document)

    return jsonify({'success': 'User created successfully.',
                    'user_id': user_document['id'],
                    'token': user_document['auth_token']})


@app.route('/sign_in_by_username', methods=['POST'])
def sign_in_by_username():
    username = request.json.get('username')
    password = hash_password(request.json.get('password'))

    collection = db['Users']

    # Find user by username and password
    user = collection.find_one({'name': username, 'password': password})

    if user is None:
        return jsonify({'error': 'The user with this credentials does not exist.'})

    return jsonify({'username': user['name'], 'token': user['auth_token']})


@app.route('/sign_in_by_email', methods=['POST'])
def sign_in_by_email():
    email = request.json.get('email')
    password = hash_password(request.json.get('password'))

    collection = db['Users']

    # Find user by email and password
    user = collection.find_one({'email': email, 'password': password})

    if user is None:
        return jsonify({'error': 'The user with this credentials does not exist.'})

    return jsonify({'username': user['name'], 'token': user['auth_token']})


def get_all_users_helper():
    collection = db['Users']

    users_cursor = collection.find({})

    users_list = []

    for user in users_cursor:
        user.pop('_id', None)  # Remove _id field from user
        users_list.append(user)

    return users_list


@app.route('/get_all_users', methods=['GET'])
def get_all_users():
    users = get_all_users_helper()

    if not users:
        return jsonify({'error': 'No users found.'})

    return jsonify(users)


@app.route('/validate_token', methods=['POST'])
def validate_token():
    collection = db['Users']
    token = request.headers.get('Authorization')

    # Find user by token
    user = collection.find_one({'auth_token': token})

    if user is None:
        return jsonify({'error': 'The user with this token does not exist.'})

    return jsonify({'user_id': user['id'], 'token_valid': True})


if __name__ == '__main__':
    app.run(host='127.0.0.1', port=3008, debug=True, threaded=False)
