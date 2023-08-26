# ! Author: Andrew Vozniak
# ! Creation Date: 13.08.2023

# ? System imports
from flask import Flask, jsonify, request
from flask_restful import Resource, Api
from flask_cors import CORS

from admin.server.Resources import UserResource

# ? User Resources

app = Flask(__name__)
api = Api(app)

CORS(allow_headers='Content-Type')
CORS(app, resources={r"/*": {"origins": "*"}})

# ! Resources
# * Get all users
api.add_resource(UserResource.all_users(), '/api/users')

if __name__ == '__main__':
    app.run(debug=True)
