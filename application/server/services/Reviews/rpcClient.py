import uuid
import pika


class RpcClient(object):
    def __init__(self, host):
        self.host = host
        self.response = None
        self.setup_channel()

    def setup_channel(self):
        self.connection = pika.BlockingConnection(pika.ConnectionParameters(self.host))
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

        # Пытаемся отправить сообщение
        try:
            self.channel.basic_publish(
                exchange='',
                routing_key=queue,
                properties=pika.BasicProperties(
                    reply_to=self.callback_queue,
                    correlation_id=self.corr_id,
                ),
                body=message)
        except (pika.exceptions.ChannelClosed, pika.exceptions.ConnectionClosed):
            print("Channel or connection was closed. Re-establishing and trying again.")
            self.setup_channel()  # Восстанавливаем канал и соединение
            return self.call(message, queue)  # Пытаемся снова

        while self.response is None:
            try:
                self.connection.process_data_events()
            except (pika.exceptions.ChannelClosed, pika.exceptions.ConnectionClosed):
                print("Channel or connection was closed. Re-establishing.")
                self.setup_channel()

        return self.response
