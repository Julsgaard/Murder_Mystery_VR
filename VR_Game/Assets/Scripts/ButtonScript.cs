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
    public List<TMP_Text> buttonTextList = new List<TMP_Text>();
    private int _amountOfEnabledButtons;
    private List<String> _clueList = new List<string>();
    

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
            buttonTextList.Add(buttonList[i].GetComponentInChildren<TMP_Text>());
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
            npcInteraction.GenerateNPCResponse(buttonTextList[buttonIndex].text);
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
                buttonTextList[0].text = "Ask about character";
                buttonTextList[1].text = "Ask about clues";
                buttonTextList[2].text = "Ask about events";
                buttonTextList[3].text = "Ask about backstory";
                backButton.gameObject.SetActive(false);
                break;
            case DialogueOptionState.CharacterQuestions:
                _amountOfEnabledButtons = 6;
                UpdateButtonPositions(_amountOfEnabledButtons);
                buttonTextList[0].text = "Ashley";
                buttonTextList[1].text = "Jens";
                buttonTextList[2].text = "Me";
                buttonTextList[3].text = "Leonard";
                buttonTextList[4].text = "Quinn";
                buttonTextList[5].text = "Chris";
                backButton.gameObject.SetActive(true);
                break;
            case DialogueOptionState.ClueQuestions:
                _amountOfEnabledButtons = _clueList.Count;
                UpdateButtonPositions(_amountOfEnabledButtons);
                //Loop over the clueList and set the text of each button to the name of the clue
                for (int i = 0; i < _clueList.Count; i++)
                {
                    buttonTextList[i].text = _clueList[i];
                }
                backButton.gameObject.SetActive(true);
                break;
            case DialogueOptionState.EventQuestions:
                _amountOfEnabledButtons = 4;
                UpdateButtonPositions(_amountOfEnabledButtons);
                buttonTextList[0].text = "What do you think of the murder?";
                buttonTextList[1].text = "What were you doing before the murder?";
                buttonTextList[2].text = "What were you doing during the night?";
                buttonTextList[3].text = "What were you doing last evening?";
                backButton.gameObject.SetActive(true);
                break;
            case DialogueOptionState.BackstoryQuestions:
                _amountOfEnabledButtons = 2;
                UpdateButtonPositions(_amountOfEnabledButtons);
                buttonTextList[0].text = "Who are you?";
                buttonTextList[1].text = "What are your interests?";
                buttonTextList[2].text = "";
                buttonTextList[3].text = "";
                backButton.gameObject.SetActive(true);
                break;
            
        }
        
    }

    //Sorry for det du kommer til at se nu
    private void UpdateButtonPositions(int amountOfButtons)
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            if (i < amountOfButtons)
            {
                buttonList[i].gameObject.SetActive(true);
            }
            else
            {
                buttonList[i].gameObject.SetActive(false);
            }
        }
        switch (amountOfButtons)
        {
            case 1:
                buttonList[0].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0f, 0f);
                break;
            case 2:
                buttonList[0].GetComponent<RectTransform>().anchoredPosition = new Vector3(-200, -30f, 0f);
                buttonList[1].GetComponent<RectTransform>().anchoredPosition = new Vector3(200f, -30f, 0);
                break;
            case 3:
                buttonList[0].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 90, 0f);
                buttonList[1].GetComponent<RectTransform>().anchoredPosition = new Vector3(200f, -30f, 0);
                buttonList[2].GetComponent<RectTransform>().anchoredPosition = new Vector3(-200, -30f, 0f);
                break;
            case 4:
                buttonList[0].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 90f, 0f);
                buttonList[1].GetComponent<RectTransform>().anchoredPosition = new Vector3(200f, -30f, 0);
                buttonList[2].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -150f, 0);
                buttonList[3].GetComponent<RectTransform>().anchoredPosition = new Vector3(-200f, -30f, 0);
                break;
            case 5:
                buttonList[0].GetComponent<RectTransform>().anchoredPosition = new Vector3(150, 90f, 0f);
                buttonList[1].GetComponent<RectTransform>().anchoredPosition = new Vector3(200f, -30f, 0);
                buttonList[2].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -150f, 0);
                buttonList[3].GetComponent<RectTransform>().anchoredPosition = new Vector3(-200f, -30f, 0);
                buttonList[4].GetComponent<RectTransform>().anchoredPosition = new Vector3(-150f, 90f, 0);
                break;
            case 6:
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

    public void updateClueList(GameObject clueGameobject)
    {
        _clueList.Add(clueGameobject.name);
    }
}
