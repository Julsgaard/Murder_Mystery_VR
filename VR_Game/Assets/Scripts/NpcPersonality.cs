using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class NpcPersonality : MonoBehaviour
{
    //TODO Add conversation history and refine the prompts (Found in Assets/Prompts)
    
    [SerializeField] 
    private string _plotPrompt; //This prompt explains the plot and setup of the story to the NPC  
    [SerializeField]
    private string _backstoryPrompt; //This prompt explains the unique backstory and personality of the NPC

    public string conversationHistory = "No conversation so far"; //This prompt explains the conversation history between the player and the NPC
    
    public string plotPath = "Assets/Prompts/PlotPrompt.txt";
    public string backstoryPath = "Assets/Prompts/BackgroundPrompt1.txt";
    

    private void Start()
    {
        _plotPrompt = File.ReadAllText(plotPath);
        _backstoryPrompt = File.ReadAllText(backstoryPath);
        //Conversation prompt here eventually
    }

    public string getPlotPrompt()
    {
        return _plotPrompt;
    }
    public string getBackstoryPrompt()
    {
        return _backstoryPrompt;
    }
    /*public string getConversationHistory()
    {
        return conversationHistory;
    }*/
    
    public string AddToConversationHistory(GameObject currentNpc, string playerResponse, string npcResponse)
    {
        conversationHistory += "Player: " + playerResponse + "\n";
        //conversationHistory += currentNpc.name + npcResponse + "\n";
        conversationHistory += "You: " + npcResponse + "\n";
        
        return conversationHistory;
    }





}
