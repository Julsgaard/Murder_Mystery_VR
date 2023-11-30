using OpenAI;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class ChatGPTManager : MonoBehaviour             
{
    [System.Serializable] public class OnResponseEvent : UnityEvent<string> { }
    
    
    //private OpenAIApi openAI = new OpenAIApi();
    private OpenAIApi openAI = new OpenAIApi("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");
    
    
    public async Task<string> AskChatGPT(List<ChatMessage> combinedMessages)
    {
        CreateChatCompletionRequest request = new CreateChatCompletionRequest();    
        request.Messages = combinedMessages;
        request.Model = "gpt-4-1106-preview";
        request.Temperature = 0.8f;
        request.MaxTokens = 256;

        var response = await openAI.CreateChatCompletion(request);

        if(response.Choices != null && response.Choices.Count > 0)
        {
            var chatResponse = response.Choices[0].Message;
            
            return chatResponse.Content;
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
