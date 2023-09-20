using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource playerAudio;
    public AudioClip clickSound;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        PlayerPrefs.SetInt("Particles", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        playerAudio.PlayOneShot(clickSound, 0.3f);
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        playerAudio.PlayOneShot(clickSound, 0.3f);
        Debug.Log("Quit");
        Application.Quit();
    }

    public void TryAgain()
    {
        playerAudio.PlayOneShot(clickSound, 0.3f);
        SceneManager.LoadScene(1);
        Cursor.visible = false;
    }

    public void GoMainMenu()
    {
        playerAudio.PlayOneShot(clickSound, 0.3f);
        SceneManager.LoadScene(0);
        Cursor.visible = true;
    }

    public void GoTutorial()
    {
        playerAudio.PlayOneShot(clickSound, 0.3f);
        SceneManager.LoadScene(3);
        Cursor.visible = true;
    }

    public void ParticlesOn()
    {
        playerAudio.PlayOneShot(clickSound, 0.3f);
        PlayerPrefs.SetInt("Particles", 0);
    }
    public void ParticlesOff()
    {
        playerAudio.PlayOneShot(clickSound, 0.3f);
        PlayerPrefs.SetInt("Particles", 1);
    }

    public void OpenWebsite()
    {
        Application.OpenURL("https://notvector.itch.io/");
    }

}
