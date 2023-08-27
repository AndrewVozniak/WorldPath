from flask import request, jsonify, Flask
from flask_cors import CORS

app = Flask(__name__)

# CORS(allow_headers='Content-Type')
# CORS(app, resources={r"/*": {"origins": "*"}})


@app.route('/user', methods=['GET'])
def get_user():
    return jsonify({'user_id': request.headers.get('Userid'), 'token': request.headers.get('Authorization')})


@app.route('/user', methods=['POST'])
def create_user():
    try:
        print('Handling POST request')
        data = request.get_json()
        print('Data received:', data)
        return jsonify({'user_id': request.headers.get('Userid'), 'token': request.headers.get('Authorization')})
    except Exception as e:
        print("Error handling POST:", str(e))
        return jsonify({'error': str(e)}), 500


@app.route('/user', methods=['PUT'])
def update_user():
    return jsonify({'user_id': request.headers.get('Userid'), 'token': request.headers.get('Authorization')})


@app.route('/user', methods=['DELETE'])
def delete_user():
    return jsonify({'user_id': request.headers.get('Userid'), 'token': request.headers.get('Authorization')})


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
