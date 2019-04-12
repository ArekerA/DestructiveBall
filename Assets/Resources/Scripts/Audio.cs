using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Audio : MonoBehaviour
{
    public AudioClip clipMenu;
    public AudioClip[] clips;
    private AudioClip clipA;
    private float volume = 0;
    private AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
        audioSource.Play();
    }
    public void ChangeAudio()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            clipA = clipMenu;
        }
        else
        {
            clipA = clips[(int)(Random.value * clips.Length)];
        }
        audioSource.loop = true;
        audioSource.volume = 1.0f;
        StartCoroutine(PlayAudio(clipA));
    }

    IEnumerator PlayAudio(AudioClip clip)
    {
        while (volume > 0)
        {
            volume -= 0.05f;
            audioSource.volume = volume < 0 ? 0 : volume;
            yield return new WaitForSecondsRealtime(0.05f);
        }
        audioSource.clip = clip;
        audioSource.Play();
        while (volume < 1)
        {
            volume += 0.05f;
            audioSource.volume = volume > 1 ? 1 : volume;
            yield return new WaitForSecondsRealtime(0.05f);
        }

    }
}
