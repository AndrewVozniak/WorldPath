# ! Author: Andrew Vozniak
# ! Creation Date: 13.08.2023

# ? System imports
from flask import Flask, jsonify, request
from flask_restful import Resource, Api
from flask_cors import CORS

# ? Route Resources
from Resources.getWeatherInPlace import GetWeatherInPlace

app = Flask(__name__)
api = Api(app)

CORS(allow_headers='Content-Type')
CORS(app, resources={r"/*": {"origins": "*"}})

# ! Resources
api.add_resource(GetWeatherInPlace, '/getweather')

if __name__ == '__main__':
    app.run(debug=True)
