using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePrefabs : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float numberOfPrefabs;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;
    [SerializeField] private float y;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private bool randomRot = true;
    void Start()
    {
        for(int i = 0; i < numberOfPrefabs; i++){
            float x = Random.Range(minX, maxX);
            float z = Random.Range(minZ, maxZ);
            if(randomRot)rotation.z = Random.Range(0, 360);
            Instantiate(prefab, new Vector3(x,y,z), Quaternion.Euler(rotation)).transform.SetParent(transform);
        }
    }

}
