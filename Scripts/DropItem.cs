using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private GameObject drop;
    private Vector3 position;
    private void Start() {
        position = transform.position;
    }
    private void OnDestroy() {
        Instantiate(drop, position+new Vector3(0,2,0), Quaternion.identity);
        Debug.Log("Dropped");
    }
}
