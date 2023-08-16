import requests


def get_traffic_data(lat, lon, radius=5000):
    overpass_url = "http://overpass-api.de/api/interpreter"

    # Запрос на получение дорог в радиусе 1 км от заданной точки
    overpass_query = f"""
    [out:json];
    way(around:{radius},{lat},{lon})["highway"];
    (._;>;);
    out body;
    """

    response = requests.get(overpass_url, params={'data': overpass_query})
    data = response.json()

    # Пример вывода названий дорог и их тип (например, primary или secondary)
    for element in data['elements']:
        if element['type'] == 'way':

            print(element['tags'].get('name', 'Unknown road'), ":", element['tags']['highway'])


get_traffic_data(48.1695, 24.9354)  # Пример для говерли
