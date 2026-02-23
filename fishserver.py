import socket

#Create a socket
server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

#User needs to input the port to bind to
serverPort = int(input("Enter port: "))

server.bind(("0.0.0.0", serverPort))

server.listen(1)
print(f"Listening for fish on port {serverPort}")

client, addr = server.accept()
print(f"Fish {addr[0]} connected on port {addr[1]}")

while True:
    msg = client.recv(2048).decode()
    if not msg:
        break
    print(f"Received {msg}")
    client.send(msg.encode())


client.close()
server.close()