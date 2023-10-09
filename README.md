# Revolving Games assignment

This assignment involves setting up a gRPC communication system where clients send requests with random numbers, the server checks if these numbers are prime, and both sides monitor various aspects of the communication, including response verification and RTT measurement. The server also keeps track of prime numbers and displays statistics. Finally, there's a bonus part involving writing a Makefile for building and testing the system.

# gRPC Server
Following are the key functions and responsibilities of the server in the described gRPC system:

### Message Reception:

The server must be able to receive incoming messages from multiple clients.
### Request Handling:

For each incoming request, the server must process the contained random number.
### Prime Number Verification:

Verify whether the received number is prime or not.
### Response Generation:

Generate a response based on the result of the prime number verification.
### Response Sending:

Send the response back to the respective client.
### Statistics Tracking:

Maintain a record of all valid prime numbers that have been requested by clients.
### Top Prime Numbers Display:

Periodically (every second), display the top 10 highest requested and validated prime numbers.
### Total Messages Count:

Keep track of the total number of messages received from clients.
### Concurrent Handling:

Handle multiple client requests concurrently to ensure responsiveness.
### Synchronization:

Use appropriate synchronization mechanisms to ensure thread safety when updating statistics and processing requests from multiple clients.
### Logging:

Log relevant information, such as received requests, response times, and server statistics, for monitoring and debugging purposes.

### Makefile Support:

Implement support for building and running the server as part of the bonus, which includes writing a Makefile for automation.

# gRPC Client
Following are the key functions and responsibilities of the client in the described gRPC system:

Sending a gRPC Request:

Generate random numbers between 1 and 1000 to include in each request, and send requests with random numbers to the server for prime number verification.
### Response Handling:

Receive and process responses from the server for each request.

### Round Trip Time (RTT) Measurement:

Measure the Round Trip Time (RTT) for each sent message, i.e., the time taken for a request to reach the server and for the response to return to the client.
### Logging and Monitoring:

Log RTT values and other relevant information for monitoring and analysis.

# How to run
You just have to clone this repository from 'master'. It already contains Makefile in the base directory of Server and Client.
To run the gRPC Server and Client and view the results, run the following command via CMD
```bash
make run-all
```
It will build and execute both Server and Client in separate terminals.
If you want to run the client again, run the following command.
```bash
make build-client
make run-client
```
