using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen = default;
    [SerializeField] private GameObject gameplayInterface = default;

    public static GameManager instance;
    
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        ResetTimeScale();
    }

    public static void ResetTimeScale()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale / 50f;
    }

    public void PauseTheGame()
    {
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0f;
        gameplayInterface.SetActive(false);
        pauseScreen.SetActive(true);
    }

    public void ResumeTheGame()
    {
        ResetTimeScale();
        pauseScreen.SetActive(false);
        gameplayInterface.SetActive(true);
    }
}
