using UnityEngine;

public class PromptManager : MonoBehaviour
{
    public GameManager gameManager;
    
    //private string _conversationHistory;
    
    //Extracts relevant prompts based on the current NPC's NpcPersonality script, and combines them with playerResponse
    public string CombinedPrompt(GameObject currentNpc)
    {
        string plotPrompt = currentNpc.GetComponent<NpcPersonality>().getPlotPrompt();
        string backstoryPrompt = currentNpc.GetComponent<NpcPersonality>().getBackstoryPrompt();
        string conversationHistory = currentNpc.GetComponent<NpcPersonality>().conversationHistory;



        string combinedPrompt = "You are an actor in a murder mystery. The murder mystery has the following setup:\n" +
                                $"{plotPrompt}\n\n" +
                                "The character you play has the following background:\n" +
                                $"{backstoryPrompt}\n\n" +
                                "This is your conversation with the player so far:\n" +
                                $"{conversationHistory}\n\n" +
                                "You must answer as you character, and with a maximum of 25 words:";
        
        return combinedPrompt;
    }
    
}
