from flask import Flask, jsonify, request
import requests

app = Flask(__name__)


@app.route('/getRoutes', methods=['GET'])
def get_routes():
    lat = float(request.args.get('lat'))
    lon = float(request.args.get('lon'))
    radius = int(request.args.get('radius', 5000))

    overpass_url = "https://overpass-api.de/api/interpreter"

    overpass_query = f"""
    [out:json];
    way(around:{radius},{lat},{lon})["highway"];
    out body;
    """

    response = requests.get(overpass_url, params={'data': overpass_query})
    data = response.json()

    return jsonify(data)


if __name__ == '__main__':
    app.run()