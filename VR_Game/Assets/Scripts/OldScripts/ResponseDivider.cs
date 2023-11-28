using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseDivider : MonoBehaviour
{
    private string _npcResponse, _dialogueOptions, _dialogueOption1, _dialogueOption2, _dialogueOption3, _dialogueOption4;
    private string[] _dialogueOptionsArray;
    //This method is used to split the response from OpenAI into the NPC's dialogue and the dialogue options presented to the player
    public void SplitResponseIntoNpcDialogueAndDialogueOptions(string inputString)
    {
        int index = inputString.IndexOf('1');

        if (index != -1)
        {
            _npcResponse = inputString.Substring(0, index);
            _dialogueOptions = inputString.Substring(index);

            // Split _dialogueOptions into four options
            string[] options = _dialogueOptions.Split(new[] { '1', '2', '3', '4' }, StringSplitOptions.RemoveEmptyEntries);

            if (options.Length >= 4)
            {
                _dialogueOption1 = options[0].Trim();
                _dialogueOption2 = options[1].Trim();
                _dialogueOption3 = options[2].Trim();
                _dialogueOption4 = options[3].Trim();
                
                _dialogueOptionsArray = new string[4];
                _dialogueOptionsArray[0] = _dialogueOption1;
                _dialogueOptionsArray[1] = _dialogueOption2;
                _dialogueOptionsArray[2] = _dialogueOption3;
                _dialogueOptionsArray[3] = _dialogueOption4;
            }
        }
    }
    
    
    //Get _npcResponse
    public string GetNpcResponse()
    {
        return _npcResponse;
    }
    
    //Get _dialogueOptionsArray
    public string[] GetDialogueOptionsArray()
    {
        return _dialogueOptionsArray;
    }
}
