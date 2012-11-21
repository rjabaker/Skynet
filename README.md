## Source for the Arduino Part of the project

## Checkout
```sh
Needs to be updated!
```
## Build Instructions

<b> RBaker: November 18, 2012 </b>

The kinect code only runs with the Arduino board connected to COM5 (though this can be adjusted within the code in the WorkBench testing project) and the arduino code uploaded to the board. Individually build the ArduinoUtilities and Skynet projects. Then, build and debug the WorkBench project. The WorkBench project will open a form in which there is a button and a text box. Clicking the button will turn on an LED (at pin 13) on the Arduino board. The text box will be populated with the on/off status of the pin, which is recieved from the board to simulate feedback.

