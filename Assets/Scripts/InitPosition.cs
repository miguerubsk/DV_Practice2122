using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPosition : MonoBehaviour {
    public GameObject player;
    bool fisrtExecution = true;
    // Start is called before the first frame update
    void Start() {
        player.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update(){
        if (fisrtExecution) {
            player.transform.position = transform.position;
            fisrtExecution = false;
        }
    }

    public void Reset() {
        player.transform.position = transform.position;
    }
}
