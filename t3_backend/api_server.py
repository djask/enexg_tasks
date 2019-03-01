from flask import Flask, jsonify, request, redirect, flash
import datetime
import time
import json
import os.path

app = Flask(__name__)
app.secret_key = b'a secret'

help_message = """
API Usage:

GET /api/help/ (prints help message)
GET /api/get_last (gets the last word stored)\n
PUT /api/update/ (updates the string with the request data)\n

"""

#define dev choice of blacklisted word
BLACKLIST_WORD = "python"

#file which stores previous message
MSG_FILE = "msg.txt"


#default help message
@app.route('/api/',methods=['GET'])
@app.route('/api/help/',methods=['GET'])
def help():
	return help_message

#string getter
@app.route('/api/get_string/', methods=['GET'])
def get_last():

	#check if the msg exists
	if not os.path.isfile(MSG_FILE):
		return "No previous message stored, have you tried storing a message?"
	
	#opens and prints the message details to the user
	with open('msg.txt') as msg_info:
		data = json.load(msg_info)
		return jsonify(data)

#string updater, the new string is specified in data
@app.route('/api/update_string/', methods=['PUT'])
def update_string():
	msg = request.data.decode()
	
	#encountered blacklist word
	if BLACKLIST_WORD in msg:
		
		#briefly flash error message, then redirect to picsum
		flash(BLACKLIST_WORD + ' is not allowed in input string')
		return redirect('https://picsum.photos/200/300')
	
	#valid messages are stored in the file
	data = {
		"message" : msg,
		"timestamp" : datetime.datetime.fromtimestamp(time.time()).strftime('%Y-%m-%d %H:%M:%S')
	}
	with open(MSG_FILE,'w+') as msg_info:
		json.dump(data, msg_info)
	return jsonify(data)

if __name__ == '__main__':
    app.run()