import socket
import random

# Create a socket object
client = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

defaultNames = ["TheFishMaster", "SomeFish", "RandomFish", "FishTheFourth", "TheFishinator", "Fysch", "Feesh"]

serverAddr = input("Input the IP of the server: ")
try:
    serverPort = int(input("Input the port of the server: "))
except ValueError:
    print('hey buddy thats not a port')
    quit()
userName = input("Please input your username: ")
if userName == "":
    userName = random.choice(defaultNames)

# Connect to server (replace with the server machine’s IP if needed)
try:
    client.connect((serverAddr, serverPort))
except ConnectionRefusedError:
    print('Error: Connection Refused')
    quit()
print("Connected to server")
client.send(f"..$CLNTMSG USERNAME: {userName}".encode())

# Send messages and receive responses
while True:
    msg = input("Enter message: ")
    if not msg:
        break
    client.send(msg.encode())
    response = client.recv(1024).decode()
    print(f"{response}")

client.close()
