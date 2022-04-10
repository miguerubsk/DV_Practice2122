using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {

    [SerializeField] float health;
    [SerializeField] AudioClip clip;
    [SerializeField] GameObject enemy;
    [SerializeField] float volume;
    [SerializeField] ParticleSystem deathEffect;

    // Start is called before the first frame update
    void Start() {
        enemy = this.gameObject;
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void TakeDamage(float damage) {
        health -= damage;
        enemy.transform.localScale = enemy.transform.localScale / 2;
        if(health <= 0) {
            FindObjectOfType<GameManager>().AddPoints(1);
            ParticleSystem _deathEffect = Instantiate(deathEffect, enemy.transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(clip, enemy.transform.position, volume);
            Destroy(enemy, 0.1f);
            Destroy(_deathEffect, 1f);
        }
    }
}
