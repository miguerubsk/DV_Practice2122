using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour {

    public int currentHealth, maxHealth;

    public PlayerControler player;
    public float invincibilityTime, flashTime = 0.1f;
    private float invincibilityCounter, flashCounter;
    public Renderer playerRenderer;
    private bool isRespawnig;
    public GameObject respawnPoint/* = new Vector3(344.8014f, 4.57f, 327.936f)*/;

    // Start is called before the first frame update
    void Start() {
        currentHealth = maxHealth;
        FindObjectOfType<GameManager>().Lifes(currentHealth);
        //player = FindObjectOfType<PlayerControler>();
    }

    // Update is called once per frame
    void Update() {
        if(invincibilityCounter > 0) {
            invincibilityCounter -= Time.deltaTime;
            flashCounter -= Time.deltaTime;
            if(flashCounter <= 0) {
                playerRenderer.enabled = !playerRenderer.enabled;

            }
            if (invincibilityCounter <= 0) {
                playerRenderer.enabled = true;
            }
        }
    }

    public void HurtPlayer(int damage, Vector3 direction) {
        if(invincibilityCounter <= 0) {
            currentHealth -= damage;
            FindObjectOfType<GameManager>().Lifes(currentHealth);
            if (currentHealth <= 0) {
                Debug.Log("MUERTE");
                Respawn();
            } else {
                player.KnockBack(direction);
                invincibilityCounter = invincibilityTime;
                playerRenderer.enabled = false;
                flashCounter = flashTime;
            }
        }
    }

    public void HealPlayer(int healAmount) {
        currentHealth += healAmount;
        if(currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }

    public void Respawn() {
        FindObjectOfType<UIController>().Death();
        //player.transform.position = respawnPoint.transform.position;
        Debug.Log("MUERTE 2");
        currentHealth = maxHealth;
    }

}
