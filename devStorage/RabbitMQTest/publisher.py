import pika
from flask import Flask, jsonify

app = Flask(__name__)

RABBITMQ_HOST = 'localhost'
RABBITMQ_QUEUE = 'flask_queue'

connection = pika.BlockingConnection(pika.ConnectionParameters(host=RABBITMQ_HOST))
channel = connection.channel()
channel.queue_declare(queue=RABBITMQ_QUEUE)


@app.route('/publish/<message>')
def publish(message):
    channel.basic_publish(exchange='', routing_key=RABBITMQ_QUEUE, body=message)
    return jsonify({"message": "Message published!"})


@app.route('/')
def hello():
    return jsonify({"message": "Hello, World!"})


if __name__ == "__main__":
    try:
        app.run(debug=True)
    finally:
        connection.close()
