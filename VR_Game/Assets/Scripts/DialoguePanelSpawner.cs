using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePanelSpawner : MonoBehaviour
{
    private GameManager _gameManager;
    private NpcCollision _npcCollision;
    private GameObject _panel; //Used to store the reference to the panel gameobject that contains all of the buttons
    [SerializeField] private float panelOffsetY, panelOffsetZ;
    void Start()
    {
        //Search for the GameManager script
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        //Search for the panel gameobject among children of the current gameobject by its name
        _panel = gameObject.transform.Find("Panel").gameObject;
        
        //Search for the player gameobject by tag and get the NpcCollision script
        _npcCollision = GameObject.FindWithTag("Player").GetComponent<NpcCollision>();
        
        
        if (_gameManager.enableDialogueOptions == false)
        {
            gameObject.SetActive(false);
        }
        
        _npcCollision.playerEnteredNpcRange += EnableDialogueOptions;
        _npcCollision.playerExitedNpcRange += DisableDialogueOptions;
    }
    
    private void EnableDialogueOptions()
    {
        //"Spawn" the dialogue options at the current npc position
        gameObject.transform.position = _npcCollision.GetCurrentNpc().transform.position;
        //Make sure it is rotated correctly (So it faces the same direction as the npc)
        gameObject.transform.rotation = _npcCollision.GetCurrentNpc().transform.rotation;
        //Move it a bit up and forward so it doesn't clip with the npc
        gameObject.transform.Translate(Vector3.up * panelOffsetY);
        gameObject.transform.Translate(Vector3.forward * panelOffsetZ);
        _panel.SetActive(true);
    }
    
    private void DisableDialogueOptions()
    {
        _panel.SetActive(false);
    }
}
