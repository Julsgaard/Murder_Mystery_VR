using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public enum HandType {
    Left,
    Right
}

public class HandAnimationController : MonoBehaviour
{
    public HandType handType;

    private Animator a;
    private InputDevice inputDevice;

    private float pinchValue;
    private float fistValue;
    private float defaultHandValue;

    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<Animator> ();
        inputDevice = GetInputDevice();
    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();
    }

    InputDevice GetInputDevice ()
    {
        InputDeviceCharacteristics controllerCharacteristic = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller;

        if (handType == HandType.Left) 
        {
            controllerCharacteristic |= InputDeviceCharacteristics.Left;
        } else if (handType == HandType.Right)
        {
            controllerCharacteristic |= InputDeviceCharacteristics.Right;
        }

        List<InputDevice> inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristic, inputDevices);

        return inputDevices[0];
    }

    void AnimateHand()
    {
        inputDevice.TryGetFeatureValue(CommonUsages.trigger, out pinchValue);
        inputDevice.TryGetFeatureValue(CommonUsages.grip, out fistValue);

        a.SetFloat("Trigger", pinchValue);
        a.SetFloat("Grip", fistValue);
    }
}
