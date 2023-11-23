using UnityEngine;

public class ClueObject : MonoBehaviour
{
    public string description; // Description for the clue
    public bool isFound; // Whether the player has found this clue
    public GameObject[] npcArray; // Array of NPCs
    
    private int _counter = 1;
    
    private void Awake()
    {
        //find every script with the tag NPC
        npcArray = GameObject.FindGameObjectsWithTag("NPC");
    }

    
    // When the player enters the trigger collider, add the clue description to the NPC's system prompt
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFound)
        {
            Debug.Log($"Player found clue: {gameObject.name}");
            

            // For each NPC with the tag NPC, add the clue description to their system prompt
            foreach (var npc in npcArray)
            {
                NpcPersonality npcPersonality = npc.GetComponent<NpcPersonality>();
                
                if (npcPersonality.cluePrompt == "No objects found yet.")
                    npcPersonality.cluePrompt = "";
                
                npcPersonality.cluePrompt += _counter + ". " + description + "\n";

                string systemPrompt = npcPersonality.UpdateSystemPrompt();
                
                npcPersonality.UpdateSystemPromptList(systemPrompt);
            }
            
            _counter++;
            isFound = true;
        }
    }
}
