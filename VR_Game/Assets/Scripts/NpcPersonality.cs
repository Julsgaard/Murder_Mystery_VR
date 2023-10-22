using System.Collections.Generic;
using System.IO;
using OpenAI;
using UnityEngine;


public class NpcPersonality : MonoBehaviour
{
    //TODO Refine the prompts (Found in Assets/Prompts)
    
    [SerializeField] private string plotPrompt; //This prompt explains the plot and setup of the story to the NPC  
    [SerializeField] private string backstoryPrompt; //This prompt explains the unique backstory and personality of the NPC
    [SerializeField] private string systemPrompt;
    
    public string plotPath = "Assets/Prompts/PlotPrompt.txt";
    public string backstoryPath = "Assets/Prompts/BackgroundPrompt1.txt";
    
    private List<ChatMessage> _combinedMessages = new List<ChatMessage>();
    

    private void Start()
    {
        plotPrompt = File.ReadAllText(plotPath);
        backstoryPrompt = File.ReadAllText(backstoryPath);

        systemPrompt = "You are a NPC in a murder mystery game. The murder mystery has the following setup:\n" +
                        $"{plotPrompt}\n\n" +
                        "The character you play has the following background:\n" +
                        $"{backstoryPrompt}\n\n" +
                        "You must answer as you character, and with a maximum of 25 words:";
        
        AddSystemPromptToList();
        
        //AddPlotPromptToList();
        //AddBackstoryPromptToList();
    }

    /*public string GetPlotPrompt()
    {
        return plotPrompt;
    }
    public string GetBackstoryPrompt()
    {
        return backstoryPrompt;
    }*/

    
    /*public string AddToConversationHistory(GameObject currentNpc, string playerResponse, string npcResponse)
    {
        ChatMessage playerMessage = new ChatMessage();
        playerMessage.Content = playerResponse;
        playerMessage.Role = "user";
        
        ChatMessage npcMessage = new ChatMessage();
        npcMessage.Content = npcResponse;
        npcMessage.Role = "assistant";
        
        
        //conversationHistory += "Player: " + playerResponse + "\n";
        ////conversationHistory += currentNpc.name + npcResponse + "\n";
        //conversationHistory += "You: " + npcResponse + "\n";
        
        return conversationHistory;
    }*/
    
    /*private void AddPlotPromptToList()
    {
        ChatMessage plotMessage = new ChatMessage();
        plotMessage.Content = plotPrompt;
        plotMessage.Role = "system";
        
        _combinedMessages.Add(plotMessage);
    }
    
    private void AddBackstoryPromptToList()
    {
        ChatMessage backstoryMessage = new ChatMessage();
        backstoryMessage.Content = backstoryPrompt;
        backstoryMessage.Role = "system";
        
        _combinedMessages.Add(backstoryMessage);
    }*/
    
    private void AddSystemPromptToList()
    {
        var plotMessage = new ChatMessage
        {
            Content = systemPrompt,
            Role = "system"
        };

        _combinedMessages.Add(plotMessage);
    }
    
    public List<ChatMessage> AddPlayerResponseToList(string playerResponse)
    {
        var userMessage = new ChatMessage
        {
            Content = playerResponse,
            Role = "user"
        };

        _combinedMessages.Add(userMessage);
        
        return _combinedMessages;
    }
    
    public void AddNpcResponseToList(string npcResponse)
    {
        var assistantMessage = new ChatMessage
        {
            Content = npcResponse,
            Role = "assistant"
        };

        _combinedMessages.Add(assistantMessage);
    }





}
