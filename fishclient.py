import socket
import random
import threading

# Create a socket object
client = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

defaultNames = ["TheFishMaster", "SomeFish", "RandomFish", "FishTheFourth", "TheFishinator", "Fysch", "Feesh",'bass (pronounced base)', 'bass (pronounced baas)', 'albacore','coelacanth','geoduck','golden tilefish','cancer borealis (crab species)','Lingcod','Northern Anchovy']
def fishclient():
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

    output_thread = threading.Thread(target=reciever)
    input_thread = threading.Thread(target=sender)
    output_thread.start()
    input_thread.start()
    
# Send messages and receive responses
def reciever():
    while True:
        response = client.recv(1024).decode()
        print(f"{response}")


def sender():
    while True:
        msg = input("\n")
        if not msg:
            break
        client.send(msg.encode())
    client.close()

fishclient()
