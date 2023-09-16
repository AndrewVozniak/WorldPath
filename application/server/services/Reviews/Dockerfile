FROM python

WORKDIR .

ENV FLASK_APP=main.py
ENV FLASK_RUN_HOST=0.0.0.0
ENV FLASK_RUN_PORT=3007
ENV PYTHONUNBUFFERED=1


COPY requirements.txt requirements.txt
RUN pip3 install -r requirements.txt

COPY . .

EXPOSE 3007

CMD [ "flask", "run" ]