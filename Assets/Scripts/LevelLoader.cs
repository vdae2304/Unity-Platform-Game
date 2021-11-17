using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    private static GameObject transitionScreen;
    private static GameObject levelClearedScreen;
    private static GameObject levelUnclearedScreen;
    private static GameObject tryAgainScreen;
    private static GameObject gameOverScreen;

    void Start() {
        transitionScreen = transform.Find("Transition").gameObject;
        levelClearedScreen = transform.Find("LevelCleared").gameObject;
        levelUnclearedScreen = transform.Find("LevelUncleared").gameObject;
        tryAgainScreen = transform.Find("TryAgain").gameObject;
        gameOverScreen = transform.Find("GameOver").gameObject;
        PlayerController.hearts = 3;
        PlayerController.gems = 0;
        Time.timeScale = 1f;
        StartCoroutine(CrossfadeIn());
    }

    IEnumerator CrossfadeIn() {
        transitionScreen.SetActive(true);
        transitionScreen.GetComponent<Animator>().SetTrigger("Activate");
        yield return new WaitForSeconds(1.0f);
        transitionScreen.SetActive(false);
    }

    public static void displayLevelClearedScreen() {
        AudioManager.Stop();
        Time.timeScale = 0f;
        levelClearedScreen.SetActive(true);
    }

    public static void displayLevelUnclearedScreen(bool display = true) {
        levelUnclearedScreen.SetActive(display);
    }

    public static void displayTryAgainScreen() {
        AudioManager.Stop();
        Time.timeScale = 0f;
        if (PlayerController.lifes > 0) {
            tryAgainScreen.SetActive(true);
        }
        else {
            gameOverScreen.SetActive(true);
        }
    }

    public void LoadNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerController.lifes--;
    }

    public void Quit() {
        SceneManager.LoadScene(0);
    }
}