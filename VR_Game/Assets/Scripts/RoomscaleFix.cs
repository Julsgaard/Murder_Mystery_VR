using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(XROrigin))]
public class RoomscaleFix : MonoBehaviour
{
    private CharacterController _character;
    private XROrigin _xrOrigin;
    public Transform itemSocketBelt;
    
    public GameObject HMD;
    private Vector3 _currentHMDlocalPosition;
    private Quaternion _currentHMDRotation;
    [Range(0.01f,1f)]
    public float heightRatio;
    void Start()
    {
        _character = GetComponent<CharacterController>();
        _xrOrigin = GetComponent<XROrigin>();

    }

    private void Update()
    {
        _currentHMDlocalPosition = HMD.transform.localPosition;
        _currentHMDRotation = HMD.transform.rotation;
        UpdateSocketInventory();
    }
    
    private void UpdateSocketInventory()
    {
        itemSocketBelt.transform.localPosition = new Vector3(_currentHMDlocalPosition.x, 0, _currentHMDlocalPosition.z);
        itemSocketBelt.transform.rotation = new Quaternion(transform.rotation.x, _currentHMDRotation.y, transform.rotation.z, _currentHMDRotation.w);
        
        itemSocketBelt.transform.localPosition = new Vector3(itemSocketBelt.transform.localPosition.x,(_currentHMDlocalPosition.y * heightRatio), itemSocketBelt.transform.transform.localPosition.z);
    }


    private void FixedUpdate()
    {
        _character.height = _xrOrigin.CameraInOriginSpaceHeight + 0.15f;

        var centerPoint = transform.InverseTransformPoint(_xrOrigin.Camera.transform.position);
        _character.center = new Vector3(centerPoint.x, _character.height / 2 + _character.skinWidth, centerPoint.z);

        _character.Move(new Vector3(0.001f, -0.001f, 0.001f));
        _character.Move(new Vector3(-0.001f, -0.001f, -0.001f));
        
        //Stuff to manage the placement of the ItemSocketBelt
        // Get the camera's Y-axis rotation and apply it to the belt's rotation.
        //Vector3 eulerAngles = itemSocketBelt.rotation.eulerAngles;
        //eulerAngles.y = _xrOrigin.Camera.transform.eulerAngles.y;
        //itemSocketBelt.rotation = Quaternion.Euler(eulerAngles);
        
        
        

    }
}
