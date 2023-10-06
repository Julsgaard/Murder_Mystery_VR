using UnityEngine;

public class GameManager : MonoBehaviour
{
    // array of GameObjects that belong to the player
    public GameObject vrPlayerObject;
    public GameObject playerObject;
    
    public bool enableVR;
    
    // Start is called before the first frame update
    void Start()
    {
        // if enableVR is true, then enable the vrPlayerObject and disable the playerObject
        if (enableVR)
        {
            vrPlayerObject.SetActive(true);
            playerObject.SetActive(false);
        }
        // if enableVR is false, then disable the vrPlayerObject and enable the playerObject
        else
        {
            vrPlayerObject.SetActive(false);
            playerObject.SetActive(true);
        }
    }
}
