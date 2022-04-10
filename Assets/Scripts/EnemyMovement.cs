using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] float speed;
    [SerializeField] Rigidbody enemy;

    // Start is called before the first frame update
    void Start() {
        enemy = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        //enemy.transform.position
        enemy.velocity = new Vector3(0f, 0f, -speed * Time.deltaTime);
    }
}
