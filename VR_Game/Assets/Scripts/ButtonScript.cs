using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    private string _buttonText;
    public NpcInteraction npcInteraction;
    

    public void OnClick()
    {
        _buttonText = gameObject.GetComponent<TextMeshPro>().text;
        Debug.Log($"Button text: {_buttonText}");
    }
}
