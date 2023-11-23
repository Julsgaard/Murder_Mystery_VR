using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScreenFade screenFade;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Camera cameraVR;
    [SerializeField] private Camera cameraNon;
    
    // array of GameObjects that belong to the player
    public GameObject vrPlayerObject;
    public GameObject nonPlayerObject;

    public bool enableVR;// Whether VR is enabled or not}

    public bool enableDialogueOptions; // Whether dialogue options are enabled or not

    public string[] availableMicrophones;
    public string selectedMicrophone;

    //public List<Clues> clues = new List<Clues>();

    
    // Start is called before the first frame update
    void Awake()
    {
        SelectMicrophone();
        
        EnableVR();

        //CheckForVrOrNonVrPlayer();
    }
    
    void Start()
    {
        //IntroScreen();

        //string endText = WinOrLose("Jens Johnson");
        
        //EndScreen(endText);
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

            canvas.worldCamera = cameraVR; // Set the canvas world camera to the VR camera
        }
        // if enableVR is false, then disable the vrPlayerObject and enable the playerObject
        else
        {
            vrPlayerObject.SetActive(false);
            nonPlayerObject.SetActive(true);
            
            canvas.worldCamera = cameraNon; // Set the canvas world camera to the non-VR camera
        }
    }
    
    private void IntroScreen()
    {
        // Add code for intro screen here
    }
    
    public void EndScreen(string endText)
    {
        StartCoroutine(screenFade.FadeToBlack());
        StartCoroutine(screenFade.FadeInText(endText));
    }
    
    public string WinOrLose(string npcName)
    {
        string outcomeText = "";
        
        if (npcName == "Jens Johnson")
        {
            outcomeText = npcName +" was the murderer";
        }
        else
        {
            outcomeText = npcName + " was not the murderer";
        }

        return outcomeText;
    }
    
    /*private void CheckForVrOrNonVrPlayer()
    {
        if (enableVR)
        {
            GameObject playerObject = vrPlayerObject;
        }
        else
        {
            GameObject playerObject = nonPlayerObject;
        }
    }*/
}



