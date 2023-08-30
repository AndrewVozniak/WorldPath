from flask import request, jsonify, Flask
from flask_cors import CORS

from pymongo.mongo_client import MongoClient

uri = "mongodb+srv://worldpath:uoSwHCbCc86dcHX3@cluster0.dumeyxp.mongodb.net/?retryWrites=true&w=majority"
client = MongoClient(uri)
db = client['Users']
collection = db['Users']

try:
    client.admin.command('ping')
    print("Pinged your deployment. You successfully connected to MongoDB!")

    if 'Users' in client.list_database_names():
        print("The database exists.")

        if 'Users' in db.list_collection_names():
            print("The collection exists.")


except Exception as e:
    print(e)

app = Flask(__name__)

CORS(allow_headers='Content-Type')
CORS(app, resources={r"/*": {"origins": "*"}})


# USERS = [
#     {
#         'id': 1,
#         'name': 'Andrew Vozniak',
#         'email': 'a.vozniaks@gmail.com',
#         'password': '123456',
#         'email_verified_at': '2020-01-01 00:00:00',
#         'auth_token': 'vozniak',
#         'profile_photo_path': 'https://via.placeholder.com/150',
#         'is_banned': False,
#         'is_warned': False,
#         'is_muted': False,
#         'is_verified': True,
#         'is_admin': True,
#         'updated_at': '2020-01-01 00:00:00',
#         'created_at': '2020-01-01 00:00:00'
#     },
#     {
#         'id': 2,
#         'name': 'Yaroslav Protsyk',
#         'email': 'yaroslavprotsyk@gmail.com',
#         'password': '123456',
#         'email_verified_at': '2020-01-01 00:00:00',
#         'auth_token': 'protsyk',
#         'profile_photo_path': 'https://via.placeholder.com/150',
#         'is_banned': False,
#         'is_warned': False,
#         'is_muted': False,
#         'is_verified': True,
#         'is_admin': True,
#         'updated_at': '2020-01-01 00:00:00',
#         'created_at': '2020-01-01 00:00:00'
#     },
# ]

@app.route('/user/<int:user_id>', methods=['GET'])
def get_user_by_id(user_id):
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
    print(request.headers)

    token = request.headers.get('Authorization')

    # Find user by token
    user = next((item for item in USERS if item["auth_token"] == token), None)

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

    # Find user by username and password
    user = next((item for item in USERS if item["name"] == username and item["password"] == password), None)

    if user is None:
        return jsonify({'error': 'The user with this credentials does not exist.'})

    return jsonify({'username': user['name'], 'token': user['auth_token']})


@app.route('/sign_in_by_email', methods=['POST'])
def sign_in_by_email():
    email = request.json.get('email')
    password = request.json.get('password')

    # Find user by email and password
    user = next((item for item in USERS if item["email"] == email and item["password"] == password), None)

    if user is None:
        return jsonify({'error': 'The user with this credentials does not exist.'})

    return jsonify({'username': user['name'], 'token': user['auth_token']})


@app.route('/get_all_users', methods=['GET'])
def get_all_user():
    return jsonify(USERS)


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
