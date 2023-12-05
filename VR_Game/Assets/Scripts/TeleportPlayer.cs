using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    
    // Teleports the player when they touch the button
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player has touched the button.");
            gameManager.TeleportPlayer();
        }
    }
}
