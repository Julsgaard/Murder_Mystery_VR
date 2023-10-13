using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    // array of GameObjects that belong to the player
    public GameObject vrPlayerObject;
    public GameObject nonPlayerObject;
    
    public bool enableVR;
    public bool isRecording = false;
    
    public string[] availableMicrophones;
    public string selectedMicrophone;
    
    
    public ChatGPTManager chatGPTManager;
    
    private Transform parentTransform; 
    private AudioClip _playerRecording; //Used to store the audio clip recorded by the player before sending it to OpenAI


    
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
        
        // Populate availableMicrophones with the names of all available microphones
        availableMicrophones = Microphone.devices;
        keyboard = Keyboard.current;
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
    
    public void StartRecording()
    {
        isRecording = true;

        _playerRecording = Microphone.Start(selectedMicrophone,false, 60, 44100);

    }
    
    public async void EndRecording()
    {
        isRecording = false;

        Microphone.End(selectedMicrophone);

        byte[] audio = SaveWav.Save("tempClip", _playerRecording);
        string transcribedText = await chatGPTManager.TranscribeAudioAndGetText(audio);

        chatGPTManager.AskChatGPT(transcribedText);
    }
}
