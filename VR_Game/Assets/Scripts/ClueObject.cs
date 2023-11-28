using System.Collections.Generic;
using UnityEngine;

public class ClueObject : MonoBehaviour
{
    public List<NPCDescription> npcDescriptions = new List<NPCDescription>(); // Description for each NPC
    public bool isFound; // Whether the player has found this clue

    private int _counter = 1;

    private void Start()
    {
        foreach (var npcDescription in npcDescriptions)
        {
            NpcPersonality npcPersonality = npcDescription.npc.GetComponent<NpcPersonality>();
            npcPersonality.cluePrompt = "No objects found yet.";

            // Update the system prompt and the system prompt list
            string systemPrompt = npcPersonality.UpdateSystemPrompt();
            npcPersonality.UpdateSystemPromptList(systemPrompt);
        }
    }

    // When the player enters the trigger collider, add the clue description to the NPC's system prompt
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFound)
        {
            Debug.Log($"Player found clue: {gameObject.name}");

            // For each NPC in npcDescriptions, add the clue description to their system prompt
            foreach (var npcDescription in npcDescriptions)
            {
                NpcPersonality npcPersonality = npcDescription.npc.GetComponent<NpcPersonality>();

                if (!npcPersonality.hasFoundFirstClue)
                {
                    npcPersonality.cluePrompt = "";
                    npcPersonality.hasFoundFirstClue = true;
                }

                npcPersonality.cluePrompt += _counter + ". " + npcDescription.description + "\n";

                string systemPrompt = npcPersonality.UpdateSystemPrompt();

                npcPersonality.UpdateSystemPromptList(systemPrompt);
            }

            _counter++;
            isFound = true;
        }
    }
}

[System.Serializable]
public class NPCDescription
{
    public GameObject npc;
    public string description;
}