using System;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class NpcManager : MonoBehaviour
{
    /** This script should contain the prompt defining the NPC's personality.
     */

    public string prompt;
    public ChatGPTManager chatGPTManager;
    public ProximityDetector proximityDetector;

    private bool isRecording = false;

    public string[] availableMicrophones;
    public string selectedMicrophone;

    public Keyboard keyboard;

    public List<GameObject> npcList;
    public List<ProximityDetector> proximityDetectorList;
    
    


    private Transform parentTransform; 

    private AudioClip _playerRecording; //Used to store the audio clip recorded by the player before sending it to OpenAI

    public void Start()
    {
        // Populate availableMicrophones with the names of all available microphones
        availableMicrophones = Microphone.devices;
        keyboard = Keyboard.current;
        
        parentTransform = gameObject.transform;
        for (int i = 0; i < parentTransform.childCount; i++) {
            GameObject child = parentTransform.GetChild(i).gameObject;
            npcList.Add(child);
            proximityDetectorList.Add(child.GetComponent<ProximityDetector>());

        }   
        
    }
    

    public void Update()
    {
        if (npcList[0].GetComponent(ProximityDetector)) && isRecording == false && keyboard.tKey.IsPressed() == true)
        {
            StartRecording();
            
        }
        if (keyboard.tKey.IsPressed() != true && isRecording == true)
        {
            EndRecording();
        }
    }

    private void StartRecording()
    {
        isRecording = true;

        _playerRecording = Microphone.Start(selectedMicrophone,false, 60, 44100);

    }

    private async void EndRecording()
    {
        isRecording = false;

        Microphone.End(selectedMicrophone);

        byte[] audio = SaveWav.Save("tempClip", _playerRecording);
        string transcribedText = await chatGPTManager.TranscribeAudioAndGetText(audio);

        chatGPTManager.AskChatGPT(transcribedText);
    }
}
