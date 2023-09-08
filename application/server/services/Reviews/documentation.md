# GET ALL REVIEWS
## Description
Get all reviews from the database, only reviews fields

### Request GET `/reviews`
### Response
```json
[
  {
    "id": "64fb627e5315f4ff0bd67e03",
    "user_id": "64f871beb841ddee0043bbb8",
    "rating": 5,
    "text": "Never thought I'd find a service this intuitive! WorldPath made my road trip to Lapland so much smoother. The real-time weather updates were a lifesaver!",
    "updated_at": "2023-09-08 21:23:12",
    "created_at": "2023-09-08 21:12:37"
  },
  {
    "id": "64fb6415c847fff67511de7f",
    "user_id": "64f871beb841ddee0043bbb8",
    "rating": 5,
    "text": "Never thought I'd find a service this intuitive! WorldPath made my road trip to Lapland so much smoother. The real-time weather updates were a lifesaver!",
    "updated_at": "2023-09-08 21:23:12",
    "created_at": "2023-09-08 21:12:37"
  }
]
```

# GET REVIEW BY COUNT 
## Description
Get all reviews from the database, all fields with user info but only base info

### Request GET `/reviews/<count>`
### Response
```json
[
    {
        "created_at": "2023-09-08 21:23:48",
        "id": "64fb66b4d6d078ee4bc39e8d",
        "rating": 5.0,
        "text": "Never thought I'd find a service this intuitive! WorldPath made my road trip to Lapland so much smoother. The real-time weather updates were a lifesaver!",
        "updated_at": "2023-09-08 21:23:48",
        "user": {
            "error": "User not found"
        }
    },
    {
        "created_at": "2023-09-08 21:23:46",
        "id": "64fb66b2d6d078ee4bc39e8c",
        "rating": 5.0,
        "text": "Never thought I'd find a service this intuitive! WorldPath made my road trip to Lapland so much smoother. The real-time weather updates were a lifesaver!",
        "updated_at": "2023-09-08 21:23:46",
        "user": {
            "name": "Andrew Vozniak",
            "profile_photo_path": "https://ui-avatars.com/api/?name=Andrew Vozniak&background=f4f4f4&color=253158&size=128&bold=true"
        }
    }
]
```

# UPDATE REVIEW
## Description
Update review by id

### Request PUT `/reviews/<id>`
### Headers: Userid
### Body
```json
{
  "rating": 5,
  "text": "Never thought I'd find a service this intuitive! WorldPath made my road trip to Lapland so much smoother. The real-time weather updates were a lifesaver!"
}
```

### Response
```json
[
  {
    "message": "Review updated successfully"
  }
]
```