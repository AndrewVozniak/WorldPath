import requests


def get_places_nearby(lat, lon, radius=5000):
    overpass_url = "http://overpass-api.de/api/interpreter"

    overpass_query = f"""
    [out:json];
    node(around:{radius},{lat},{lon})["amenity"];
    out body;
    """

    response = requests.get(overpass_url, params={'data': overpass_query})
    data = response.json()

    for element in data['elements']:
        name = element['tags'].get('name', 'Unknown place')
        amenity_type = element['tags']['amenity']
        print(f"{name} ({amenity_type}) at {element['lat']}, {element['lon']}")


get_places_nearby(60.1695, 24.9354)  # Координати Гельсинки
