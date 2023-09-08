import json

import pika
from database import db
from bson import ObjectId

RABBITMQ_HOST = 'rabbitmq-user'


def get_user_base_info(ch, method, props, body):
    user_id = body.decode()

    users_collection = db['Users']

    user = users_collection.find_one({"_id": ObjectId(user_id)})

    response = json.dumps({
        "id": str(user['_id']),
        "name": user['name'],
        "email": user['email'],
        "profile_photo_path": user['profile_photo_path'],
        "created_at": user['created_at'],
        "updated_at": user['updated_at']
    })

    ch.basic_publish(
        exchange='',
        routing_key=props.reply_to,
        properties=pika.BasicProperties(
            correlation_id=props.correlation_id
        ),
        body=response)


connection = pika.BlockingConnection(pika.ConnectionParameters(host=RABBITMQ_HOST))
channel = connection.channel()

channel.queue_declare(queue='get_user_base_info')
channel.basic_consume(queue='get_user_base_info', on_message_callback=get_user_base_info)

print("RPC Server is waiting for requests...")
channel.start_consuming()
