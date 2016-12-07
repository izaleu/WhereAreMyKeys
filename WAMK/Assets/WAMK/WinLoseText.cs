using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinLoseText: MonoBehaviour {

    [SerializeField]
    string[] WinTexts;

    [SerializeField]
    string[] LoseTexts;

    string[] SequentialTexts;

    [SerializeField]
    Color White;

    [SerializeField]
    Color Clear;

    [SerializeField]
    Text DisplayText;

    [SerializeField]
    float FadeDuration = 1.0f;

    int CurrentText = -1;

    bool ReadyToFade = true;

    bool WinLoseCompleted = false;
    
    void Start()
    {
        if (Application.loadedLevelName == "Win")
        {
            SequentialTexts = WinTexts;
        }
        else
        {
            SequentialTexts = LoseTexts;
        }
        StartCoroutine(FadeIn(FadeDuration));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ReadyToFade==true)
        {
            if (CurrentText == SequentialTexts.Length - 1)
            {
                WinLoseCompleted = true;
                StartCoroutine(FadeOut(FadeDuration));
            }
            else {
                ReadyToFade = false;
                StartCoroutine(FadeOut(FadeDuration));
            }
        }
    }

    void AdvanceText()
    {
        ++CurrentText;

        if (CurrentText == SequentialTexts.Length)
        {
            CurrentText = 0;
        }

        DisplayText.text = SequentialTexts[CurrentText];

        StartCoroutine(FadeIn(FadeDuration));
    }

    IEnumerator FadeOut(float duration)
    {
        float TargetTime = Time.time + duration;

        while (DisplayText.color.a != 0.0f)
        {
     //       Debug.Log("Fading text to Clear");
            DisplayText.color = Color.Lerp(DisplayText.color, Clear, 1 - (TargetTime - Time.time) / duration);
            yield return new WaitForEndOfFrame();
        }
        if (WinLoseCompleted == false) {
            AdvanceText();
        }
        else
        {
            Application.LoadLevel(0);
        }
    }

    IEnumerator FadeIn(float duration)
    {
        float TargetTime = Time.time + duration;

        while (DisplayText.color.a != 1.0f)
        {
    //        Debug.Log("Fading in text to White");
            DisplayText.color = Color.Lerp(DisplayText.color, White, 1 - (TargetTime - Time.time) / duration);
            yield return new WaitForEndOfFrame();
        }
        
        ReadyToFade = true;
    }
}
