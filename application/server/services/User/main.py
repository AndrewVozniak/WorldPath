import datetime
import json
import rpcClient

from bson import ObjectId
from flask import request, jsonify, Flask
from flask_cors import CORS
from database import db
from actions.hash_password import hash_password
from actions.generate_auth_token import generate_auth_token

app = Flask(__name__)

CORS(allow_headers='Content-Type')
CORS(app, resources={r"/*": {"origins": "*"}})


@app.route('/user', methods=['GET'])
async def get_user_by_token():
    collection = db['Users']

    token = request.headers.get('Authorization')

    if token is None:
        return jsonify({'error': 'No token provided.'})

    # Find user by token
    user = collection.find_one({'auth_token': token})

    if user is None:
        return jsonify({'error': 'The user with this token does not exist.'})

    return jsonify({
        'id': str(user['_id']),
        'name': user['name'],
        'email': user['email'],
        'profile_photo_path': user['profile_photo_path'],
        'is_banned': user['is_banned'],
        'is_warned': user['is_warned'],
        'is_muted': user['is_muted'],
        'is_verified': user['is_verified'],
        'is_admin': user['is_admin'],
        'updated_at': user['updated_at'],
        'created_at': user['created_at']
    })


@app.route('/user/<user_id>', methods=['GET'])
async def get_user_by_id(user_id):
    collection = db['Users']

    user = collection.find_one({'_id': ObjectId(user_id)})

    if user is None:
        return jsonify({'error': 'The user with this id does not exist.'})

    return jsonify({
        'id': str(user['_id']),
        'name': user['name'],
        'email': user['email'],
        'profile_photo_path': user['profile_photo_path'],
        'is_banned': user['is_banned'],
        'is_warned': user['is_warned'],
        'is_muted': user['is_muted'],
        'is_verified': user['is_verified'],
        'is_admin': user['is_admin'],
        'updated_at': user['updated_at'],
        'created_at': user['created_at']
    })


@app.route('/user', methods=['PUT'])
async def update_user_by_token():
    token = request.headers.get('Authorization')

    if token is None:
        return jsonify({'error': 'No token provided.'})

    collection = db['Users']

    # Find user by token
    user = collection.find_one({'auth_token': token})

    if user is None:
        return jsonify({'error': 'The user with this token does not exist.'})

    # Prepare updated info
    updated_info = {
        'name': request.json.get('name', user['name']),
        'email': request.json.get('email', user['email']),
        'password': hash_password(request.json.get('password')) if request.json.get('password') else user['password'],
        'email_verified_at': request.json.get('email_verified_at', user['email_verified_at']),
        'profile_photo_path': request.json.get('profile_photo_path', user['profile_photo_path']),
        'is_banned': request.json.get('is_banned', user['is_banned']),
        'is_warned': request.json.get('is_warned', user['is_warned']),
        'is_muted': request.json.get('is_muted', user['is_muted']),
        'is_verified': request.json.get('is_verified', user['is_verified']),
        'is_admin': request.json.get('is_admin', user['is_admin']),
        'updated_at': datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    }

    # Check if user with this name but another auth token already exists
    if any(user['name'] == updated_info['name'] and user['auth_token'] != token for user in
           await get_all_users_helper()):
        return jsonify({'error': 'The user with this name already exists.'})

    # Check if user with this email but another auth token already exists
    if any(user['email'] == updated_info['email'] and user['auth_token'] != token for user in
           await get_all_users_helper()):
        return jsonify({'error': 'The user with this email already exists.'})

    # Update user
    collection.update_one({'auth_token': token}, {'$set': updated_info})

    return jsonify({'success': 'User updated successfully.'})


@app.route('/user/<user_id>', methods=['PUT'])
async def update_user_by_id(user_id):
    collection = db['Users']

    # Find user by id
    if user_id is None:
        return jsonify({'error': 'No user id provided.'})

    user_id = ObjectId(user_id)
    user = collection.find_one({'_id': user_id})

    if user is None:
        return jsonify({'error': 'The user with this id does not exist.'})

    # Prepare updated info
    updated_info = {
        'name': request.json.get('name', user['name']),
        'email': request.json.get('email', user['email']),
        'password': hash_password(request.json.get('password')) if request.json.get('password') else user['password'],
        'email_verified_at': request.json.get('email_verified_at', user['email_verified_at']),
        'profile_photo_path': request.json.get('profile_photo_path', user['profile_photo_path']),
        'is_banned': request.json.get('is_banned', user['is_banned']),
        'is_warned': request.json.get('is_warned', user['is_warned']),
        'is_muted': request.json.get('is_muted', user['is_muted']),
        'is_verified': request.json.get('is_verified', user['is_verified']),
        'is_admin': request.json.get('is_admin', user['is_admin']),
        'updated_at': datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    }

    # Check if user with this name but another id already exists
    if any(user['name'] == updated_info['name'] and user['_id'] != user_id for user in await get_all_users_helper()):
        return jsonify({'error': 'The user with this name already exists.'})

    # Check if user with this email but another id already exists
    if any(user['email'] == updated_info['email'] and user['_id'] != user_id for user in await get_all_users_helper()):
        return jsonify({'error': 'The user with this email already exists.'})

    # Update user
    collection.update_one({'_id': user_id}, {'$set': updated_info})

    return jsonify({'success': 'User updated successfully.'})


