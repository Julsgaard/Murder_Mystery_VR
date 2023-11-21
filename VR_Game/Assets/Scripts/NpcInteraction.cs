using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


public class NpcInteraction : MonoBehaviour
{
    public NpcCollision npcCollision;
    public ChatGPTManager chatGPTManager;
    public GameManager gameManager;
    public TTSManager ttsManager;
    public ResponseDivider responseDivider;
    
    private AudioClip _playerRecording; //Used to store the audio clip recorded by the player before sending it to OpenAI
    private bool _isRecording;
    //private String _systemPrompt; //The final combined prompt to be sent to openAI

    public event Action doneGeneratingNpcResponse;

    private void Start()
    {
        _isRecording = false;
    }

    //Controls the logic whether the recording should be started or stopped
    private void Update()
    {
        CheckForPlayerProximity();
        //Debug.Log($"In range:{npcCollision.IsPlayerInProximity()}");
    }

    // Checks if the player is in proximity of an NPC and if the player is pressing the E key
    private void CheckForPlayerProximity()
    {
        if (npcCollision.IsPlayerInProximity())
        {
            if (Keyboard.current.eKey.isPressed && _isRecording == false)
            {
                //Debug.Log("Player is talking to NPC");

                StartRecording();
            }

            if (Keyboard.current.eKey.IsPressed() == false && _isRecording)
            {
                //Debug.Log("Done talking");
                EndRecording();
            }
        }
        else if (_isRecording) //To handle the case where the player moves out of proximity without lifting the E key
        {
            // Player moved out of proximity while recording
            EndRecording();
        }
    }


    //Starts recording the player voice input to be transcribed and sent to OpenAI
    private void StartRecording()
    {
        _isRecording = true;

        _playerRecording = Microphone.Start(gameManager.selectedMicrophone,false, 60, 44100);

    }
    
    //Ends the recording, transcribes the audio and sends it to OpenAI
    private async void EndRecording()
    {
        _isRecording = false;
        Microphone.End(gameManager.selectedMicrophone);
        
        
        // Trim the audio clip to remove silence
        AudioClip trimmedAudioClip = SaveWav.TrimSilence(_playerRecording, 0.001f);

        // Save the audio clip to a wav file
        byte[] audio = SaveWav.Save("tempClip", trimmedAudioClip);
        
        // Save to file for debugging
        //string filePath = Application.dataPath + "/TempAudio.wav";
        //System.IO.File.WriteAllBytes(filePath, audio);

        // Transcribes the audio and get the text
        string playerResponse = await chatGPTManager.TranscribeAudioAndGetText(audio);
        Debug.Log($"Transcribed text: {playerResponse}");
        
        await GenerateNPCResponse(playerResponse);
    }
    
    

    public async Task GenerateNPCResponse(string playerResponse)
    {
        //Adds the playerResponse to the list of messages for ChatGPT API
        var combinedMessages = npcCollision.GetCurrentNpc().GetComponent<NpcPersonality>()
            .AddPlayerResponseToList(playerResponse);

        // Get the response from OpenAI
        string gptResponse = await chatGPTManager.AskChatGPT(combinedMessages);
        
        Debug.Log($"GPT response: {gptResponse}");
        
        //Response contains both the npcdialogue and the dialogue options for the player so we split it
        responseDivider.SplitResponseIntoNpcDialogueAndDialogueOptions(gptResponse);
        string _npcResponse = responseDivider.GetNpcResponse();
        
        
        
        
        Debug.Log($"NPC response: {_npcResponse}");
        Debug.Log($"Dialogue options: {responseDivider.GetDialogueOptionsArray()[0]}+{responseDivider.GetDialogueOptionsArray()[1]}+{responseDivider.GetDialogueOptionsArray()[2]}+{responseDivider.GetDialogueOptionsArray()[3]}");

        //Plays the TTS audio
        ttsManager.startTTS(npcCollision.GetCurrentNpc(), _npcResponse);

        //Adds the npcResponse to the list of messages for ChatGPT API
        npcCollision.GetCurrentNpc().GetComponent<NpcPersonality>().AddNpcResponseToList(_npcResponse);
        
        //Calls an event to notify the ButtonScript that the NPC response has been generated
        doneGeneratingNpcResponse?.Invoke();
    }
    
}

