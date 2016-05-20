using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    [SerializeField]
    Key[] Keys;
    
    void Awake()
    {
        Instance = this;
        Keys = FindObjectsOfType<Key>();

        Timer.TimerFinished += Lose;
        Timer.TimerStopped += Win;
    }

    void Start()
    {
        SpawnKey();
    }

    public void SpawnKey()
    {
        int random = Random.Range(0, Keys.Length - 1);

        for (int i = 0; i < Keys.Length; ++i)
        {
            if (i != random)
            {
                Keys[i].gameObject.SetActive(false);
            }
        }
    }

    public void Win()
    {
        Application.LoadLevel(1);
    }

    public void Lose()
    {
        Application.LoadLevel(2);
    }
}
