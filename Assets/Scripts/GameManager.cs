using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {

    int currentGold;
    public Text goldText, time, lifes;
    [SerializeField] float goalGoldPercent;
    [SerializeField] int timeToSurvive;
    int goalGold;
    bool once;
    private static int points;

    // Start is called before the first frame update
    void Start() {
        if(SceneManager.GetActiveScene().buildIndex == 0) {
            currentGold = 0;
            goalGoldPercent /= 100;
            goalGold = (int)(FindObjectOfType<GoldGenerator>().GetMaxGold() * goalGoldPercent);
        } else {
            once = true;
            if (SceneManager.GetActiveScene().name == "FPS") {
                points = 0;
                timeToSurvive = 30;
            } else {
                timeToSurvive = 60;
                currentGold = points;
                goldText.text = "Points: " + currentGold;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            time.text = "Time: " + (int)Time.timeSinceLevelLoad;
        } else {
            time.text = "Time left: " + (int)(timeToSurvive - Time.timeSinceLevelLoad);
            if ((int)Time.timeSinceLevelLoad >= timeToSurvive) {
                points = currentGold;
                if (once) {
                    FindObjectOfType<UIController>().Win();
                    once = false;
                }
            }
        }

    }

    public void AddGold(int goldToAdd) {
        this.currentGold += goldToAdd;
        goldText.text = "Gold: " + currentGold;
        if(goalGold == currentGold) {
            FindObjectOfType<UIController>().Win();
        }

    }

    public void AddPoints(int pointsToAdd) {
        currentGold += pointsToAdd;
        goldText.text = "Points: " + currentGold;
        points = currentGold;
    }

    public void Lifes(int lifes) {
        this.lifes.text = "Lifes: " + lifes;
    }

    public int GetGoalGold() => goalGold;
    public int GetPoints() => points;

    public int GetTimeToSurvive() => timeToSurvive;

    public void ResetPoints() {
        points = 0;
    }
}
