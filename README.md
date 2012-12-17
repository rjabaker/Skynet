## Skynet

Skynet's final objective is to use Microsoft Kinect to control a robotic hand. Use this project in conjunction with rjabaker/arduino, which reads data from the serial port to control output pins on an Arduino board.

#### Design

Skynet is composed of three major components:

- KinectUtilities: This project interfaces with Microsoft Kinect. It reads Skelton frames from the sensor, mining joint information from the provided .NET objects. These frames are also studied for gestures. Events are fired in response to captured gestures/tracked joints.
- ArduinoUtilities: This project sends and recieves byte packages from the serial port, communicating with an Arduino.
- Skynet: Acting as the middle-man between the KinectUtilities and ArduinoUtilities projects, Skynet recieves gesture/joint information for the sensor, determines the appropriate pin commands that need to occur, and fires byte packages to an Arduino via ArduinoUtilities.

#### Current State

Currently, this project tracks Skeleton joint positions and gestures. In response to this information, it writes corresponding pin commands into the serial port for an Arduino board to respond to.

