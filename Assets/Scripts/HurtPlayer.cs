using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

    public int damage = 1;
    [SerializeField] private AudioClip[] damages;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        Random.Range(0, damages.Length);
    }
    public void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            AudioSource.PlayClipAtPoint(damages[Random.Range(0, damages.Length-1)], this.transform.position, 1);
            Vector3 hitDirection = other.transform.position - transform.position;
            hitDirection = hitDirection.normalized;
            FindObjectOfType<HealthManager>().HurtPlayer(damage, hitDirection);
        }
    }

    public void OnTriggerStay(Collider other) {
        //AudioSource.PlayClipAtPoint(damages[Random.Range(0, damages.Length - 1)], this.transform.position, 1);
        Vector3 hitDirection = other.transform.position - transform.position;
        hitDirection = hitDirection.normalized;
        FindObjectOfType<HealthManager>().HurtPlayer(0, hitDirection);
    }

}
