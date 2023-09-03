import pika

RABBITMQ_HOST = 'localhost'
RABBITMQ_QUEUE = 'flask_queue'

connection = pika.BlockingConnection(pika.ConnectionParameters(host=RABBITMQ_HOST))
channel = connection.channel()
channel.queue_declare(queue=RABBITMQ_QUEUE)


def callback(ch, method, properties, body):
    print(f"Received: {body}")


channel.basic_consume(queue=RABBITMQ_QUEUE, on_message_callback=callback, auto_ack=True)

print('Waiting for messages...')
channel.start_consuming()
