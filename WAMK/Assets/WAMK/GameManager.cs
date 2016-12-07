using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    [SerializeField]
    Key[] Keys;

    string WinSceneName = "Win", LoseSceneName = "Lose";

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
        SceneManager.LoadScene(WinSceneName);
    }

    public void Lose()
    {
        Application.LoadLevel(LoseSceneName);
    }
}
