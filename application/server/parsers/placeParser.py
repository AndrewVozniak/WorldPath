import requests
import pyodbc


conn = pyodbc.connect(
    'DRIVER={ODBC Driver 17 for SQL Server};'
    'SERVER=DESKTOP-FG20699\\SQLEXPRESS;'
    'DATABASE=Places;'
    'Trusted_Connection=yes;'
)

cursor = conn.cursor()

cursor.execute('''
    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tourist_places]') AND type in (N'U'))
    BEGIN
    CREATE TABLE [dbo].[tourist_places] (
    id INT PRIMARY KEY IDENTITY,
    name NVARCHAR(255),
    amenity NVARCHAR(50),
    lat FLOAT,
    lon FLOAT
    )
    END
''')
conn.commit()

overpass_url = 'https://overpass-api.de/api/interpreter'
query = (
    "[out:json];"
    "node[amenity=restaurant];"
    "node[amenity=hotel];"
    "node[amenity=museum];"
    "(._;>;);"
    "out;"
)

response = requests.post(overpass_url, data=query)

try:
    data = response.json()
    # Тут ви можете продовжити обробку даних JSON
    print(data)
except requests.exceptions.JSONDecodeError as e:
    print("Response is not a valid JSON:", e)
    print("Response text:", response.text)

# Фільтрація точок на боці Python
filtered_elements = [
    element for element in data['elements']
    if 'amenity' in element['tags'] and element['tags']['amenity'] in ['restaurant', 'hotel', 'museum']
]

# Цикл обробки і вставки даних у базу даних
for element in filtered_elements:
    name = element.get('tags', {}).get('name', 'Unknown')
    amenity = element.get('tags', {}).get('amenity', 'Unknown')
    lat = element.get('lat', 0)
    lon = element.get('lon', 0)

    cursor.execute('INSERT INTO tourist_places (name, amenity, lat, lon) VALUES (?, ?, ?, ?)', (name, amenity, lat, lon))
    conn.commit()

conn.close()

print('Data has been successfully saved to the MSSQL database.')