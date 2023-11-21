using System;
using UnityEngine;

public class NpcCollision : MonoBehaviour
{
    /*
     * This script is used to detect whether the player is in proximity of an NPC or not.
     * The script passes whether the player is within range of an NPC, and which NPC the player is in range of.
     */
    private bool _isPlayerInProximity;   // Whether the player is in proximity or not
    private GameObject _currentNpc; // The current NPC that the player has collided with

    public event Action playerEnteredNpcRange; //Event that is invoked when the player enters the proximity of an NPC (Used to enable the UI in the DialoguePanelSpawner script)
    public event Action playerExitedNpcRange;
    
    
    public bool IsPlayerInProximity()
    {
        return _isPlayerInProximity;
    }
    public GameObject GetCurrentNpc()
    {
        return _currentNpc;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            // Player has entered the proximity
            Debug.Log($"Player is now in proximity: {other.gameObject.name}");
            _isPlayerInProximity = true;
            _currentNpc = other.gameObject;
            
            playerEnteredNpcRange?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            // Player has entered the proximity
            

            //RaycastCollision();
            
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            // Player has exited the proximity
            Debug.Log($"Player exited the proximity: {other.gameObject.name}");
            _isPlayerInProximity = false;
            //_currentNpc = null; <- Not sure if we should set this, it's easier if we don't but unsure if it'll cause bugs
            
            playerExitedNpcRange?.Invoke();
            
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
