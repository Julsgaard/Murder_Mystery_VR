using System;
using System.Collections;
using System.Collections.Generic;
using Meta.WitAi.TTS.Utilities;
using UnityEngine;

public class TTSManager : MonoBehaviour
{
    public TTSSpeaker ttsSpeaker;
    private string[] ttsMessages;
    public void startTTS(GameObject currentNPC, string chatGptResponse)
    {
        ttsSpeaker = currentNPC.GetComponent<TTSSpeaker>();
        ttsSpeaker.Speak(chatGptResponse);
    }
    
    public void divideMessagesAndStartTTS(GameObject currentNPC, string chatGptResponse)
    {
        ttsSpeaker = currentNPC.GetComponent<TTSSpeaker>();

        // Check for question marks, exclamation marks, or dots
        char[] sentenceSeparators = { '.', '!', '?' };
        ttsMessages = chatGptResponse.Split(sentenceSeparators, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < ttsMessages.Length; i++)
        {
            Debug.Log($"Message{i + 1}: {ttsMessages[i]}");
            ttsSpeaker.SpeakQueued(ttsMessages[i]);
        }
    }
}
