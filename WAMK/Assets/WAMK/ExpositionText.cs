﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExpositionText : MonoBehaviour {

    [SerializeField]
    string[] SequentialTexts;

    [SerializeField]
    GameObject SkipButton;

    [SerializeField]
    Color White;

    [SerializeField]
    Color Clear;

    [SerializeField]
    Text DisplayText;

    [SerializeField]
    float FadeDuration = 1.0f;

    float ButtonHeld = 0.0f;

    int CurrentText = -1;

    bool ReadyToFade = true;

    bool CompletedExposition = false;
    
    void Start()
    {
        StartCoroutine(FadeIn(FadeDuration));
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && ReadyToFade==true)
        {
            if (CurrentText == SequentialTexts.Length - 1)
            {
                ExpositionFinished();
                CompletedExposition = true;
                StartCoroutine(FadeOut(FadeDuration));
            }
            else {
                ReadyToFade = false;
                StartCoroutine(FadeOut(FadeDuration));
            }
        }

       if(Input.GetMouseButton(0) && CurrentText>0)
        {
            ButtonHeld += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            ButtonHeld = 0.0f;
        }

        if (ButtonHeld > 1.0f)
        {
            SkipIntro();
        }
    }

    void AdvanceText()
    {
        ++CurrentText;

        if (CurrentText == SequentialTexts.Length)
        {
            CurrentText = 0;
        }

        if (CurrentText == 0)
        {
            SkipButton.SetActive(true);
        }
        else
        {
            SkipButton.SetActive(false);
        }

        DisplayText.text = SequentialTexts[CurrentText];

        StartCoroutine(FadeIn(FadeDuration));
    }

    public void SkipIntro()
    {
        ExpositionFinished();
        CompletedExposition = true;
        StartCoroutine(FadeOut(FadeDuration));
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
        if (CompletedExposition == false) {
            AdvanceText();
        }
        else
        {
            gameObject.SetActive(false);
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
    public delegate void EventHandler();

    public static event EventHandler ExpositionFinished;
}
