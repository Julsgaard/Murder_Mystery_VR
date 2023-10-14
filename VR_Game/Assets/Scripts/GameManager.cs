using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    // array of GameObjects that belong to the player
    public GameObject vrPlayerObject;
    public GameObject nonPlayerObject;
    
    public bool enableVR;
    
    
    // Start is called before the first frame update
    void Start()
    {
        EnableVR();
        
        if (enableVR)
        {
            GameObject playerObject = vrPlayerObject;
        }
        else
        {
            GameObject playerObject = nonPlayerObject;
        }
        
    }

    private void EnableVR()
    {
        // if enableVR is true, then enable the vrPlayerObject and disable the playerObject
        if (enableVR)
        {
            vrPlayerObject.SetActive(true);
            nonPlayerObject.SetActive(false);
        }
        // if enableVR is false, then disable the vrPlayerObject and enable the playerObject
        else
        {
            vrPlayerObject.SetActive(false);
            nonPlayerObject.SetActive(true);
        }
    }
    
    
}
