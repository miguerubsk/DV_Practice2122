using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {
    [SerializeField] GameObject enemy;
    [SerializeField] int enemyCount, nEnemigos;
    [SerializeField] float espera;
    [SerializeField] GameObject rangeL, rangeR;
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(EnemyDrop());
    }

    // Update is called once per frame
    void Update() {
        
    }

    IEnumerator EnemyDrop() {
        while (enemyCount++ <= nEnemigos) {
            for (int i = 0; i < nEnemigos; i++) {
                float xpos = Random.Range(rangeL.transform.position.x, rangeR.transform.position.x);
                Instantiate(enemy, new Vector3(xpos, rangeR.transform.position.y, rangeL.transform.position.z), Quaternion.identity);
            }
            yield return new WaitForSeconds(espera);

        }
    }
}
