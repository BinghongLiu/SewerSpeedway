using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheeseDuplication : MonoBehaviour
{
    public GameObject cheese;
    
    public float spawnInterval;
    public float spawnRadius;
    public float spawnTimer;
    public bool cloned;

    void Start() {
        cloned = false;
    }

    void Update() {
        if (cloned == false) {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0f) {
                spawnTimer = spawnInterval;
                CreateCheese();
            }
        } else {
            cheese.SetActive(true);
            spawnTimer = spawnInterval;
        }
    }

    private void CreateCheese() {
        Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
        randomPosition.y = 0;
        float randomRotationY = Random.Range(0f, 360f); 
        float randomRotationZ = Random.Range(0f, 360f); 
        GameObject cheeseClone = Instantiate(gameObject, transform.position + randomPosition, Quaternion.Euler(0, randomRotationY, randomRotationZ));
        cloned = true;
    }

}
