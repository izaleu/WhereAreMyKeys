using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public static Timer Instance;

    [SerializeField]
    float TimerLength = 30.0f;

    [SerializeField]
    Text TimerDisplay;

    float TimeRemaining = float.MaxValue, TargetTime=0.0f;

    void Awake()
    {
        Instance = this;
        ExpositionText.ExpositionFinished += StartTimer;
    }

	// Use this for initialization
	public void StartTimer () {

        TargetTime = Time.time + TimerLength;
        StartCoroutine(RunTimer(TimerLength));
	}

    IEnumerator RunTimer(float durationInSeconds)
    {
        Debug.Log("Started timer!");
        while (TimeRemaining > 0)
        {

            TimeRemaining = TargetTime - Time.time;
            TimerDisplay.text = string.Format("{0:0}", TimeRemaining);

            yield return new WaitForEndOfFrame();

            
        }
        //yield return new WaitForSeconds(durationInSeconds);
      //  TimerFinished();
    }

    public delegate void EventHandler();

    public static event EventHandler TimerFinished;
}
