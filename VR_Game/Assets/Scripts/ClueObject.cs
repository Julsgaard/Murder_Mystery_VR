using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClueObject : MonoBehaviour
{
    public List<NPCDescription> npcDescriptions = new List<NPCDescription>(); // Description for each NPC
    public bool isFound; // Whether the player has found this clue
    public ParticleSystem clueParticles; // The particle system for the clue
    public ButtonScript buttonScript;


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

    //When the player collides with the clue, add the clue description to the NPC's system prompt
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player has collided with the clue and that the clue has not been found yet
        if (collision.collider.CompareTag("Player") && !isFound)
        {
            Debug.Log($"Player found clue: {gameObject.name}");
            
            // Stop the particle system when the clue is found
            clueParticles.Stop();

            // For each NPC in the class npcDescriptions, add the clue description to their system prompt
            foreach (var npcDescription in npcDescriptions)
            {
                NpcPersonality npcPersonality = npcDescription.npc.GetComponent<NpcPersonality>();
                
                // If this is the first clue found, set the clue prompt to an empty string (to remove the default clue prompt "No objects found yet.")
                if (!npcPersonality.hasFoundFirstClue)
                {
                    npcPersonality.cluePrompt = "";
                    npcPersonality.hasFoundFirstClue = true;
                }
                
                // Add the clue description to the clue prompt
                npcPersonality.cluePrompt += _counter + ". " + npcDescription.description + "\n";

                // Update the system prompt
                string systemPrompt = npcPersonality.UpdateSystemPrompt();

                // Update the system prompt list
                npcPersonality.UpdateSystemPromptList(systemPrompt);
            }
            // Increase the counter
            _counter++;
            // Set isFound to true
            isFound = true;
            // Update the dialogue option buttons
            buttonScript.updateClueList(gameObject);
        }
    }
}

[System.Serializable]
public class NPCDescription
{
    public GameObject npc;
    public string description;
}