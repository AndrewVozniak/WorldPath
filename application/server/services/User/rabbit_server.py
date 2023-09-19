import json

import pika
from database import db
from bson import ObjectId

RABBITMQ_HOST = 'rabbitmq-user'


def get_users_base_info(ch, method, props, body):
    try:
        users_ids = json.loads(body.decode())

        users_collection = db['Users']

        users = users_collection.find({"_id": {"$in": [ObjectId(user_id) for user_id in users_ids]}},
                                      {"name": 1, "profile_photo_path": 1})

        if users is None:
            response = json.dumps({"error": "Users not found"})
        else:
            response = json.dumps([{
                "id": str(user['_id']),
                "name": user['name'],
                "profile_photo_path": user['profile_photo_path'],
            } for user in users])

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

channel.queue_declare(queue='get_users_base_info')
channel.basic_consume(queue='get_users_base_info', on_message_callback=get_users_base_info)

print("RPC Server is waiting for requests...")
channel.start_consuming()
