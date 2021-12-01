using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    private static GameObject transitionScreen;

    public int gemsToPassLevel = 0;
    public static int gemsRequired;

    private static GameObject levelClearedScreen;
    private static GameObject tryAgainScreen;
    private static GameObject gameOverScreen;
    private static Text tryAgainText;

    void Awake() {
        PlayerController.hearts = 3;
        PlayerController.gems = 0;
        Time.timeScale = 1f;
    }

    void Start() {
        transitionScreen = transform.Find("Transition").gameObject;
        gemsRequired = gemsToPassLevel;
        levelClearedScreen = transform.Find("LevelCleared").gameObject;
        tryAgainScreen = transform.Find("TryAgain").gameObject;
        gameOverScreen = transform.Find("GameOver").gameObject;
        tryAgainText = tryAgainScreen.GetComponentInChildren<Text>();
        StartCoroutine(CrossfadeIn());
    }

    IEnumerator CrossfadeIn() {
        transitionScreen.SetActive(true);
        transitionScreen.GetComponent<Animator>().SetTrigger("Activate");
        yield return new WaitForSeconds(1.0f);
        transitionScreen.SetActive(false);
    }

    public static IEnumerator displayLevelClearedScreen() {
        if (PlayerController.gems >= gemsRequired) {
            AudioManager.Stop();
            AudioManager.PlayOneShot(AudioManager.stageClearSound);
            yield return new WaitForSeconds(7f);
            Time.timeScale = 0f;
            levelClearedScreen.SetActive(true);
        }
        else {
            displayTryAgainScreen("Diamantes insuficientes");
        }
    }

    public static void displayTryAgainScreen(string text) {
        AudioManager.Stop();
        Time.timeScale = 0f;
        if (PlayerController.lifes > 0) {
            tryAgainText.text = text;
            tryAgainScreen.SetActive(true);
        }
        else {
            gameOverScreen.SetActive(true);
        }
    }

    public void LoadNextLevel() {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevel == SceneManager.sceneCountInBuildSettings) {
            nextLevel = 0;
        }
        SceneManager.LoadScene(nextLevel);
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerController.lifes--;
    }

    public void Quit() {
        SceneManager.LoadScene(0);
    }
}