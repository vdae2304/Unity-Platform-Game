using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    private PlayerDamage player;

    void Start() {
        player = GameObject.Find("player").GetComponent<PlayerDamage>();
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        player.increaseNumberOfLifes(-1);
        Time.timeScale = 1f;
    }

    public void Quit() {
        Application.Quit();
    }
}
