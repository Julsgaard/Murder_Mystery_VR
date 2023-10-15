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
    public PromptManager promptManager;
    
    private AudioClip _playerRecording; //Used to store the audio clip recorded by the player before sending it to OpenAI
    private bool _isRecording = false;
    private String _combinedPrompt; //The final combined prompt to be sent to openAI
    
    
    //Controls the logic whether the recording should be started or stopped
    private void Update()
    {
        CheckForPlayerProximity();
    }

    private void CheckForPlayerProximity()
    {
        if (npcCollision.IsPlayerInProximity()) ;
        {
            if (Keyboard.current.eKey.isPressed && _isRecording != true)
            {
                //Debug.Log("Player is talking to NPC");

                StartRecording();
            }

            if (Keyboard.current.eKey.IsPressed() != true && _isRecording)
            {
                //Debug.Log("Done talking");
                EndRecording();
            }
        }
    }


    //Starts recording the player voice input to be transcribed and sent to OpenAI
    public void StartRecording()
    {
        _isRecording = true;

        _playerRecording = Microphone.Start(gameManager.selectedMicrophone,false, 60, 44100);

    }
    
    //Ends the recording, transcribes the audio and sends it to OpenAI
    public async void EndRecording()
    {
        _isRecording = false;
        Microphone.End(gameManager.selectedMicrophone);
        
        AudioClip trimmedAudioClip = SaveWav.TrimSilence(_playerRecording, 0.001f);

        byte[] audio = SaveWav.Save("tempClip", trimmedAudioClip);
        
        // Save to file for debugging
        //string filePath = Application.dataPath + "/TempAudio.wav";
        //System.IO.File.WriteAllBytes(filePath, audio);

        
        string transcribedText = await chatGPTManager.TranscribeAudioAndGetText(audio);
        
        Debug.Log($"Transcribed text: {transcribedText}");
        
        _combinedPrompt = promptManager.CombinedPrompt(npcCollision.GetCurrentNpc(),transcribedText);
        chatGPTManager.AskChatGPT(_combinedPrompt);

        //chatGPTManager.AskChatGPT(transcribedText);
    }
}
