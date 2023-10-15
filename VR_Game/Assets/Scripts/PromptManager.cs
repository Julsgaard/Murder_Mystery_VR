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
    
    //Extracts relevant prompts based on the current NPC's NpcPersonality script, and combines them with playerResponse
    public string CombinedPrompt(GameObject currentNpc, string playerResponse)
    {
        string plotPrompt = currentNpc.GetComponent<NpcPersonality>().getPlotPrompt();
        string backstoryPrompt = currentNpc.GetComponent<NpcPersonality>().getBackstoryPrompt();
        string combinedPrompt = plotPrompt + backstoryPrompt + playerResponse;
        return combinedPrompt;
    }
}
