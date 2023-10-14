using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class NpcCollision : MonoBehaviour
{
    private bool _isPlayerInProximity;   // Whether the player is in proximity or not
    
    // Returns whether the player is in proximity or not
    public bool IsPlayerInProximity()
    {
        return _isPlayerInProximity;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            // Player has entered the proximity
            Debug.Log("Player is in proximity!");
            _isPlayerInProximity = true;


            //other.gameObject.GetComponent<NpcPersonality>();


            //RaycastCollision();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            // Player has exited the proximity
            Debug.Log("Player exited the proximity!");
            _isPlayerInProximity = false;
            
            
        }
    }

    private void RaycastCollision()
    {
        //*********** CAN BE USED TO GET THE NPC THAT THE PLAYER IS LOOKING AT ***********
        // Define a ray that starts from the player's position and goes forward
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~LayerMask.GetMask("Ignore Raycast"),
                QueryTriggerInteraction.Ignore))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);

            // Check if the ray hits an object with the "NPC" tag
            if (hit.collider.CompareTag("NPC"))
            {
                Debug.Log("Hit an NPC: " + hit.collider.name);
                // You can access the specific NPC that was hit using hit.collider
                // Example: NPC npc = hit.collider.GetComponent<NPC>();
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }


    
}
