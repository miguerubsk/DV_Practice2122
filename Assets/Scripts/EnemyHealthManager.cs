using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {

    [SerializeField] float health;
    [SerializeField] AudioClip clip;
    [SerializeField] float volume;
    [SerializeField] ParticleSystem deathEffect;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start() {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void TakeDamage(float damage) {
        health -= damage;
        transform.localScale = transform.localScale / 2;
        if(health <= 0) {
            gameManager.AddPoints(1);
            ParticleSystem _deathEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
            Destroy(this.gameObject, 0.1f);
            Destroy(_deathEffect, 1f);
        }
    }
}
