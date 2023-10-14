using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class NpcInteraction : MonoBehaviour
{
    public NpcCollision npcCollision;
    public ChatGPTManager chatGPTManager;
    public GameManager gameManager;
    
    private AudioClip _playerRecording; //Used to store the audio clip recorded by the player before sending it to OpenAI
    public bool isRecording = false;
    
    
    //Controls the logic whether the recording should be started or stopped
    private void Update()
    {
        CheckForPlayerProximity();
    }

    private void CheckForPlayerProximity()
    {
        if (npcCollision.IsPlayerInProximity()) ;
        {
            if (Keyboard.current.eKey.isPressed && isRecording != true)
            {
                //Debug.Log("Player is talking to NPC");

                StartRecording();
            }

            if (Keyboard.current.eKey.IsPressed() != true && isRecording)
            {
                //Debug.Log("Done talking");
                EndRecording();
            }
        }
    }


    //Starts recording the player voice input to be transcribed and sent to OpenAI
    public void StartRecording()
    {
        isRecording = true;

        _playerRecording = Microphone.Start(gameManager.selectedMicrophone,false, 60, 44100);

    }
    
    //Ends the recording, transcribes the audio and sends it to OpenAI
    public async void EndRecording()
    {
        isRecording = false;
        Microphone.End(gameManager.selectedMicrophone);
        
        AudioClip trimmedAudioClip = SaveWav.TrimSilence(_playerRecording, 0.001f);

        byte[] audio = SaveWav.Save("tempClip", trimmedAudioClip);
        
        // Save to file for debugging
        //string filePath = Application.dataPath + "/TempAudio.wav";
        //System.IO.File.WriteAllBytes(filePath, audio);

        
        string transcribedText = await chatGPTManager.TranscribeAudioAndGetText(audio);
        
        Debug.Log($"Transcribed text: {transcribedText}");

        chatGPTManager.AskChatGPT(transcribedText);
    }
}
