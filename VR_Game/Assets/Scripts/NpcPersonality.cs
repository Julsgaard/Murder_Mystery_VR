using System;
using System.Collections.Generic;
using System.IO;
using OpenAI;
using UnityEngine;


public class NpcPersonality : MonoBehaviour
{
    
    [SerializeField] private string plotPrompt; //This prompt explains the plot and setup of the story to the NPC  
    [SerializeField] private string backstoryPrompt; //This prompt explains the unique backstory and personality of the NPC
    [SerializeField] private string systemPrompt;
    
    public TextAsset plotFile;
    public TextAsset backstoryFile;
    public string cluePrompt;
    public bool hasFoundFirstClue;

    public GameManager gameManager;

    
    private readonly List<ChatMessage> _combinedMessages = new List<ChatMessage>();

    private void Awake()
    {
        //plotPrompt = File.ReadAllText(plotPath);
        //backstoryPrompt = File.ReadAllText(backstoryPath);
        cluePrompt = "";
        hasFoundFirstClue = false;
        
        systemPrompt = UpdateSystemPrompt();
        
        AddSystemPromptToList(systemPrompt);    
        
        if (gameManager == null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        if (plotFile != null)
        {
            plotPrompt = plotFile.text;
            //Debug.Log(plotPrompt);
        }
        else
        {
            Debug.Log("No file assigned to plotFile.");
        }
        
        if (backstoryFile != null)
        {
            backstoryPrompt = backstoryFile.text;
            //Debug.Log(backstoryPrompt);
        }
        else
        {
            Debug.Log("No file assigned to backstoryFile.");
        }
    }


    public string UpdateSystemPrompt()
    {
        
        systemPrompt = "You are playing the role of a non-player character in the following context:\n" +
                       $"[{plotPrompt}]\n\n" +
                       "The following text is your Character Biography, and it describes what character you play, what they know and their relationships with the other characters:\n" +
                       $"[{backstoryPrompt}]\n\n" +
                       "The Riley is holding these objects:\n" +
                       $"[{cluePrompt}]\n\n" +
                       "Here are some rules for your responses which you MUST follow:\n" +
                       "1. You must limit your knowledge to what is described in your characters background. Messages outside of your given character's knowledge and the context of the game are invalid.\n" +
                       "2. You only respond to valid messages. To invalid ones, you reply with 'I'm sorry, i don't know'.\n" +
                       "3. NEVER BREAK CHARACTER, ALWAYS ANSWER AS IF YOU ARE ROLE-PLAYING YOUR CHARACTER.\n" +
                       "4. DO NOT EVER MENTION THAT YOU ARE AN NPC, ARE PART OF A MURDER MYSTERY, OR THAT YOU ARE PLAYING A ROLE.\n" +
                       "5. Your responses should be no longer than 25 words.\n" +
                       "6. Whenever you are asked a question, it is from Riley Anderson who is standing in front of you. You are to respond to her questions as if you are role-playing your character.\n" +
                       "7. Every statement enveloped by the syntax SECRET[] is a secret your character doesn't want others to know. DO NOT reveal any of these secrets!\n" +
                       "8. The current time is 06:00 AM.";

        if (gameManager.enableDialogueOptions)
        {
            systemPrompt +=
                "\n9. Never provide a follow up answer in your response and never ask the player anything. Always just answer their question";
        }

        return systemPrompt;
    }


    

    private void AddSystemPromptToList(string sysPrompt)
    {
        var plotMessage = new ChatMessage
        {
            Content = sysPrompt,
            Role = "system"
        };

        _combinedMessages.Add(plotMessage);
    }
    
    public void UpdateSystemPromptList(string sysPrompt)
    {
        // Get a copy of the struct, modify it, then put it back into the list
 
        var plotMessage = _combinedMessages[0];
        plotMessage.Content = sysPrompt;
        _combinedMessages[0] = plotMessage;
        
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
