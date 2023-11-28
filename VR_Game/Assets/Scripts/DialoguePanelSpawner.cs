using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialoguePanelSpawner : MonoBehaviour
{
    private GameManager _gameManager;
    private NpcCollision _npcCollision;
    private GameObject _panel; //Used to store the reference to the panel gameobject that contains all of the buttons
    [SerializeField] private float panelOffsetY, panelOffsetZ, panelOffsetX, panelRotationY;

    private Vector3 _panelPos;
    private Transform _NPCHipTransform;
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

    private void Update()
    {
        _panelPos.y = _NPCHipTransform.position.y + panelOffsetY;
        transform.position = _panelPos;
    }

    private void EnableDialogueOptions()
    {
        //"Spawn" the dialogue options at the current npc position
        _panelPos = _npcCollision.GetCurrentNpc().transform.TransformPoint(panelOffsetX,panelOffsetY,panelOffsetZ);

        //Make sure it is rotated correctly (So it faces the same direction as the npc)
        gameObject.transform.rotation = _npcCollision.GetCurrentNpc().transform.rotation;
        transform.Rotate(Vector3.up * -panelRotationY);

        _NPCHipTransform = _npcCollision.GetCurrentNpc().transform.GetChild(1).transform.GetChild(0).transform;

        _panel.SetActive(true);
    }
    
    private void DisableDialogueOptions()
    {
        _panel.SetActive(false);
    }
}
