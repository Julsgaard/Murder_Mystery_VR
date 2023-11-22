using UnityEngine;

public class GameManager : MonoBehaviour
{
    // array of GameObjects that belong to the player
    public GameObject vrPlayerObject;
    public GameObject nonPlayerObject;

    public bool enableVR;// Whether VR is enabled or not}

    public bool enableDialogueOptions; // Whether dialogue options are enabled or not

    public string[] availableMicrophones;
    public string selectedMicrophone;

    
    
    // Start is called before the first frame update
    void Awake()
    {
        SelectMicrophone();
        
        EnableVR();

        CheckForVrOrNonVrPlayer();
    }

    private void SelectMicrophone()
    {
        availableMicrophones = Microphone.devices; // Populate availableMicrophones with the names of all available microphones
        
        // If VR is enabled and dialogue options are disabled, then select the Quest 2 Microphone
        if (enableVR && !enableDialogueOptions)
        {
            selectedMicrophone = Microphone.devices[0]; // Select the first microphone in the list of available microphones
            return;
            
            
            // This code selects the Oculus microphone, but the oculus microphone creates a lag spike when starting and ending recording (might need it later)
            string oculusMic = "Headset Microphone (Oculus Virtual Audio Device)";
            bool oculusMicFound = false;

            // Check if Oculus microphone is available
            foreach (var mic in availableMicrophones)
            {
                if (mic == oculusMic)
                {
                    selectedMicrophone = oculusMic;
                    oculusMicFound = true;
                    break;
                }
            }
            
            // If Oculus microphone is not found, use the default microphone
            if (!oculusMicFound)
            {
                Debug.LogWarning("No Quest 2 microphone found. Using default microphone instead.");
                selectedMicrophone = Microphone.devices[0]; // Select the first microphone in the list of available microphones
            }
        }
        else
        {
            selectedMicrophone = Microphone.devices[0]; // Select the first microphone in the list of available microphones
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



