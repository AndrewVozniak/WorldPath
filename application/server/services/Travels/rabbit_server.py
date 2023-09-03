import json

import pika
from database import db
from bson import ObjectId

RABBITMQ_HOST = 'localhost'


def get_liked_travels(ch, method, props, body):
    user_id = body.decode()

    likes_collection = db['Likes']
    travels_collection = db['Travels']
    places_collection = db['Places']
    routes_collection = db['Routes']

    likes = []

    for like in likes_collection.find({"user_id": ObjectId(user_id)}):
        travel = travels_collection.find_one({"_id": like['travel_id']})

        places = []
        routes = []

        for place in places_collection.find({"travel_id": travel['_id']}):
            places.append({
                "place_id": str(place['place_id'])
            })

        for route in routes_collection.find({"travel_id": travel['_id']}):
            routes.append({
                "route_id": str(route['route_id'])
            })

        likes.append({
            "id": str(travel['_id']),
            "title": travel['title'],
            "description": travel['description'],
            "type": travel['type'],
            "places": places,
            "routes": routes,
            "updated_at": travel['updated_at'],
            "created_at": travel['created_at']
        })

    response = json.dumps(likes)

    ch.basic_publish(
        exchange='',
        routing_key=props.reply_to,
        properties=pika.BasicProperties(
            correlation_id=props.correlation_id
        ),
        body=response)
    ch.basic_ack(delivery_tag=method.delivery_tag)


connection = pika.BlockingConnection(pika.ConnectionParameters(host=RABBITMQ_HOST))
channel = connection.channel()

channel.queue_declare(queue='get_liked_travels')
channel.basic_consume(queue='get_liked_travels', on_message_callback=get_liked_travels)

print("RPC Server is waiting for requests...")
channel.start_consuming()
