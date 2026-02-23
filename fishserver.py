import socket

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

serverport = int(input("Enter port: "))

server.bind(("0.0.0.0", serverport))

server.listen(3)
print(f"Listening for fish on port {serverport}")

client, addr = server.accept()
print(f"Client {addr} connected")

while True:
    msg = client.recv(2048)
    if not msg:
        break
    print(f"Received {msg}")
    client.send(f"{msg}".encode())


client.close()
server.close()