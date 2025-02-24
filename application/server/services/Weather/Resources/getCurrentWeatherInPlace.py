from flask import request, jsonify
from flask_restful import Resource

from Controllers import weatherController as weatherApi


class GetCurrentWeatherInPlace(Resource):
    def get(self):
        city = request.args.get('city')

        if city:
            weather = weatherApi.WeatherController(city=city).get_current_weather()
            return jsonify(weather)

        return f"Please provide a city in the query string. " \
               f"Example: {request.url}?city=cityName", 400
