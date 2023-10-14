using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class NpcInteraction : MonoBehaviour
{
    public NpcCollision npcCollision;
    public ChatGPTManager chatGPTManager;
    
    public string[] availableMicrophones;
    public string selectedMicrophone = null;
    
    private AudioClip _playerRecording; //Used to store the audio clip recorded by the player before sending it to OpenAI
    public bool isRecording = false;
    
    //Finds the available microphones and selects the first one (The default)
    void Start()
    {
        availableMicrophones = Microphone.devices;
        selectedMicrophone = availableMicrophones[0];
    }
    
    //Controls the logic whether the recording shuold be started or stopped
    private void Update()
    {
        if (npcCollision.IsPlayerInProximity()) ;
        {
            if (Keyboard.current.eKey.isPressed && isRecording != true)
            {
                Debug.Log("Player is talking to NPC");
                
                StartRecording();
            }
            
            if (Keyboard.current.eKey.IsPressed() != true && isRecording)
            {
                Debug.Log("Done talking");
                EndRecording();
            }
        }
    }
    
    
    //Starts recording the player voice input to be transcribed and sent to OpenAI
    public void StartRecording()
    {
        isRecording = true;

        _playerRecording = Microphone.Start(selectedMicrophone,false, 60, 44100);

    }
    
    //Ends the recording, transcribes the audio and sends it to OpenAI
    public async void EndRecording()
    {
        isRecording = false;
        Microphone.End(selectedMicrophone);
        
        AudioClip trimmedAudioClip = SaveWav.TrimSilence(_playerRecording, 0.001f);
        
        

        byte[] audio = SaveWav.Save("tempClip", trimmedAudioClip);
        string transcribedText = await chatGPTManager.TranscribeAudioAndGetText(audio);
        
        Debug.Log($"Transcribed text: {transcribedText}");

        chatGPTManager.AskChatGPT(transcribedText);
    }
}
