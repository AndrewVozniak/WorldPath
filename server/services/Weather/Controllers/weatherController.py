from . import apiController


class WeatherController:
    def __init__(self, city: str = None, lat: float = None, lon: float = None):
        self.city = city
        self.lat = lat
        self.lon = lon

    def get_current_weather(self):
        return apiController.ApiController().make_current_weather_request(self.city)

    def get_week_weather(self):
        response = apiController.ApiController().make_week_weather_request(self.city)

        week_forecast = []

        for i in response['list']:
            week_forecast.append({
                'date': i['dt_txt'],
                'temp': i['main']['temp'],
                'feels_like': i['main']['feels_like'],
                'temp_min': i['main']['temp_min'],
                'temp_max': i['main']['temp_max'],
                'pressure': i['main']['pressure'],
                'humidity': i['main']['humidity'],
                'weather': i['weather'][0]['main'],
                'weather_description': i['weather'][0]['description'],
                'wind_speed': i['wind']['speed'],
                'wind_deg': i['wind']['deg'],
                'visibility': i['visibility'],
                'clouds': i['clouds']['all'],
                'rain': i['rain']['3h'] if 'rain' in i else 0,
            })

        return week_forecast

    def get_current_weather_by_coordinates(self):
        return apiController.ApiController().make_current_weather_request_by_coordinates(self.lat, self.lon)

    def get_week_weather_by_coordinates(self):
        response = apiController.ApiController().make_week_weather_request_by_coordinates(self.lat, self.lon)

        week_forecast = []

        for i in response['list']:
            week_forecast.append({
                'date': i['dt_txt'],
                'temp': i['main']['temp'],
                'feels_like': i['main']['feels_like'],
                'temp_min': i['main']['temp_min'],
                'temp_max': i['main']['temp_max'],
                'pressure': i['main']['pressure'],
                'humidity': i['main']['humidity'],
                'weather': i['weather'][0]['main'],
                'weather_description': i['weather'][0]['description'],
                'wind_speed': i['wind']['speed'],
                'wind_deg': i['wind']['deg'],
                'visibility': i['visibility'],
                'clouds': i['clouds']['all'],
                'rain': i['rain']['3h'] if 'rain' in i else 0,
            })

        return week_forecast
