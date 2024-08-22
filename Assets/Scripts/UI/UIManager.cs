using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("GameOver")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    [Header("Victory")]
    [SerializeField] private GameObject victoryScreen;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        if (victoryScreen != null)
            victoryScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //if pause screen already active unpause and viceversa
            if (pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }

    public bool IsPauseOrVictoryScreenActive()
    {
        return pauseScreen.activeInHierarchy || victoryScreen.activeInHierarchy;
    }

    #region Game Over

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();


    }
    #endregion

    #region Pause
    public void PauseGame(bool status)
    {
        //if status == true pause | if statue == false unpause
        pauseScreen.SetActive(status);

        //When pause status is true change timescale to 0 | When false change back to 1
        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
    #endregion

    #region Victory
    public void Victory()
    {
        victoryScreen.SetActive(true);
        Time.timeScale = 0;
    }
    #endregion
}
