using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveTree : MonoBehaviour
{
    private void OnTriggerStay(Collider other) {
        if(other.tag == "Breakable"){
            Destroy(other.gameObject);
        }
    }
}
