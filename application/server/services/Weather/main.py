# ! Author: Andrew Vozniak
# ! Creation Date: 13.08.2023

# ? System imports
from flask import Flask, jsonify, request
from flask_restful import Resource, Api
from flask_cors import CORS

# ? Route Resources
from Resources.getCurrentWeatherInPlace import GetCurrentWeatherInPlace
from Resources.getWeekWeatherInPlace import GetWeekWeatherInPlace
from Resources.getCurrentWeatherInPlaceByCoordinates import GetCurrentWeatherInPlaceByCoordinates
from Resources.getWeekWeatherInPlaceByCoordinates import GetWeekWeatherInPlaceByCoordinates

app = Flask(__name__)
api = Api(app)

CORS(allow_headers='Content-Type')
CORS(app, resources={r"/*": {"origins": "*"}})

# ! Resources
# * Get current weather in place by city name
api.add_resource(GetCurrentWeatherInPlace, '/weather/now')

# * Get weather in place for week by city name
api.add_resource(GetWeekWeatherInPlace, '/weather/week')

# * Get current weather in place by coordinates
api.add_resource(GetCurrentWeatherInPlaceByCoordinates, '/weather/now/coordinates')

# * Get weather in place for week by coordinates
api.add_resource(GetWeekWeatherInPlaceByCoordinates, '/weather/week/coordinates')


@app.before_request
def before_request():
    print(request.headers)


if __name__ == '__main__':
    app.run(debug=True, port=3001)