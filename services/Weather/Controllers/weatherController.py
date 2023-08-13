from . import apiController



class WeatherController:
    def __init__(self, city: str):
        self.city = city

    def get_weather(self):
        return apiController.ApiController().make_weather_request(self.city)