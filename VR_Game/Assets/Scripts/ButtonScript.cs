using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    private string _buttonText;
    public List<Button> buttonList = new List<Button>();
    public NpcInteraction npcInteraction;
    

    private void Start()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            int buttonIndex = i; // Capture the index in a local variable
            buttonList[i].onClick.AddListener(() => OnClick(buttonIndex));
            
        }
        
    }


    
    public void OnClick(int buttonIndex)
    {
        if (buttonIndex >= 0 && buttonIndex < buttonList.Count)
        {
            Debug.Log($"Button {buttonIndex + 1} clicked");
            
            _buttonText = buttonList[buttonIndex].GetComponentInChildren<TMP_Text>().text;
            Debug.Log($"Button {buttonIndex + 1} text: {_buttonText}");
            
            //npcInteraction.SendDialogueOptionToOpenAI();
            
            // Call a method based on the clicked button
            //npcInteraction.HandleButtonClicked(buttonIndex);
        }
    }
}
