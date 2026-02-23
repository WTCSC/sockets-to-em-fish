import socket
import threading

#User needs to input the port to bind to
serverPort = int(input("Enter port: "))

def server():
    socketed_server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    socketed_server.bind(("0.0.0.0", serverPort))
    socketed_server.listen()
    print(f"Server listening for fish on port {serverPort}")
    while True:
        conn, addr = socketed_server.accept()

        thread = threading.Thread(target=client_handler, args=(conn, addr))
        thread.start()
        print(f'Active fish: {threading.active_count() - 1}')

def client_handler(conn, addr):
    print(f"Fish {addr[0]} has connected on port {addr[1]}.")
    while True:
        try:
            msg = conn.recv(1024)
            if not msg:
                break
            print(f"Fish {addr[0]} has sent {msg.decode()}")
            conn.sendall(f"{addr[0]}: {msg.decode()}".encode())
        except ConnectionResetError:
            print(f"Fish {addr[0]} has disconnected")
            break
    conn.close()
    print(f"Connection to {addr[0]} closed.")



server()