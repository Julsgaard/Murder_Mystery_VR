using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialoguePanelSpawner : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private NpcCollision _npcCollision;
    [SerializeField] private GameObject _panel; //Used to store the reference to the panel gameobject that contains all of the buttons
    [SerializeField] private float panelOffsetY, panelOffsetZ, panelOffsetX, panelRotationY;

    private Vector3 _panelPos;
    private Transform _NPCHipTransform;
    private bool _isColliding = false;
    void Start()
    {
        // Only enable the dialogue options if dialogue options are enabled
        if (!_gameManager.enableDialogueOptions)
            return;
        
        //Search for the panel gameobject among children of the current gameobject by its name
        _panel = gameObject.transform.Find("Panel").gameObject;

        // Gets the NpcCollision script from the correct player object 
        if (_gameManager.enableVR)
        {
            _npcCollision = GameObject.Find("XR Origin (XR Rig)").GetComponent<NpcCollision>();        
        }
        else
        {
            _npcCollision = _gameManager.nonPlayerObject.GetComponent<NpcCollision>();
        }
   
        
        _npcCollision.playerEnteredNpcRange += EnableDialogueOptions;
        _npcCollision.playerExitedNpcRange += DisableDialogueOptions;
    }

    private void Update()
    {
        if (!_gameManager.enableDialogueOptions)
            return;
        
        // Move the panel to the correct position if the player is colliding with an npc
        if (_isColliding)
        {
            _panelPos.y = _NPCHipTransform.position.y + panelOffsetY;
            transform.position = _panelPos;
        }
    }

    private void EnableDialogueOptions()
    {
        // Only enable the dialogue options if VR is enabled
        if (_gameManager.enableVR)
        {
            //"Spawn" the dialogue options at the current npc position
            _panelPos = _npcCollision.GetCurrentNpc().transform.TransformPoint(panelOffsetX,panelOffsetY,panelOffsetZ);

            //Make sure it is rotated correctly (So it faces the same direction as the npc)
            gameObject.transform.rotation = _npcCollision.GetCurrentNpc().transform.rotation;
            transform.Rotate(Vector3.up * -panelRotationY);

            _NPCHipTransform = _npcCollision.GetCurrentNpc().transform.GetChild(1).transform.GetChild(0).transform;

            _isColliding = true;
            _panel.SetActive(true);
        }
    }
    
    private void DisableDialogueOptions()
    {
        _isColliding = false;
        _panel.SetActive(false);
    }
}
