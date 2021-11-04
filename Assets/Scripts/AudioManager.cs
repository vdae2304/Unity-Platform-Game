using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

    public static AudioSource audioSource;
    public static AudioClip jump;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        jump = Resources.Load<AudioClip>("Audio/smrpg_jump");
    }
}
