using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {

    public Image HeartsCounter;
    public Sprite[] HeartsSprites;
    public TextMeshProUGUI LivesCounter;

    public void updateNumberOfHearts(int hearts) {
        HeartsCounter.sprite = HeartsSprites[hearts];
    }

    public void updateNumberOfLives(int lives) {
        LivesCounter.text = lives.ToString();
    }
}
