using UnityEngine;

public class ProximityDetector : MonoBehaviour
{
    /*
    public float proximityRadius = 5f;  // The proximity radius for detection
    private bool isPlayerInProximity;   // Whether the player is in proximity or not

    private void Start()
    {
        // Create a SphereCollider component
        SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();

        // Set the collider as a trigger
        sphereCollider.isTrigger = true;

        // Set the radius of the sphere collider
        sphereCollider.radius = proximityRadius;
    }

    // This function is called whenever another collider enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player has entered the proximity
            Debug.Log("Player entered the proximity!");
            isPlayerInProximity = true;
        }
    }

    // This function is called whenever a collider exits the trigger collider
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player has exited the proximity
            Debug.Log("Player exited the proximity!");
            isPlayerInProximity = false;
        }
    }

    public bool IsPlayerInProximity()
    {
        return isPlayerInProximity;
    }*/
}