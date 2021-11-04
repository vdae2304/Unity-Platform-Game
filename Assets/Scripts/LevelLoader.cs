using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    [SerializeField] private GameObject levelUnclearedScreen;
    [SerializeField] private GameObject levelClearedScreen;
    [SerializeField] private GameObject transitionScreen;

    void Start() {
        PlayerScoring.hearts = 3;
        PlayerScoring.cherries = 0;
        PlayerScoring.gems = 0;
        StartCoroutine(CrossfadeIn());
    }

    IEnumerator CrossfadeIn() {
        transitionScreen.SetActive(true);
        transitionScreen.GetComponent<Animator>().SetTrigger("Activate");
        yield return new WaitForSeconds(1.0f);
        transitionScreen.SetActive(false);
    }

    public void displayLevelUnclearedScreen(bool display = true) {
        levelUnclearedScreen.SetActive(display);
    }

    public void displayLevelClearedScreen() {
        AudioManager.audioSource.Stop();
        levelClearedScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LoadNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, 
                               LoadSceneMode.Single);
        Time.timeScale = 1f;
    }
}
