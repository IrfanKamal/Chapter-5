using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // Variables
    public GameObject pausePanel;
    public GameObject resumeBtn;
    public GameObject levelClearTxt;
    private Scene currActiveScene;

    // Start is called before the first frame update
    void Start()
    {
        currActiveScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currActiveScene.name);
    }

    public void EndGame()
    {
        pausePanel.SetActive(true);
        resumeBtn.SetActive(false);
        levelClearTxt.SetActive(true);
    }
}
