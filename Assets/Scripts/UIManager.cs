using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private Image HeartsCounter;
    [SerializeField] private Sprite[] HeartsSprites;
    [SerializeField] private Text LifesCounter;

    [SerializeField] private Text Pause;
    [SerializeField] private GameObject TryAgain;
    [SerializeField] private GameObject GameOver;

    void Start() {
        updateNumberOfHearts(PlayerDamage.hearts);
        updateNumberOfLifes(PlayerDamage.lifes);
        Pause.enabled = false;
        TryAgain.SetActive(false);
        GameOver.SetActive(false);
    }

    void Update() {
        if (!TryAgain.activeSelf && Input.GetButtonDown("Submit")) {
            togglePause();
        }
    }

    public void togglePause() {
        Pause.enabled = !Pause.enabled;
        if (Pause.enabled) {
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
            TryAgain.SetActive(true);
        }
        else {
            GameOver.SetActive(true);
        }
    }
    
    public void updateNumberOfHearts(int hearts) {
        HeartsCounter.sprite = HeartsSprites[hearts];
    }

    public void updateNumberOfLifes(int lifes) {
        LifesCounter.text = lifes.ToString();
    }
}