@app.route('/create_user', methods=['POST'])
async def create_user():
    collection = db['Users']

    users = await get_all_users_helper()

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
        'name': name,
        'email': email,
        'password': password,
        'auth_token': generate_auth_token(users),
        'email_verified_at': None,
        'profile_photo_path': 'https://ui-avatars.com/api/?name=' + name + '&background=f4f4f4&color=253158&size=128'
                                                                           '&bold=true',
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
                    'user_id': str(user_document['_id']),
                    'token': user_document['auth_token']})


@app.route('/sign_in_by_username', methods=['POST'])
async def sign_in_by_username():
    username = request.json.get('username')
    password = hash_password(request.json.get('password'))

    collection = db['Users']

    # Find user by username and password
    user = collection.find_one({'name': username, 'password': password})

    if user is None:
        return jsonify({'error': 'The user with this credentials does not exist.'})

    return jsonify({'username': user['name'], 'token': user['auth_token']})


@app.route('/sign_in_by_auth_token', methods=['POST'])
async def sign_in_by_auth_token():
    token = request.headers.get('Authorization')

    collection = db['Users']

    # Find user by token
    user = collection.find_one({'auth_token': token})

    if user is None:
        return jsonify({'error': 'The user with this token does not exist.'})

    return jsonify({'username': user['name'], 'token': user['auth_token']})


@app.route('/sign_in_by_email', methods=['POST'])
async def sign_in_by_email():
    email = request.json.get('email')
    password = hash_password(request.json.get('password'))

    collection = db['Users']

    # Find user by email and password
    user = collection.find_one({'email': email, 'password': password})

    if user is None:
        return jsonify({'error': 'The user with this credentials does not exist.'})

    return jsonify({'username': user['name'], 'token': user['auth_token']})


async def get_all_users_helper():
    collection = db['Users']

    users_cursor = collection.find({})

    users_list = []

    for user in users_cursor:
        user['_id'] = str(user['_id'])
        user['id'] = user.pop('_id')
        user.pop('auth_token')
        user.pop('password')
        users_list.append(user)

    return users_list


@app.route('/get_all_users', methods=['GET'])
async def get_all_users():
    users = await get_all_users_helper()

    if not users:
        return jsonify({'error': 'No users found.'})

    return jsonify(users)


@app.route('/validate_token', methods=['POST'])
async def validate_token():
    collection = db['Users']
    token = request.headers.get('Authorization')

    # Find user by token
    user = collection.find_one({'auth_token': token})

    if user is None:
        return jsonify({'error': 'The user with this token does not exist.'})

    return jsonify({'user_id': str(user['_id']), 'token_valid': True})


@app.route('/travel_history', methods=['POST'])
async def add_travel_to_history():
    history_collection = db['Travel_Histories']

    data = request.get_json()

    user_id = request.headers.get('Userid')

    # Get travel_id from data safely
    travel_id = data.get('travel_id')

    # Basic validations
    if not travel_id:
        return jsonify({"message": "Travel id is required"}), 400

    if user_id is None:
        return jsonify({"message": "User id is required"}), 400

    # Prepare data to insert
    history_info = {
        "user_id": ObjectId(user_id),
        "travel_id": ObjectId(travel_id),
        "updated_at": datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S"),
        "created_at": datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    }

    history_collection.insert_one(history_info)

    return jsonify({"message": "Travel added to history successfully"})


@app.route('/get_travel_history', methods=['GET'])
async def get_travel_history():
    history_collection = db['Travel_Histories']

    user_id = request.headers.get('Userid')

    if user_id is None:
        return jsonify({"message": "User id is required"}), 400

    history_cursor = history_collection.find({"user_id": ObjectId(user_id)})

    raw_history_list = []

    for history in history_cursor:
        history['_id'] = str(history['_id'])
        history['id'] = history.pop('_id')
        history['user_id'] = str(history['user_id'])
        history['travel_id'] = str(history['travel_id'])
        raw_history_list.append(history)

    response = rpcClient.RpcClient('rabbitmq-travels').call(json.dumps(raw_history_list), queue='get_travels_by_ids')

    history_list = json.loads(response.decode())
    print(history_list)

    return jsonify(history_list)


@app.route('/liked_travels', methods=['GET'])
async def get_liked_travels():
    user_id = request.headers.get('Userid')

    response = rpcClient.RpcClient('rabbitmq-travels').call(user_id, queue='get_liked_travels')

    data = json.loads(response.decode())

    return jsonify(data)


@app.route('/update_password', methods=['PUT'])
async def update_password():
    users_collection = db['Users']


if __name__ == '__main__':
    app.run(host='0.0.0.0', port=3008, debug=True, threaded=True)
