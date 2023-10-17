using LMNT;
using OpenAI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChatGPTManager : MonoBehaviour             
{
    [System.Serializable]
    public class OnResponseEvent : UnityEvent<string> { }

    public OnResponseEvent onResponse;

    public LMNTSpeech speech;






    private OpenAIApi openAI = new OpenAIApi();
    //private OpenAIApi openAI = new OpenAIApi("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");
    private List<ChatMessage> messages = new List<ChatMessage>();

    private string GetDefaultPrompt()
    {
        return "Answer the following question as if you were a medieval peasant, and in no longer than 20 words: ";
    }

    public async Task<string> AskChatGPT(string npcPrompt, string transcribedText)
    {
        ChatMessage systemMessage = new ChatMessage();
        systemMessage.Content = npcPrompt;
        systemMessage.Role = "system";   
        
        ChatMessage playerMessage = new ChatMessage();
        playerMessage.Content = transcribedText;
        playerMessage.Role = "user";  
        
        messages.Add(systemMessage);
        messages.Add(playerMessage);   

        CreateChatCompletionRequest request = new CreateChatCompletionRequest();    
        request.Messages = messages;
        request.Model = "gpt-3.5-turbo";
        

        var response = await openAI.CreateChatCompletion(request);

        if(response.Choices != null && response.Choices.Count > 0)
        {
            var chatResponse = response.Choices[0].Message;
            //messages.Add(chatResponse);
            
            //Clearing the message list is the easier option as the openAI.CreateChatCompletion requires a list of messages
            messages.Clear();
            
            return chatResponse.Content;

            //onResponse.Invoke(chatResponse.Content);

            //LMNT SPEECH STUFF
            //speech.dialogue = chatResponse.Content;
            //StartCoroutine(speech.Talk());
            //responseField.text = chatResponse.Content;
        }

        return null;
    }

    public async Task<string> TranscribeAudioAndGetText(byte[] audio)
    {
        var req = new CreateAudioTranscriptionsRequest
        {
            FileData = new FileData() { Data = audio, Name = "audio.wav" },
            Model = "whisper-1",
            Language = "en"
        };

        var res = await openAI.CreateAudioTranscription(req);

        // Return the transcribed text
        return res.Text;
    }








}
