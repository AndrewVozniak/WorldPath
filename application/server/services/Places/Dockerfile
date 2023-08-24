FROM python:3.8

ENV FLASK_APP=app.py
ENV FLASK_RUN_HOST=0.0.0.0

WORKDIR /usr/src/app

COPY . .

EXPOSE 5000

CMD ["flask", "run"]