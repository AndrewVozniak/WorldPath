import hashlib


def hash_password(password):
    if password is None:
        return None

    # Hash password
    hashed_password = hashlib.sha256(password.encode()).hexdigest()

    return hashed_password
