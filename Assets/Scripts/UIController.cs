using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour {

    [SerializeField] GameObject pauseMenuPanel;
    [SerializeField] GameObject deathScreenPanel;
    [SerializeField] GameObject winSreenPanel;
    [SerializeField] GameObject startPanel;
    [SerializeField] AudioSource levelMusic;
    [SerializeField] AudioClip winMusic;
    [SerializeField] AudioClip deathMusic;
    [SerializeField] TextMeshProUGUI goalText;
 
    public static bool isDeath, isPaused, isWinner, isStarting;
    // Start is called before the first frame update
    void Start() {
        pauseMenuPanel.SetActive(false);
        deathScreenPanel.SetActive(false);
        winSreenPanel.SetActive(false);
        startPanel.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isDeath = isPaused = isWinner = isStarting = false;
        StarterPanel();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && CanDoThings()) {
            if (isPaused) {
                Resume();
            } else {
                PauseGame();
            }
        }
    }
    public void Resume() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuPanel.SetActive(false);
        isPaused = isStarting = false;
        startPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void PauseGame() {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        pauseMenuPanel.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }
    public void ChangeMode() {
        isDeath = isPaused = isWinner = false;
        Time.timeScale = 1f;
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            SceneManager.LoadScene(1);
        } else {
            SceneManager.LoadScene(0);
        }
    }

    public void QuitGame() {
        Application.Quit();
    }
    public void Retry() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        deathScreenPanel.SetActive(false);
        if(SceneManager.GetActiveScene().buildIndex != 0) {
            FindObjectOfType<GameManager>().ResetPoints();
        }
        isDeath = false;
        Time.timeScale = 1f;
        levelMusic.loop = true;
        if (SceneManager.GetActiveScene().buildIndex == 2) {
            SceneManager.LoadScene(1);
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Death() {
        if (SceneManager.GetActiveScene().buildIndex != 0) {
            FindObjectOfType<GameManager>().ResetPoints();
        }
        levelMusic.Stop();
        levelMusic.clip = deathMusic;
        levelMusic.volume = 1f;
        levelMusic.loop = false;
        levelMusic.Play();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        deathScreenPanel.SetActive(true);
        isDeath = true;
        Time.timeScale = 0f;
    }

    public void Win() {
        levelMusic.Stop();
        levelMusic.clip = winMusic;
        levelMusic.volume = 0.25f;
        levelMusic.Play();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        winSreenPanel.SetActive(true);
        isWinner = true;
        Time.timeScale = 0f;
    }

    public void NextLevel() {
        Time.timeScale = 1f;
        isDeath = isPaused = isWinner = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(2);
    }

    public void StarterPanel() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        isStarting = true;
        startPanel.SetActive(true);
        if(SceneManager.GetActiveScene().buildIndex == 0) {
            goalText.text = "Collect " + FindObjectOfType<GameManager>().GetGoalGold() + " gold!";
        } else {
            goalText.text = "Survive for " + FindObjectOfType<GameManager>().GetTimeToSurvive() + " seconds!";
        }

        Time.timeScale = 0f;
    }

    public static bool CanDoThings() {
        return !isDeath && !isPaused && !isWinner && !isStarting;
    }
}
