# Task Three - Backend
A RESTful api for sending and receiving messages
## Usage and Setup Instructions
### Dependencies
* Python flask
* datetime
* time
* json
* os
### Running the server
The server starts on port 5000
```
$ python api_server.py
 * Running on http://127.0.0.1:5000/ (Press CTRL+C to quit)
```

### Using the API
API has only two main functionalities  

**GET /api/help/ (prints help message)**  
simply access the link with a get request to get a help message

**GET /api/get_last (gets the last word stored)**   
send a http GET request to this path to get the current json string

**PUT /api/update/ (updates the string with the request data)**  
send a http PUT request to update the string  
string should be encoded in plain text and sent as request data


#### Example usage below with CURL
Input with a blacklisted word. You will be redirected to picsum photos
```
$ curl -XPUT -H "Content-Type: text/plain" http://localhost:5000/api/update_string/ -d 'i like python'
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 3.2 Final//EN">
<title>Redirecting...</title>
<h1>Redirecting...</h1>
<p>You should be redirected automatically to target URL: <a href="https://picsum.photos/200/300">https://picsum.photos/200/300</a>.  If not click the link.
```

Input with a normal text string, it will be stored successfully
```
$ curl -XPUT -H "Content-Type: text/plain" http://localhost:5000/api/update_string/ -d 'a sample message'
{
  "message": "a sample message",
  "timestamp": "2019-03-01 13:48:29"
}
```

Request the last stored string
```
$ curl -XGET http://localhost:5000/api/get_string/
{
  "message": "a sample message",
  "timestamp": "2019-03-01 13:48:29"
}
```

