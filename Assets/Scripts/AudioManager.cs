using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioSource audioSource;
    public static AudioClip jumpSound;
    public static AudioClip stageClearSound;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        jumpSound = Resources.Load<AudioClip>("Audio/smrpg_jump");
        stageClearSound = Resources.Load<AudioClip>("Audio/mega-man-10-stage-clear");
    }

    public static void Play() {
        audioSource.Play();
    }

    public static void Pause() {
        audioSource.Pause();
    }

    public static void UnPause() {
        audioSource.UnPause();
    }
    
    public static void Stop() {
        audioSource.Stop();
    }

    public static void PlayOneShot(AudioClip clip) {
        audioSource.PlayOneShot(clip);
    }
}