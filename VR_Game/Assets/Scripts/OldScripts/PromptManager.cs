using UnityEngine;

public class PromptManager : MonoBehaviour
{
    public GameManager gameManager;
    
    //Extracts relevant prompts based on the current NPCs NpcPersonality script, and combines them with playerResponse
    /*public string SystemPrompt(GameObject currentNpc)
    {
        string plotPrompt = currentNpc.GetComponent<NpcPersonality>().GetPlotPrompt();
        string backstoryPrompt = currentNpc.GetComponent<NpcPersonality>().GetBackstoryPrompt();
        //string conversationHistory = currentNpc.GetComponent<NpcPersonality>().GetConversationHistory();
        
        string systemPrompt = "You are a NPC in a murder mystery game. The murder mystery has the following setup:\n" +
                              $"{plotPrompt}\n\n" +
                              "The character you play has the following background:\n" +
                              $"{backstoryPrompt}\n\n" +
                              "You must answer as you character, and with a maximum of 25 words:";
        
        return systemPrompt;
    }*/
}
