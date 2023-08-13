from flask import request, jsonify
from flask_restful import Resource

from Controllers import weatherController as weatherApi


class GetWeekWeatherInPlaceByCoordinates(Resource):
    def get(self):
        lat = request.args.get('lat')
        lon = request.args.get('lon')

        if lat and lon:
            weather = weatherApi.WeatherController(lat=lat, lon=lon).get_week_weather_by_coordinates()
            return jsonify(weather)

        return f"Please provide a city in the query string. " \
               f"Example: {request.url}?lat=lat&lon=lon", 400
