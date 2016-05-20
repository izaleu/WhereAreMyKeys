using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class FadeController : MonoBehaviour {

    [SerializeField]
    GameObject FadeBox;

    Image[] Screens;

    [SerializeField]
    Color Black;

    [SerializeField]
    Color Clear;

    void Start()
    {
        ExpositionText.ExpositionFinished += StartFadeIn;
        Screens = FadeBox.GetComponentsInChildren<Image>();
    }

    IEnumerator FadeIn(float duration)
    {
        bool fading = true;
        float TargetTime = Time.time + duration;
        
        while (fading==true)
        {
            // Debug.Log("Fading");

            foreach (Image Screen in Screens)
            {
                Screen.color = Color.Lerp(Screen.color, Clear, 1 - (TargetTime - Time.time) / duration);
                yield return new WaitForEndOfFrame();
            }
            if(Screens[Screens.Length-1].color.a == 0.0f)
            {
                fading = false;
            }
            
        }
    }

    IEnumerator FadeOut(float duration)
    {
        yield return new WaitForSeconds(duration);
    }

    void StartFadeIn()
    {
        StartCoroutine(FadeIn(1.0f));
    }

    void OnDestroy()
    {
        ExpositionText.ExpositionFinished -= StartFadeIn;
    }
}
