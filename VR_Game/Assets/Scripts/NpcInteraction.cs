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
    public TTSManager ttsManager;
    
    private AudioClip _playerRecording; //Used to store the audio clip recorded by the player before sending it to OpenAI
    private bool _isRecording;
    private String _combinedPrompt; //The final combined prompt to be sent to openAI

    private void Start()
    {
        _isRecording = false;
    }

    //Controls the logic whether the recording should be started or stopped
    private void Update()
    {
        CheckForPlayerProximity();
    }

    private void CheckForPlayerProximity()
    {
        if (npcCollision.IsPlayerInProximity());
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

        
        string playerResponse = await chatGPTManager.TranscribeAudioAndGetText(audio);
        Debug.Log($"Transcribed text: {playerResponse}");
        
        
        
        _combinedPrompt = promptManager.CombinedPrompt(npcCollision.GetCurrentNpc());
        Debug.Log(_combinedPrompt);
        
        string npcResponse = await chatGPTManager.AskChatGPT(_combinedPrompt, playerResponse);
        Debug.Log(npcResponse);
        
        ttsManager.startTTS(npcCollision.GetCurrentNpc(), npcResponse);
        
        string conversationHistory = npcCollision.GetCurrentNpc().GetComponent<NpcPersonality>().
            AddToConversationHistory(npcCollision.GetCurrentNpc(), playerResponse, npcResponse);
        
        
        //chatGPTManager.AskChatGPT(transcribedText);
    }
}
