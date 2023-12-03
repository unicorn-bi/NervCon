import socket
import keyboard 
import time

def main():
    # Set up UDP socket
    udp_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    udp_socket.bind(('127.0.0.1', 5000))

    try:
        while True:
            # Receive data from the UDP port
            data, addr = udp_socket.recvfrom(1024)
            value = data.decode('utf-8')

            # Print the received value
            print(f"Received value: {value}")

            # Perform actions based on the received value
            if value != "":
                # Simulate pressing a key corresponding to the received value
                keyboard.press(value)
                # Keep the key pressed for 350 milliseconds
                time.sleep(0.3)

                # Release the key
                keyboard.release(value)
                print(f"Pressed key: {value}")

    except KeyboardInterrupt:
        print("Stopping the UDP listener.")

if __name__ == "__main__":
    main()