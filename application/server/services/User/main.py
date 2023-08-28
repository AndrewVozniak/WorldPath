from flask import request, jsonify, Flask
from flask_cors import CORS

app = Flask(__name__)

CORS(allow_headers='Content-Type')
CORS(app, resources={r"/*": {"origins": "*"}})

USERS = [
    {
        'id': 1,
        'name': 'Andrew Vozniak',
        'email': 'a.vozniaks@gmail.com',
        'password': '123456',
        'email_verified_at': '2020-01-01 00:00:00',
        'auth_token': 'vozniak',
        'profile_photo_path': 'https://via.placeholder.com/150',
        'is_banned': False,
        'is_warned': False,
        'is_muted': False,
        'is_verified': True,
        'is_admin': True,
        'updated_at': '2020-01-01 00:00:00',
        'created_at': '2020-01-01 00:00:00'
    },
    {
        'id': 2,
        'name': 'Yaroslav Protsyk',
        'email': 'yaroslavprotsyk@gmail.com',
        'password': '123456',
        'email_verified_at': '2020-01-01 00:00:00',
        'auth_token': 'protsyk',
        'profile_photo_path': 'https://via.placeholder.com/150',
        'is_banned': False,
        'is_warned': False,
        'is_muted': False,
        'is_verified': True,
        'is_admin': True,
        'updated_at': '2020-01-01 00:00:00',
        'created_at': '2020-01-01 00:00:00'
    },
]


@app.route('/sign_in_by_username', methods=['POST'])
def sign_in_by_username():
    username = request.json.get('username')
    password = request.json.get('password')

    # Find user by username and password
    user = next((item for item in USERS if item["name"] == username and item["password"] == password), None)

    if user is None:
        return jsonify({'error': 'The user with this credentials does not exist.'})

    return jsonify({'username': user['name'], 'token': user['auth_token']})


@app.route('/get_all_users', methods=['GET'])
def get_all_user():
    return jsonify(USERS)


@app.route('/validate_token', methods=['POST'])
def validate_token():
    token = request.headers.get('Authorization')

    # TODO: Real validate token logic
    if token is None:
        return jsonify({'user_id': None, 'token_valid': False})

    elif token == 'test':
        return jsonify({'user_id': 1, 'token_valid': True})

    else:
        return jsonify({'user_id': None, 'token_valid': False})


if __name__ == '__main__':
    app.run(host='127.0.0.1', port=3008, debug=True, threaded=False)
