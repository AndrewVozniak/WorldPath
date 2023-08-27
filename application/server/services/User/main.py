from flask import request, jsonify, Flask
from flask_cors import CORS

app = Flask(__name__)

# CORS(allow_headers='Content-Type')
# CORS(app, resources={r"/*": {"origins": "*"}})


@app.route('/user', methods=['GET'])
def get_user():
    print('Handling GET request')
    data = request.get_json()
    print('Data received:', data)
    return jsonify({'user_id': request.headers.get('Userid'), 'token': request.headers.get('Authorization'),
                    'method': 'GET'})


@app.route('/user', methods=['POST'])
def create_user():
    print('Handling POST request')
    data = request.get_json()
    print('Data received:', data)
    return jsonify({'user_id': request.headers.get('Userid'), 'token': request.headers.get('Authorization'),
                    'method': 'POST'})


@app.route('/user', methods=['PUT'])
def update_user():
    print('Handling PUT request')
    data = request.get_json()
    print('Data received:', data)
    return jsonify({'user_id': request.headers.get('Userid'), 'token': request.headers.get('Authorization'),
                    'method': 'PUT'})


@app.route('/user', methods=['PATCH'])
def patch_user():
    print('Handling PATCH request')
    data = request.get_json()
    print('Data received:', data)
    return jsonify({'user_id': request.headers.get('Userid'), 'token': request.headers.get('Authorization'),
                    'method': 'PATCH'})


@app.route('/user', methods=['DELETE'])
def delete_user():
    print('Handling DELETE request')
    data = request.get_json()
    print('Data received:', data)
    return jsonify({'user_id': request.headers.get('Userid'), 'token': request.headers.get('Authorization'),
                    'method': 'DELETE'})


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
