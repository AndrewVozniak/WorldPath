import json

import pika
from database import db
from bson import ObjectId

RABBITMQ_HOST = 'rabbitmq-place'


def get_place_by_name(ch, method, props, body):
    try:
        places_names = json.loads(body.decode())

        places_collection = db['Places']

        places = places_collection.find({"_id": {"$in": [ObjectId(place_id) for place_id in places_names]}},
                                        {"name": 1, "profile_photo_path": 1})

        if places is None:
            response = json.dumps({"error": "Places not found"})
        else:
            response = json.dumps([{
                "id": str(place['_id']),
                "name": place['Name'],
                "lat": place['Lat'],
                "lon": place['Lon'],
                "placeType": place['PlaceType'],
                "updatedAt": place['UpdatedAt'],
                "createdAt": place['CreatedAt'],
                "location": place['Location']
            } for place in places])

            print(response)

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

channel.queue_declare(queue='get_place_by_name')
channel.basic_consume(queue='get_place_by_name', on_message_callback=get_place_by_name)

print("RPC Server is waiting for requests...")
channel.start_consuming()
