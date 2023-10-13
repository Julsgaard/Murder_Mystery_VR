using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using UnityEngine.Events;
using TMPro;
using System;
using UnityEngine.UI;
using LMNT;

public class ChatGPTManagerOLD : MonoBehaviour             
{
    [System.Serializable]
    public class OnResponseEvent : UnityEvent<string> { }

    public OnResponseEvent onResponse;
    

    [SerializeField] private Button recordButton;
    [SerializeField] private Image progressBar;
    [SerializeField] private TMP_InputField message;
    [SerializeField] private Dropdown dropdown;

    public LMNTSpeech speech;



    private readonly string fileName = "output.wav";
    private readonly int duration = 5;

    private AudioClip clip;
    private bool isRecording;
    private float time;



    private OpenAIApi openAI = new OpenAIApi();
    //private OpenAIApi openAI = new OpenAIApi("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");
    private List<ChatMessage> messages = new List<ChatMessage>();

    private string GetPrompt()
    {
        return "Answer the following question as if you were a mediaval peasant, and in no longer than 20 words: ";
    }

    public async void AskChatGPT(string newText)
    {
        ChatMessage newMessage = new ChatMessage();
        newMessage.Content = GetPrompt() + newText;
        newMessage.Role = "user";   

        messages.Add(newMessage);   

        CreateChatCompletionRequest request = new CreateChatCompletionRequest();    
        request.Messages = messages;
        request.Model = "gpt-3.5-turbo";
        

        var response = await openAI.CreateChatCompletion(request);

        if(response.Choices != null && response.Choices.Count > 0)
        {
            var chatResponse = response.Choices[0].Message;
            messages.Add(chatResponse);

            Debug.Log(chatResponse.Content);

            onResponse.Invoke(chatResponse.Content);
            speech.dialogue = chatResponse.Content;
            StartCoroutine(speech.Talk());
            //responseField.text = chatResponse.Content;
        }
    }

    private void Start()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
            dropdown.options.Add(new Dropdown.OptionData("Microphone not supported on WebGL"));
        #else
        foreach (var device in Microphone.devices)
        {
            dropdown.options.Add(new Dropdown.OptionData(device));
        }
        recordButton.onClick.AddListener(StartRecording);
        dropdown.onValueChanged.AddListener(ChangeMicrophone);

        var index = PlayerPrefs.GetInt("user-mic-device-index");
        dropdown.SetValueWithoutNotify(index);
        #endif
    }
    private void ChangeMicrophone(int index)
    {
        PlayerPrefs.SetInt("user-mic-device-index", index);
    }

    private void StartRecording()
    {
        isRecording = true;
        recordButton.enabled = false;

        var index = PlayerPrefs.GetInt("user-mic-device-index");

        #if !UNITY_WEBGL
        clip = Microphone.Start(dropdown.options[index].text, false, duration, 44100);
        #endif
    }

    private async void EndRecording()
    {
        message.text = "Transcribing...";
        
        
        #if !UNITY_WEBGL
        Microphone.End(null);
        #endif

        byte[] data = SaveWav.Save(fileName, clip);

        var req = new CreateAudioTranscriptionsRequest
        {
            FileData = new FileData() { Data = data, Name = "audio.wav" },
            // File = Application.persistentDataPath + "/" + fileName,
            Model = "whisper-1",
            Language = "en"
        };
        var res = await openAI.CreateAudioTranscription(req);

        progressBar.fillAmount = 0;
        message.text = res.Text;
        recordButton.enabled = true;

        AskChatGPT(res.Text);
    }
    private void Update()
    {
        if (isRecording)
        {
            time += Time.deltaTime;
            progressBar.fillAmount = time / duration;

            if (time >= duration)
            {
                time = 0;
                isRecording = false;
                EndRecording();
            }
        }
    }






}
