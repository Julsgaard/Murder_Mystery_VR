using System.Collections;
using System.Collections.Generic;
using Meta.WitAi.TTS.Utilities;
using UnityEngine;

public class TTSManager : MonoBehaviour
{
    public TTSSpeaker ttsSpeaker;
    public void startTTS(GameObject currentNPC, string chatGptResponse)
    {
        ttsSpeaker.Speak(chatGptResponse);
    }
}
