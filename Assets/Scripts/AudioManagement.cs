using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManagement : MonoBehaviour
{
    [SerializeField] 
    [Header("Plays during splash screen")]
    private AudioClip splashMusic;
    [SerializeField]
    [Header("Plays in main menu, level and character select")]
    private AudioClip menuMusic;
    [SerializeField]
    [Header("Plays in the credit scene")]
    private AudioClip creditMusic;
    [SerializeField]
    [Header("Array of songs that play randomly during match")]
    private AudioClip[] levelMusic;

    private AudioSource audioSource;
    private string currentSceneClass;

    private void Awake()
    {
        // Looks for another audio manager in the scene and deletes the duplicate
        if (FindObjectsOfType<AudioManagement>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        SceneManager.sceneLoaded += this.OnLevelFinishedLoading;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentSceneClass = LevelManager.SceneClass();
    }

    // Called when the scene finishes loading
    void OnLevelFinishedLoading (Scene scene, LoadSceneMode mode)
    {
        /* Checks to see if the scene loaded is the same class
           If it isn't, plays new music and updates the local scene class */
        if (LevelManager.SceneClass() != currentSceneClass)
        {
            SelectMusic(LevelManager.SceneClass());
            currentSceneClass = LevelManager.SceneClass();
        }
    }

    public void StartMusic ()
    {
        audioSource.Play();
    }

    public void StopMusic ()
    {
        audioSource.Stop();
    }

    public void SelectMusic (string clip)
    {
        switch (clip)
        {
            case "Menu":
                audioSource.clip = menuMusic;
                StartMusic();
                break;
            case "Credits":
                audioSource.clip = creditMusic;
                StartMusic();
                break;
            case "Level":
                SelectMusicRandom();
                StartMusic();
                break;
            case "Splash":
                audioSource.clip = splashMusic;
                StartMusic();
                break;
        }
    }

    public void SelectMusicRandom ()
    {
        audioSource.clip = levelMusic[Random.Range(0, levelMusic.Length)];
    }

}
