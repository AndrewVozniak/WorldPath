from flask import jsonify


class UserController:
    @staticmethod
    def all_users():
        return jsonify({
            'users': [
                {
                    'id': 1,
                    'name': 'Andrew',
                    'surname': 'Vozniak',
                    'email': 'a.vozniaks@gmail.com',
                },
            ]
        })