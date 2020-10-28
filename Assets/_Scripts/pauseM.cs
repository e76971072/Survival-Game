using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseM : MonoBehaviour
{
    public static bool GameIsPause = false;
    public GameObject pauseMenuUI;
    public GameObject music;
    public GameObject death;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;
                Resume();
            }
            else
            {
                music.GetComponent<AudioSource>().Pause();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Pause();
            }
        }

        
    }

    void Awake()
    {
        if (GameIsPause)
        {
            Debug.Log("game is paused");
            Time.timeScale = 1f;
        }
    }

    public void Resume()
    {
        music.GetComponent<AudioSource>().UnPause();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
