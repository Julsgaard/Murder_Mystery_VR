using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DialogueRayVisibility : MonoBehaviour
{
    private GameManager _gameManager;
    private NpcCollision _npcCollision;
    
    [SerializeField]
    XRRayInteractor _xrRayInteractor;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager.enableDialogueOptions == false)
            return; // if dialogue is disabled, we dont want rays either, so we just skip the setup

        _npcCollision = GameObject.FindWithTag("Player").GetComponent<NpcCollision>();
        _xrRayInteractor = transform.GetChild(2).gameObject.GetComponent<XRRayInteractor>();

        _npcCollision.playerEnteredNpcRange += EnableRay;
        _npcCollision.playerExitedNpcRange += LateDisableRay;
    }

    void EnableRay () {
        _xrRayInteractor.enabled = true;
    }

    void DisableRay ()
    {
        if (!_npcCollision.IsPlayerInProximity())
            _xrRayInteractor.enabled = false;
    }

    void LateDisableRay ()
    {
        Invoke(nameof(DisableRay), 1.1f);
    }
}
