using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {

    [SerializeField] private GameObject InstructionsScreen;
    [SerializeField] private GameObject[] InstructionsList;
    private int currentScreen = 0;

    public void StartGame() {
        PlayerScoring.lifes = 3;
        PlayerScoring.hearts = 3;
        PlayerScoring.cherries = 0;
        PlayerScoring.gems = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, 
                               LoadSceneMode.Single);
    }

    public void DisplayInstructions() {
        currentScreen = 0;
        InstructionsList[currentScreen].SetActive(true);
        InstructionsScreen.SetActive(true);
    }

    public void HideInstructions() {
        InstructionsScreen.SetActive(false);
        InstructionsList[currentScreen].SetActive(false);
    }

    public void Previous() {
        if (currentScreen == 0) {
            HideInstructions();
        }
        else {
            InstructionsList[currentScreen].SetActive(false);
            currentScreen--;
            InstructionsList[currentScreen].SetActive(true);
        }
    }

    public void Next() {
        if (currentScreen == InstructionsList.Length - 1) {
            HideInstructions();
        }
        else {
            InstructionsList[currentScreen].SetActive(false);
            currentScreen++;
            InstructionsList[currentScreen].SetActive(true);
        }
    }

    public void Exit() {
        Application.Quit();
    }
}
