using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpdate : MonoBehaviour
{
    [SerializeField] private MapGenerator generator;
    [SerializeField] private MeshCollider collider;
    [SerializeField] private MeshFilter filter;
    void Start()
    {
        generator.seed = Random.Range(0,30000);
        generator.GenerateMap();
        collider.sharedMesh = null;
        collider.sharedMesh = filter.mesh;
    }
}
