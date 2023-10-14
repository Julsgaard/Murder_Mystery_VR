using UnityEngine;

public class PromptManager : MonoBehaviour
{
    public GameManager gameManager;
    
    //TODO: Not working yet
    // Combines the backstory prompt, interaction prompt, the player's response, the conversation history 
    public string CombinedPrompt(string backstoryPrompt, string interactionPrompt, string playerResponse, string conversationHistory)
    {
        string combinedPrompt = backstoryPrompt + interactionPrompt + playerResponse + conversationHistory;
        return combinedPrompt;
    }
}
