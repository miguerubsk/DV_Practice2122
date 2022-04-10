using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour {

    [SerializeField] AudioClip clip;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void OnTriggerEnter(Collider other) {
        if(SceneManager.GetActiveScene().buildIndex == 0) {
            if (other.tag == "Player") {
                AudioSource.PlayClipAtPoint(clip, FindObjectOfType<PlayerControler>().transform.position, 1);
                FindObjectOfType<UIController>().Death();
            }
        } else {
            if(other.tag == "Enemy") {
                //AudioSource.PlayClipAtPoint(clip, FindObjectOfType<FPSController>().transform.position, 1);
                FindObjectOfType<UIController>().Death();
            }
        }
    }
}
