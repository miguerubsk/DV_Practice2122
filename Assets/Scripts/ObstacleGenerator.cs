using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour {

    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] Vector3 center;
    [SerializeField] bool useCustomCenter;
    [SerializeField] TerrainData terrain;
    [SerializeField] int radius;
    [SerializeField] int maxObstacles;
    // Start is called before the first frame update
    void Start() {
        if (!useCustomCenter) {
            center = this.transform.position;
        }
        int nObstacles = 0;
        do {
            GenerateObstacle();
        } while (nObstacles++ <= maxObstacles);
    }

    // Update is called once per frame
    void Update() {
        
    }

    private float GetHeightWorldCoords(TerrainData terrainData, Vector2 point) {
        Vector3 scale = terrainData.heightmapScale;
        return (float)terrainData.GetHeight((int)(point.x / scale.x), (int)(point.y / scale.z));
    }

    private void GenerateObstacle() {
        float r = radius * Mathf.Sqrt(Random.value);
        float theta = Random.value * 2 * Mathf.PI;
        Vector2 point = new Vector2((center.x + r * Mathf.Cos(theta)), (center.z + r * Mathf.Sin(theta)));
        Vector3 spawnPosition = new Vector3(point.x, GetHeightWorldCoords(terrain, point), point.y);
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}
