import socket

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

serverPort = int(input("Enter port: "))

server.bind(("0.0.0.0", serverPort))

server.listen(3)
print(f"Listening for fish on port {serverPort}")

client, addr = server.accept()
print(f"Fish {addr[0]} connected on port {addr[1]}")

while True:
    msg = client.recv(2048)
    if not msg:
        break
    print(f"Received {msg}")
    client.send(f"{msg}".encode())


client.close()
server.close()