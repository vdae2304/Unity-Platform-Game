using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    [SerializeField] private Image heartsCounter;
    [SerializeField] private Sprite[] heartsSprites;
    [SerializeField] private Text lifesCounter;
    [SerializeField] private Text cherryCounter;
    [SerializeField] private Text gemCounter;
    
    public float timeLimitInSeconds = 120;
    public bool timeIsRunning = true;
    [SerializeField] private Text timeCounter;

    private GameObject pauseScreen;
    private GameObject tryAgainScreen;
    private GameObject gameOverScreen;

    void Start() {
        pauseScreen = transform.Find("Pause").gameObject;
        tryAgainScreen = transform.Find("TryAgain").gameObject;
        gameOverScreen = transform.Find("GameOver").gameObject;
    }

    void Update() {
        heartsCounter.sprite = heartsSprites[PlayerScoring.hearts];
        lifesCounter.text = PlayerScoring.lifes.ToString();
        cherryCounter.text = PlayerScoring.cherries.ToString();
        gemCounter.text = PlayerScoring.gems.ToString();

        if (timeIsRunning) {
            timeLimitInSeconds -= Time.deltaTime;
            timeCounter.text = Mathf.Ceil(timeLimitInSeconds).ToString();
            if (timeLimitInSeconds <= 30f) {
                timeCounter.color = Color.red;
            }
            if (timeLimitInSeconds <= 0f) {
                timeIsRunning = false;
                displayTryAgainScreen();
            }
        }

        if (
            Input.GetButtonDown("Submit") && 
            !tryAgainScreen.activeSelf && 
            !gameOverScreen.activeSelf
        ) {
            togglePause();
        }
    }

    public void togglePause() {
        pauseScreen.SetActive(!pauseScreen.activeSelf);
        if (pauseScreen.activeSelf) {
            AudioManager.audioSource.Pause();
            Time.timeScale = 0f;
        }
        else {
            AudioManager.audioSource.UnPause();
            Time.timeScale = 1f;
        }
    }

    public void displayTryAgainScreen() {
        AudioManager.audioSource.Stop();
        Time.timeScale = 0f;
        if (PlayerScoring.lifes > 0) {
            tryAgainScreen.SetActive(true);
        }
        else {
            gameOverScreen.SetActive(true);
        }
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerScoring.lifes--;
        Time.timeScale = 1f;
    }

    public void Quit() {
        Application.Quit();
    }
}
