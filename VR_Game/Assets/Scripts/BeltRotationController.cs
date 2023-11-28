using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltRotationController : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    Vector3 cameraRotation, beltRotation;

    private void Start() { mainCamera = Camera.main; }

    void Update()
    {
        cameraRotation = mainCamera.transform.rotation.eulerAngles;

        if (cameraRotation.x > 50 && cameraRotation.x < 90) { return; }
            

        beltRotation = transform.rotation.eulerAngles;
        beltRotation.y = cameraRotation.y;
        transform.rotation = Quaternion.Euler(beltRotation);
    }
}
