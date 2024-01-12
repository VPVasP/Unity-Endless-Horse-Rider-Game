using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject lostPanel; //panel when we lose
    private AudioSource aud;
    public AudioSource mainMusic;
    [SerializeField] private AudioClip cheer;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //we add and assign the audiosource also we change the settings of the audiosource
        if (aud == null)
        {
            gameObject.AddComponent<AudioSource>();
        }
        aud = GetComponent<AudioSource>();
        aud.playOnAwake = false;
        aud.loop = false;
        aud.volume = 0.3f;
        lostPanel.SetActive(false);
    }
    //we enable our lose panel
   public void LostPanel()
    {
        lostPanel.SetActive(true);

    }
    //we play the cheer audioclip and set the volume for both the clip and the mainmusic
    public void PlayCheerSound()
    {
        aud.clip = cheer;
        aud.volume = 0.3f;
        mainMusic.volume = 0.5f;
        aud.Play();
    }
    //we stop the cheer audioclip and set the volume for the mainmusic
    public void StopCheerSound()
    {
        aud.Stop();
        mainMusic.volume = 1f;
    }
    //we restart our game
    public void RestartGame()
    {
      Scene scene = SceneManager.GetActiveScene();
      SceneManager.LoadScene(scene.name);
    }
}
