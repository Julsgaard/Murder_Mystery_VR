using UnityEngine;

public class GameManager : MonoBehaviour
{
    // array of GameObjects that belong to the player
    public GameObject vrPlayerObject;
    public GameObject nonPlayerObject;

    public bool enableVR;// Whether VR is enabled or not}
    
    public string[] availableMicrophones;
    public string selectedMicrophone;

    
    
    // Start is called before the first frame update
    void Awake()
    {
        
        availableMicrophones = Microphone.devices; // Populate availableMicrophones with the names of all available microphones
        
        selectedMicrophone = Microphone.devices[2]; // Select the first microphone in the list of available microphones
        
        EnableVR();

        CheckForVrOrNonVrPlayer();

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
    
    private void CheckForVrOrNonVrPlayer()
    {
        if (enableVR)
        {
            GameObject playerObject = vrPlayerObject;
        }
        else
        {
            GameObject playerObject = nonPlayerObject;
        }
    }
}



