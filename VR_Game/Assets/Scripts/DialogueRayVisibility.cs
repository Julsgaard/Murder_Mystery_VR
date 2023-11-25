using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DialogueRayVisibility : MonoBehaviour
{
    GameManager _gameManager;
    NpcCollision _npcCollision;
    
    [SerializeField]
    XRRayInteractor _xrRayInteractor;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _npcCollision = GameObject.FindWithTag("Player").GetComponent<NpcCollision>();

        if (_gameManager.enableDialogueOptions == false)
            return; // if dialogue is disabled, we dont want rays either, so we just skip the setup


        _xrRayInteractor = transform.GetChild(2).gameObject.GetComponent<XRRayInteractor>();

        _npcCollision.playerEnteredNpcRange += EnableRay;
        _npcCollision.playerExitedNpcRange += LateDisableRay;
    }

    void EnableRay () {
        _xrRayInteractor.enabled = true;
    }

    void DisableRay ()
    {
        _xrRayInteractor.enabled = false;
    }

    void LateDisableRay ()
    {
        Invoke(nameof(DisableRay), 1.1f);
    }
}
