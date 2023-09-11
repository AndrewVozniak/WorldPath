import uuid
import pika
import time


class RpcClient(object):
    MAX_RETRIES = 3
    TIMEOUT = 5

    def __init__(self, host):
        self.host = host
        self.connect()

    def connect(self):
        self.connection = pika.BlockingConnection(pika.ConnectionParameters(self.host))
        self.channel = self.connection.channel()

        result = self.channel.queue_declare(queue='', exclusive=True)
        self.callback_queue = result.method.queue

        self.channel.basic_consume(
            queue=self.callback_queue,
            on_message_callback=self.on_response,
            auto_ack=True)

    def reconnect(self):
        if self.connection.is_open:
            self.connection.close()
        self.connect()

    def on_response(self, ch, method, props, body):
        if self.corr_id == props.correlation_id:
            self.response = body

    def call(self, message, queue, retry_count=0):
        if retry_count >= self.MAX_RETRIES:
            raise Exception("Max retries reached. Could not connect to RabbitMQ.")

        self.response = None
        self.corr_id = str(uuid.uuid4())
        try:
            self.channel.basic_publish(
                exchange='',
                routing_key=queue,
                properties=pika.BasicProperties(
                    reply_to=self.callback_queue,
                    correlation_id=self.corr_id,
                ),
                body=message)
        except pika.exceptions.ChannelWrongStateError:
            self.reconnect()
            return self.call(message, queue, retry_count + 1)  # рекурсивный вызов

        end_time = time.time() + self.TIMEOUT
        while self.response is None and time.time() < end_time:
            try:
                self.connection.process_data_events()
            except pika.exceptions.ChannelWrongStateError:
                self.reconnect()

        if self.response is None:
            raise Exception("Timeout waiting for response from RabbitMQ.")

        return self.response
