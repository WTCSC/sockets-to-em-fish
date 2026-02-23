import socket

# Create a socket object
client = socket.socket(socket.AF_INET, socket.SOCK_STREAM)


serverAddr = input("Input the IP of the server: ")
serverPort = int(input("Input the port of the server: "))

# Connect to server (replace with the server machine’s IP if needed)
client.connect((serverAddr, serverPort))
print("Connected to server")

# Send messages and receive responses
while True:
    msg = input("Enter message: ")
    if not msg:
        break
    client.send(msg.encode())
    response = client.recv(1024).decode()
    print(f"{response}")

client.close()
