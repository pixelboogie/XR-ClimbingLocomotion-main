# XR-ClimbingLocomotion-main

## Description
**XR-ClimbingLocomotion-main** is a Unity project that implements a climbing system for virtual reality (VR) using the XR Toolkit. The project demonstrates how to create a custom interactable locomotion provider that enables climbing in a VR environment. It also showcases how to capture controller velocity via the input system to enhance the climbing experience.

This project is based on the video tutorial "XR Toolkit Intermediate Programming - Climbing" by Andrew, which can be found here: [Climbing](https://www.youtube.com/watch?v=qKDzZd0sjxQ).

## Features
- **Custom Climbing System**: Implements a climbing mechanic using a custom locomotion provider.
- **Controller Velocity Tracking**: Utilizes controller velocity to determine the player's movement during climbing.
- **Gravity Management**: Handles gravity toggling to ensure a smooth climbing and falling experience.
- **Seamless Locomotion Integration**: Works well with other XR Toolkit locomotion providers, allowing for a cohesive movement experience.

## Installation
To set up this project locally, follow these steps:

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/your-username/XR-ClimbingLocomotion-main.git

   cd XR-ClimbingLocomotion-main

2. **Open in Unity:**

    - Open the project folder in Unity.
    - Ensure that you have the required version of Unity and the XR Interaction Toolkit installed.

3. **Configure the Scene:**

    - Set up your XR Origin with the appropriate input and locomotion settings.
    - Add the provided climbing scripts to the scene as instructed in the video tutorial.

4. **Set Up Input Actions:**

    - Open the Input Actions asset and configure the velocity inputs for both left and right hand controllers.

5. **Test the Climbing System:**

    - Enter Play mode in Unity and use your VR controllers to interact with the ladder or climbing objects to test the climbing functionality.

## Example Code

Here is an example of how the climbing provider works:

    public class ClimbingProvider : LocomotionProvider {
    private CharacterController characterController;
    private bool isClimbing = false;
    private List<VelocityContainer> activeVelocities = new List<VelocityContainer>();

    private void Update() {
        TryBeginClimb();
        if (isClimbing) {
            ApplyVelocity();
        }
        TryEndClimb();
    }

    private void ApplyVelocity() {
        Vector3 velocity = CollectControllerVelocity();
        if (characterController != null) {
            characterController.Move(-velocity * Time.deltaTime);
        }
    }

    private Vector3 CollectControllerVelocity() {
        Vector3 totalVelocity = Vector3.zero;
        foreach (var container in activeVelocities) {
            totalVelocity += container.Velocity;
        }
        return totalVelocity;
    }

This script handles the core functionality of climbing, including starting and stopping the climbing action, and applying the appropriate movement based on the controller's velocity.

## License
This project is licensed under the MIT License. See the LICENSE file for more information.

