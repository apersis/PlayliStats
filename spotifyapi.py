# Python Script for call on spotify API

import requests

import json

token = 'Bearer BQCDG-gnkIh72NEHzMsmR6rws5pJKTme3KFI8DeCi6UZXborE9Ekml2umzGr7MazqSR9XGQiZNG3PYNsGJm4mR9Qag2EGAXeQYJeAQqjrZqOA5CLSzs'

url = 'https://api.spotify.com/v1/playlists/39KIbOWUS96nH8UMOAjMiV/tracks'

headers = {
    'Authorization': token,
}

params = {
    'market': 'FR',
    'limit': '100',
    'offset': '0',
}

for i in range(2):
    offset = 100 * i
    params = {
        'market': 'FR',
        'limit': '100',
        'offset': offset,
    }

    # A GET request to the API
    response = requests.get(url, params=params, headers=headers)

    
    with open('data.json', 'a') as f:
        json.dump(response.json(), f)

