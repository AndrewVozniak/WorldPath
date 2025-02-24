import pika
import uuid
from flask import Flask, jsonify

app = Flask(__name__)

RABBITMQ_HOST = 'localhost'


class RpcClient(object):
    def __init__(self):
        self.connection = pika.BlockingConnection(pika.ConnectionParameters(host=RABBITMQ_HOST))
        self.channel = self.connection.channel()

        result = self.channel.queue_declare(queue='', exclusive=True)
        self.callback_queue = result.method.queue

        self.channel.basic_consume(
            queue=self.callback_queue,
            on_message_callback=self.on_response,
            auto_ack=True)

    def on_response(self, ch, method, props, body):
        if self.corr_id == props.correlation_id:
            self.response = body

    def call(self, message, queue):
        self.response = None
        self.corr_id = str(uuid.uuid4())
        self.channel.basic_publish(
            exchange='',
            routing_key=queue,
            properties=pika.BasicProperties(
                reply_to=self.callback_queue,
                correlation_id=self.corr_id,
            ),
            body=message)
        while self.response is None:
            self.connection.process_data_events()
        return self.response


rpc_client = RpcClient()


@app.route('/hello/<message>')
def rpc_call(message):
    response = rpc_client.call(message, queue='hello_queue')
    return jsonify({"response": response.decode()})


@app.route('/bye/<message>')
def rpc_call2(message):
    response = rpc_client.call(message, queue='bye_queue')
    return jsonify({"response": response.decode()})


if __name__ == "__main__":
    app.run(debug=True)
