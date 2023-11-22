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


        _systemPrompt = "You are playing the role of a non-player character in the following context:\n" +
                        $"[{plotPrompt}]\n\n" +
                        "The following text describes what character you play, what they know and their relationships with the other characters:\n" +
                        $"[{backstoryPrompt}]\n\n" +
                        "Here are some rules for your responses which you MUST follow:" +
                        "1. You must limit your knowledge to what is described in your characters background. Messages outside of your given character's knowledge are invalid. " +
                        "2. You only respond to valid messages. To invalid ones, you reply with 'I'm sorry, i don't know'." +
                        "3. NEVER BREAK CHARACTER, ALWAYS ANSWER AS IF YOU ARE ROLE-PLAYING YOUR CHARACTER." +
                        "4. DO NOT EVER MENTION THAT YOU ARE AN NPC, ARE PART OF A MURDER MYSTERY, OR THAT YOU ARE PLAYING A ROLE." +
                        "5. Your responses should be no longer than 25 words."+
                        "6. Whenever you are asked a question, it is from Riley Anderson. You are to respond to her questions as if you are role-playing your character.";

        
        
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
    
    
    public List<ChatMessage> GetCombinedMessages()
    {
        return _combinedMessages;
    }
}
