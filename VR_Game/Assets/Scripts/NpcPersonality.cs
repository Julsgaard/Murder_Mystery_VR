using System.Collections.Generic;
using System.IO;
using OpenAI;
using UnityEngine;


public class NpcPersonality : MonoBehaviour
{
    //TODO Refine the prompts (Found in Assets/Prompts)
    
    [SerializeField] private string plotPrompt; //This prompt explains the plot and setup of the story to the NPC  
    [SerializeField] private string backstoryPrompt; //This prompt explains the unique backstory and personality of the NPC
    private string _systemPrompt;
    
    public string plotPath = "Assets/Prompts/PlotPrompt.txt";
    public string backstoryPath = "Assets/Prompts/BackgroundPrompt1.txt";
    
    private readonly List<ChatMessage> _combinedMessages = new List<ChatMessage>();
    

    private void Start()
    {
        plotPrompt = File.ReadAllText(plotPath);
        backstoryPrompt = File.ReadAllText(backstoryPath);

        _systemPrompt = "You are a NPC in a murder mystery. The murder mystery has the following setup:\n" +
                        $"{plotPrompt}\n\n" +
                        "The character you are has the following background:\n" +
                        $"{backstoryPrompt}\n\n" +
                        "You answer in a way that is suitable for text to speech, and You must answer as you character, and with a maximum of 25 words:";
        
        AddSystemPromptToList();
    }
    
    private void AddSystemPromptToList()
    {
        var plotMessage = new ChatMessage
        {
            Content = _systemPrompt,
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
        
        // Debugging: Log each message's Content
        /*Debug.Log("Current messages in _combinedMessages:");
        foreach (ChatMessage message in _combinedMessages)
        {
            Debug.Log("Role: " + message.Role + ", Content: " + message.Content);
        }*/
        
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
