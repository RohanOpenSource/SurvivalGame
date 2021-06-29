using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;
    [SerializeField] private float y;
    private float timer;
    private void Update() {
        timer += Time.deltaTime;
        if(timer >= timeBetweenSpawns){
            timer = 0;
            Spawn();
        }
    }

    private void Spawn(){
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);
        Instantiate(enemy, new Vector3(x, y, z), Quaternion.identity);
    }
}
