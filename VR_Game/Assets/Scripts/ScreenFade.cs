using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenFade : MonoBehaviour
{
    //[SerializeField] private GameManager gameManager;
    
    [SerializeField] private Image fadeImage;
    [SerializeField] private TextMeshProUGUI fadeText;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float fadeDurationText;
    [SerializeField] private float delayTimeText;
    
    

    public IEnumerator FadeToBlack()
    {
        float elapsedTime = 0.0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }

    public IEnumerator FadeInText(string endText)
    {
        yield return new WaitForSeconds(delayTimeText);
        
        float elapsedTime = 0.0f;
        Color textColor = fadeText.color;
        textColor.a = 0;
        fadeText.color = textColor;
        fadeText.text = endText;

        while (elapsedTime < fadeDurationText)
        {
            elapsedTime += Time.deltaTime;
            textColor.a = Mathf.Clamp01(elapsedTime / fadeDurationText);
            fadeText.color = textColor;
            yield return null;
        }
    }
}