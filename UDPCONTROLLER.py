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
            value = int(data.decode('utf-8'))

            # Print the received value
            print(f"Received value: {value}")

            # Perform actions based on the received value
            if 1 <= value <= 9:
                # Simulate pressing a key corresponding to the received value
                key_to_press = str(value)
                keyboard.press(key_to_press)
                # Keep the key pressed for 350 milliseconds
                time.sleep(0.19)

                # Release the key
                keyboard.release(key_to_press)
                print(f"Pressed key: {key_to_press}")

    except KeyboardInterrupt:
        print("Stopping the UDP listener.")

if __name__ == "__main__":
    main()