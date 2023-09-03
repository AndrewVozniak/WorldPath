import uuid


def generate_auth_token(users):
    # Generate auth token 32 characters long
    token = uuid.uuid4().hex + uuid.uuid4().hex

    # Check if token already exists
    if any(user['auth_token'] == token for user in users):
        generate_auth_token(users)

    return token
