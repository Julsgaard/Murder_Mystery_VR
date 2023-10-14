using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcPersonality : MonoBehaviour
{
    [SerializeField]
    private string _backstoryPrompt;
    [SerializeField]
    private string _conversationPrompt;
    
    public string getBackstoryPrompt()
    {
        return _backstoryPrompt;
    }
    public string getConversationPrompt()
    {
        return _conversationPrompt;
    }






}
