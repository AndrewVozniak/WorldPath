## API Documentation: Travel Service

This document provides an overview of the Travel Service API, which allows users to manage travel-related information, including travels, places, routes, comments, and likes.

### Table of Contents

- [Introduction](#introduction)
- [API Endpoints](#api-endpoints)
  - [Get Travels](#get-travels)
  - [Get Travels by User ID](#get-travels-by-user-id)
  - [Get Travel by Travel ID](#get-travel-by-travel-id)
  - [Add Travel](#add-travel)
  - [Edit Travel](#edit-travel)
  - [Delete Travel](#delete-travel)
  - [Add Comment](#add-comment)
  - [Add Like](#add-like)
  - [Delete Like](#delete-like)

### Introduction

The Travel Service API provides functionality to manage travel-related data, including information about travels, places, routes, comments, and likes. It communicates with a MongoDB database to store and retrieve data.

### API Endpoints

#### Get Travels

Retrieve a list of travels.

- **URL:** `/travel_service/travels/`
- **Method:** `GET`

##### Response

- **Status Code:** 200 (OK)
- **Response Body:**

```json
[
  {
    "id": "travel_id",
    "title": "Travel Title",
    "description": "Travel Description",
    "type": "public/private",
    "places": [...],
    "routes": [...],
    "updated_at": "YYYY-MM-DD HH:MM:SS",
    "created_at": "YYYY-MM-DD HH:MM:SS"
  },
  ...
]
```

#### Get Travels by User ID

Retrieve a list of travels belonging to a specific user.

- **URL:** `/travel_service/user/travels/`
- **Method:** `GET`
- **Headers:**
  - `Userid`: User's ID

##### Response

- **Status Code:** 200 (OK)
- **Response Body:**

```json
[
  {
    "id": "travel_id",
    "title": "Travel Title",
    "description": "Travel Description",
    "type": "public/private",
    "places": [...],
    "routes": [...],
    "updated_at": "YYYY-MM-DD HH:MM:SS",
    "created_at": "YYYY-MM-DD HH:MM:SS"
  },
  ...
]
```

#### Get Travel by Travel ID
Get Travel by Travel ID (with places and routes).

- **URL:** `/travel_service/travel/<travel_id>`
- **Method:** `GET`
- **URL Parameters:**
  - `travel_id`: ID of the travel to retrieve
- **Headers:**
  - `None`: not required
- **Request Body:**
  - `None`: not required

##### Response
```json 
{
    "created_at": "2023-09-02 22:59:35",
    "description": "Some description",
    "id": "64f3942757dd4a6c552f4515",
    "places": [
        {
            "place_id": "1"
        },
        {
            "place_id": "2"
        }
    ],
    "routes": [
        {
            "route_id": "1"
        },
        {
            "route_id": "1"
        }
    ],
    "title": "Travel 3",
    "type": "private",
    "updated_at": "2023-09-02 22:59:35"
}
```


#### Add Travel

Add a new travel with places and routes.

- **URL:** `/travel_service/travel`
- **Method:** `POST`
- **Headers:**
  - `Userid`: User's ID
- **Request Body:**

```json
{
  "title": "Travel Title",
  "description": "Travel Description",
  "type": "public/private",
  "places": [...],
  "routes": [...]
}
```

##### Response

- **Status Code:** 200 (OK)
- **Response Body:**

```json
{
  "message": "Travel added successfully"
}
```

#### Edit Travel

Edit an existing travel's information.

- **URL:** `/travel_service/travel/<travel_id>`
- **Method:** `PUT`
- **URL Parameters:**
  - `travel_id`: ID of the travel to edit
- **Headers:**
  - `Userid`: User's ID
- **Request Body:**

```json
{
  "title": "New Title",
  "description": "New Description",
  "type": "public/private"
}
```

##### Response

- **Status Code:** 200 (OK)
- **Response Body:**

```json
{
  "message": "Travel updated successfully"
}
```

#### Delete Travel

Delete a travel along with its places and routes.

- **URL:** `/travel_service/travel/<travel_id>`
- **Method:** `DELETE`
- **URL Parameters:**
  - `travel_id`: ID of the travel to delete
- **Headers:**
  - `Userid`: User's ID

##### Response

- **Status Code:** 200 (OK)
- **Response Body:**

```json
{
  "message": "Travel deleted successfully"
}
```

#### Add Comment

Add a new comment to a travel.

- **URL:** `/travel_service/travel/<travel_id>/comments`
- **Method:** `POST`
- **URL Parameters:**
  - `travel_id`: ID of the travel to comment on
- **Headers:**
  - `Userid`: User's ID
- **Request Body:**

```json
{
  "text": "Comment Text"
}
```

##### Response

- **Status Code:** 200 (OK)
- **Response Body:**

```json
{
  "message": "Comment added successfully"
}
```

#### Add Like

Add a like to a travel.

- **URL:** `/travel_service/like`
- **Method:** `POST`
- **Headers:**
  - `Userid`: User's ID
- **Request Body:**

```json
{
  "travel_id": "travel_id"
}
```

##### Response

- **Status Code:** 200 (OK)
- **Response Body:**

```json
{
  "message": "Like added successfully"
}
```

#### Delete Like

Delete a like from a travel.

- **URL:** `/travel_service/like/<like_id>`
- **Method:** `DELETE`
- **URL Parameters:**
  - `like_id`: ID of the like to delete
- **Headers:**
  - `Userid`: User's ID

##### Response

- **Status Code:** 200 (OK)
- **Response Body:**

```json
{
  "message": "Like deleted successfully"
}
```

---

This API documentation provides an overview of the available endpoints and their functionalities. For detailed usage and further information, refer to the code implementation and accompanying comments.
