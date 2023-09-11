import json

import pika
from database import db
from bson import ObjectId

RABBITMQ_HOST = 'rabbitmq-travels'


def get_liked_travels(ch, method, props, body):
    try:
        user_id = body.decode()

        likes_collection = db['Likes']
        travels_collection = db['Travels']
        places_collection = db['Places']
        routes_collection = db['Routes']

        likes = []

        for like in likes_collection.find({"user_id": ObjectId(user_id)}, {"travel_id": 1}):
            travel = travels_collection.find_one({"_id": like['travel_id']}, {"_id": 1, "title": 1, "description": 1, "type": 1, "updated_at": 1, "created_at": 1})

            places = []
            routes = []

            for place in places_collection.find({"travel_id": travel['_id']}, {"place_id": 1}):
                places.append({
                    "place_id": str(place['place_id'])
                })

            for route in routes_collection.find({"travel_id": travel['_id']}, {"route_id": 1}):
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
    except Exception as e:
        print(f"Error processing message: {e}")
    finally:
        ch.basic_ack(delivery_tag=method.delivery_tag)


def get_travels_by_ids(ch, method, props, body):
    try:
        travels_to_prepare = json.loads(body.decode())  # parse JSON string to Python list or dict
        print(travels_to_prepare)

        travels_collection = db['Travels']

        travels = []

        # Assuming travels_to_prepare is a list of dictionaries, each with a "travel_id" key:
        for travel_dict in travels_to_prepare:
            travel_id = travel_dict["travel_id"]
            travel = travels_collection.find_one({"_id": ObjectId(travel_id)}, {"_id": 1, "title": 1, "description": 1, "type": 1, "updated_at": 1, "created_at": 1})

            travels.append({
                "id": str(travel['_id']),
                "title": travel['title'],
                "description": travel['description'],
                "type": travel['type'],
                "updated_at": travel['updated_at'],
                "created_at": travel['created_at']
            })

        response = json.dumps(travels)

        ch.basic_publish(
            exchange='',
            routing_key=props.reply_to,
            properties=pika.BasicProperties(
                correlation_id=props.correlation_id
            ),
            body=response)
    except Exception as e:
        print(f"Error processing message: {e}")
    finally:
        ch.basic_ack(delivery_tag=method.delivery_tag)


connection = pika.BlockingConnection(pika.ConnectionParameters(host=RABBITMQ_HOST))
channel = connection.channel()

channel.queue_declare(queue='get_liked_travels')
channel.basic_consume(queue='get_liked_travels', on_message_callback=get_liked_travels)

channel.queue_declare(queue='get_travels_by_ids')
channel.basic_consume(queue='get_travels_by_ids', on_message_callback=get_travels_by_ids)

print("RPC Server is waiting for requests...")
channel.start_consuming()
