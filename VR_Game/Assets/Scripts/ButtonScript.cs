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
    public List<Button> buttonList = new List<Button>();
    public List<TMP_Text> _buttonTextList = new List<TMP_Text>();
    private int _amountOfEnabledButtons;

    public Button backButton; //Button to handle going back to the defaultQuestions

    private enum DialogueOptionState
    {
        DefaultQuestions,
        CharacterQuestions,
        ClueQuestions,
        EventQuestions,
        BackstoryQuestions
    }

    private DialogueOptionState _currentState;
    
    //Reference to NpcInteraction script in order to access the chatGPT api
    public NpcInteraction npcInteraction;
    
   
    
    
    private void Start()
    {
        //Set current state to DefaultQuestions
        _currentState = DialogueOptionState.DefaultQuestions;
        
        //Add onClick listeners to button in buttonList
        for (int i = 0; i < buttonList.Count; i++)
        {
            int buttonIndex = i;
            buttonList[i].onClick.AddListener(() => OnClick(buttonIndex));
            
            //Also add the text of each button to buttonTextList
            _buttonTextList.Add(buttonList[i].GetComponentInChildren<TMP_Text>());
        }
        
        //Add a listener to the backbutton
        backButton.onClick.AddListener(OnClickBackButton);
        UpdateButtonText();
    }
    

    public void OnClick(int buttonIndex)
    {
        //If in default state, change state based on which button was pressed
        if (_currentState == DialogueOptionState.DefaultQuestions)
        {
            if (buttonIndex == 0) _currentState = DialogueOptionState.CharacterQuestions;
            else if (buttonIndex == 1) _currentState = DialogueOptionState.ClueQuestions;
            else if (buttonIndex == 2) _currentState = DialogueOptionState.EventQuestions;
            else if (buttonIndex == 3) _currentState = DialogueOptionState.BackstoryQuestions;
        }
        //If in another other state than default state, send the button text to openAI API and return to default state
        else
        {
            npcInteraction.GenerateNPCResponse(_buttonTextList[buttonIndex].text);
            _currentState = DialogueOptionState.DefaultQuestions;
        }

        //Update the text (And position of buttons) based on the current state (NOT VERY ELEGANT I KNOW...)
        UpdateButtonText();

    }

    private void UpdateButtonText()
    {
        //switch statement to update the text of each button based on the current state
        switch (_currentState)
        {   
            case DialogueOptionState.DefaultQuestions:
                _amountOfEnabledButtons = 4;
                UpdateButtonPositions(_amountOfEnabledButtons);
                _buttonTextList[0].text = "Ask about character";
                _buttonTextList[1].text = "Ask about clues";
                _buttonTextList[2].text = "Ask about events";
                _buttonTextList[3].text = "Ask about backstory";
                backButton.gameObject.SetActive(false);
                break;
            case DialogueOptionState.CharacterQuestions:
                _amountOfEnabledButtons = 6;
                UpdateButtonPositions(_amountOfEnabledButtons);
                _buttonTextList[0].text = "Ashley";
                _buttonTextList[1].text = "Jens";
                _buttonTextList[2].text = "Me";
                _buttonTextList[3].text = "Leonard";
                _buttonTextList[4].text = "Quinn";
                _buttonTextList[5].text = "Chris";
                backButton.gameObject.SetActive(true);
                break;
            case DialogueOptionState.ClueQuestions:
                _amountOfEnabledButtons = 4;
                UpdateButtonPositions(_amountOfEnabledButtons);
                _buttonTextList[0].text = "Clue 1";
                _buttonTextList[1].text = "Clue 2";
                _buttonTextList[2].text = "Clue 3";
                _buttonTextList[3].text = "Clue 4";
                backButton.gameObject.SetActive(true);
                break;
            case DialogueOptionState.EventQuestions:
                _amountOfEnabledButtons = 4;
                UpdateButtonPositions(_amountOfEnabledButtons);
                _buttonTextList[0].text = "The murder";
                _buttonTextList[1].text = "Your alibi";
                _buttonTextList[2].text = "";
                _buttonTextList[3].text = "";
                backButton.gameObject.SetActive(true);
                break;
            case DialogueOptionState.BackstoryQuestions:
                _amountOfEnabledButtons = 4;
                UpdateButtonPositions(_amountOfEnabledButtons);
                _buttonTextList[0].text = "Who are you?";
                _buttonTextList[1].text = "What are your interests?";
                _buttonTextList[2].text = "";
                _buttonTextList[3].text = "";
                backButton.gameObject.SetActive(true);
                break;
            
        }
        
    }

    private void UpdateButtonPositions(int amountOfButtons)
    {
        switch (amountOfButtons)
        {
            case 4:
                buttonList[4].gameObject.SetActive(false);
                buttonList[5].gameObject.SetActive(false);
                
                buttonList[0].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 90f, 0f);
                buttonList[1].GetComponent<RectTransform>().anchoredPosition = new Vector3(200f, -30f, 0);
                buttonList[2].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -150f, 0);
                buttonList[3].GetComponent<RectTransform>().anchoredPosition = new Vector3(-200f, -30f, 0);
                break;
            case 5:
                break;
            case 6:
                buttonList[4].gameObject.SetActive(true);
                buttonList[5].gameObject.SetActive(true);
                
                buttonList[0].GetComponent<RectTransform>().anchoredPosition = new Vector3(148.9001f, 101.6799f, 0);
                buttonList[1].GetComponent<RectTransform>().anchoredPosition = new Vector3(172.88f, -27.82f, 0);
                buttonList[2].GetComponent<RectTransform>().anchoredPosition = new Vector3(148.9001f, -162.1301f, 0);
                buttonList[3].GetComponent<RectTransform>().anchoredPosition = new Vector3(-138.8999f, -162.1201f, 0);
                buttonList[4].GetComponent<RectTransform>().anchoredPosition = new Vector3(-162.8799f, -27.82568f, 0);
                buttonList[5].GetComponent<RectTransform>().anchoredPosition = new Vector3(-138.8999f, 101.6799f, 0);
                break;
            
        }
    }
    
    private void OnClickBackButton()
    {
        _currentState = DialogueOptionState.DefaultQuestions;
        
        UpdateButtonText();
    }
}
