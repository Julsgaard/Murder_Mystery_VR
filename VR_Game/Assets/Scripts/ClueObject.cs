using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClueObject : MonoBehaviour
{
    public List<NPCDescription> npcDescriptions = new List<NPCDescription>(); // Description for each NPC
    public bool isFound; // Whether the player has found this clue
    public ParticleSystem clueParticles; // The particle system for the clue
    public ButtonScript ButtonScript;


    private int _counter = 1;

    private void Start()
    {
        // Set the default clue prompt for each NPC
        foreach (var npcDescription in npcDescriptions)
        {
            NpcPersonality npcPersonality = npcDescription.npc.GetComponent<NpcPersonality>();
            npcPersonality.cluePrompt = "No objects found yet.";

            // Update the system prompt and the system prompt list
            string systemPrompt = npcPersonality.UpdateSystemPrompt();
            npcPersonality.UpdateSystemPromptList(systemPrompt);
        }
        
        // Start the particle system when the clue is not found
        if (!isFound)
        {
            clueParticles.Play();
        }
    }

    // When the player enters the trigger collider, add the clue description to the NPC's system prompt
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFound)
        {
            Debug.Log($"Player found clue: {gameObject.name}");
            
            // Stop the particle system when the clue is found
            clueParticles.Stop();

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
            ButtonScript.updateClueList(gameObject);
        }
    }
}

[System.Serializable]
public class NPCDescription
{
    public GameObject npc;
    public string description;
}