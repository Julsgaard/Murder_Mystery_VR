using System;
using UnityEngine;

public class HandCuffTrigger : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private bool _isHandcuffed;
    
    void Start()
    {
        _isHandcuffed = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.CompareTag("NPC") && !_isHandcuffed)
        {
            _isHandcuffed = true;
            
            string npcName = other.gameObject.name;
            Debug.Log($"Player has handcuffed {npcName}");
            
            string endText = gameManager.WinOrLose(npcName);
            gameManager.EndScreen(endText);
        }
    }
}
