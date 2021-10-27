using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private Image heartsCounter;
    [SerializeField] private Sprite[] heartsSprites;
    [SerializeField] private Text lifesCounter;

    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject tryAgainScreen;
    [SerializeField] private GameObject gameOverScreen;

    void Update() {
        heartsCounter.sprite = heartsSprites[PlayerDamage.hearts];
        lifesCounter.text = PlayerDamage.lifes.ToString();
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
            audioSource.Pause();
            Time.timeScale = 0f;
        }
        else {
            audioSource.UnPause();
            Time.timeScale = 1f;
        }
    }

    public void displayTryAgainScreen() {
        audioSource.Stop();
        Time.timeScale = 0f;
        if (PlayerDamage.lifes > 0) {
            tryAgainScreen.SetActive(true);
        }
        else {
            gameOverScreen.SetActive(true);
        }
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerDamage.lifes--;
        PlayerDamage.hearts = 3;
        Time.timeScale = 1f;
    }

    public void Quit() {
        Application.Quit();
    }
}
