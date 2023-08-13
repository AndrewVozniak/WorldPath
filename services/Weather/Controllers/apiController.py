class ApiController:
    def __init__(self):
        self.apiKey = "345627b06ea58f0fa6f49fb5c51eb9b5"

    def make_weather_request(self, city):
        import requests
        url = f'http://api.openweathermap.org/data/2.5/weather?q={city}&appid={self.apiKey}&units=metric'
        response = requests.get(url)
        return response.json()