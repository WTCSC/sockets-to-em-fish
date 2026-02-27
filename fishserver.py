import socket
import threading

serverPort = int(input("Enter port: "))

clients = set()
usernames = {}
clients_lock = threading.Lock()

def server():
    socketed_server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    #Bind to any address on the port the user specifies
    socketed_server.bind(("0.0.0.0", serverPort))
    socketed_server.listen()
    print(f"Server listening for fish on port {serverPort}")
    while True:
        conn, addr = socketed_server.accept()

        #Start a thread for each connecting client
        thread = threading.Thread(target=client_handler, args=(conn, addr))
        thread.start()
        print(f'Active fish: {threading.active_count() - 1}')

def client_handler(conn, addr):
    print(f"Fish {addr[0]} has connected on port {addr[1]}.")
    with clients_lock:
        #add the connecting client to the list of currently connected clients
        clients.add(conn)
    while True:
        try:
            msg = conn.recv(1024)
            if not msg:
                break
            if msg.decode().startswith("..$CLNTMSG USERNAME:"):
                user_name = msg.decode().replace("..$CLNTMSG USERNAME: ", "")
                usernames[f"{addr[0]}:{addr[1]}"] = user_name
            else:
                print(f"Fish {usernames[f"{addr[0]}:{addr[1]}"]} has sent {msg.decode()}")
                with clients_lock:
                    for c in clients:
                        if c != conn:
                            c.sendall(f"{usernames[f"{addr[0]}:{addr[1]}"]}: {msg.decode()}".encode())
        #If a client disconnects, we want the client's thread to end automatically
        except ConnectionResetError:
            print(f"Fish {addr[0]} has disconnected")
            with clients_lock:
                #remove the now disconnected client from the list of currently connected clients
                clients.remove(conn)
            break
    conn.close()
    print(f"Connection to {addr[0]} closed.")


#actually start the server
server()
