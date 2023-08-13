import requests


class ApiController:
    def __init__(self):
        self.apiKey = "345627b06ea58f0fa6f49fb5c51eb9b5"

    def make_current_weather_request(self, city):
        url = f'http://api.openweathermap.org/data/2.5/weather?q={city}&appid={self.apiKey}&units=metric'
        response = requests.get(url)

        return response.json()

    def make_week_weather_request(self, city):
        url = f'http://api.openweathermap.org/data/2.5/forecast?q={city}&appid={self.apiKey}&units=metric'
        response = requests.get(url)

        return response.json()

    def make_current_weather_request_by_coordinates(self, lat, lon):
        url = f'http://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={self.apiKey}&units=metric'
        response = requests.get(url)

        return response.json()

    def make_week_weather_request_by_coordinates(self, lat, lon):
        url = f'http://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&appid={self.apiKey}&units=metric'
        response = requests.get(url)

        return response.json()
