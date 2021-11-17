using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField] private Image heartsCounter;
    [SerializeField] private Sprite[] heartsSprites;
    [SerializeField] private Text lifesCounter;
    
    [SerializeField] private Text cherryCounter;
    [SerializeField] private Text gemCounter;
    
    public float timeLimitInSeconds = 120;
    public bool timeIsRunning = true;
    [SerializeField] private Text timeCounter;

    [SerializeField] private GameObject pauseScreen;

    void Update() {
        heartsCounter.sprite = heartsSprites[PlayerController.hearts];
        lifesCounter.text = PlayerController.lifes.ToString();
        cherryCounter.text = PlayerController.cherries.ToString();
        gemCounter.text = PlayerController.gems.ToString();
        if (timeIsRunning) {
            updateTime();
        }
        if (
            Input.GetButtonDown("Pause") && 
            (Time.timeScale != 0 || pauseScreen.activeSelf)
        ) {
            togglePause();
        }
    }

    void updateTime() {
        timeLimitInSeconds -= Time.deltaTime;
        timeCounter.text = Mathf.Ceil(timeLimitInSeconds).ToString();
        if (timeLimitInSeconds <= 30f) {
            timeCounter.color = Color.red;
        }
        if (timeLimitInSeconds <= 0f) {
            timeIsRunning = false;
            LevelLoader.displayTryAgainScreen();
        }
    }

    public void togglePause() {
        pauseScreen.SetActive(!pauseScreen.activeSelf);
        if (pauseScreen.activeSelf) {
            AudioManager.Pause();
            Time.timeScale = 0f;
        }
        else {
            AudioManager.UnPause();
            Time.timeScale = 1f;
        }
    }
}