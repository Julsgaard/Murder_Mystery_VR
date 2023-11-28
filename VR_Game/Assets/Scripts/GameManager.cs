using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Scripts 
    //[SerializeField] private ScreenFade screenFade;
    [SerializeField] private GameTimer gameTimer;
    
    //[SerializeField] private Canvas canvas;
    //[SerializeField] private Camera cameraVR;
    //[SerializeField] private Camera cameraNon;
    
    // End screen text
    [SerializeField] private TextMeshPro endScreenTMP;

    // GameObjects that belong to the player
    public GameObject vrPlayerObject;
    public GameObject xrOrigin;
    public GameObject nonPlayerObject;
    
    // GameObjects that belong to the control tutorial in the intro scene
    public GameObject pushToTalkToolTip;
    public GameObject uiPressToolTip;

    public bool enableVR;// Whether VR is enabled or not}
    public bool enableDialogueOptions; // Whether dialogue options are enabled or not

    // Microphone variables
    public string[] availableMicrophones;
    public string selectedMicrophone;
    
    
    void Awake()
    {
        // Selects the microphone
        SelectMicrophone();
        
        // Enables VR if enableVR is true, otherwise enables playercontroller for testing
        EnableVR();
        
        // Checks if dialogue options are enabled or not and enables/disables the correct tool tips
        CheckForDialogueOptions();
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
            
            //canvas.worldCamera = cameraVR; // Set the canvas world camera to the VR camera
        }
        // if enableVR is false, then disable the vrPlayerObject and enable the playerObject
        else
        {
            vrPlayerObject.SetActive(false);
            nonPlayerObject.SetActive(true);
            
            //canvas.worldCamera = cameraNon; // Set the canvas world camera to the non-VR camera
        }
    }
    
    private void CheckForDialogueOptions()
    {
        // If dialogue options are enabled, then disable the push to talk tool tip and enable the UI press tool tip
        if (enableDialogueOptions)
        {
            pushToTalkToolTip.SetActive(false);
            uiPressToolTip.SetActive(true);
        }
        // If dialogue options are disabled, then enable the push to talk tool tip and disable the UI press tool tip
        else
        {
            pushToTalkToolTip.SetActive(true);
            uiPressToolTip.SetActive(false);
        }
    }
    
    public void TeleportPlayer()
    {
        //StartCoroutine(screenFade.FadeToBlack());

        if (enableVR)
        {
            // Starts the watch timer
            gameTimer.StartTimer();
            
            // Moved the VR player to the correct position
            xrOrigin.transform.localPosition = new Vector3(0, 0, 0);
            //vrPlayerObject.transform.position = new Vector3(-8.8f,5.4f,-19.0f);
            vrPlayerObject.transform.position = new Vector3(7.42f, 5.88f, -16.14f);
            vrPlayerObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            // Moves the non-VR player to the correct position
            //nonPlayerObject.transform.position = new Vector3(-8.8f,5.4f+1,-19.0f);
            nonPlayerObject.transform.position = new Vector3(7.42f, 5.88f+1, -16.14f);
        }
        
        //StartCoroutine(screenFade.FadeFromBlack());
    }
    
    public void EndScreen(string endText)
    {
        //StartCoroutine(screenFade.FadeToBlack());
        //StartCoroutine(screenFade.FadeInText(endText));
        
        //StartCoroutine(PauseAfterDelayCoroutine(5f));
        
        endScreenTMP.text = endText;
        
        if (enableVR)
        {
            xrOrigin.transform.localPosition = new Vector3(0, 0, 0);
            vrPlayerObject.transform.position = new Vector3(-11.27f,-17.59f,-21.29f);
            vrPlayerObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            nonPlayerObject.transform.position = new Vector3(-11.27f,-17.59f+1,-21.29f);
            nonPlayerObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
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
    
}



