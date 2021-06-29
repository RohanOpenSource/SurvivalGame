using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWithGround : MonoBehaviour
{
    [SerializeField] private LayerMask ground;
    [SerializeField] private float downwardsOffset;
    private float radius;
    [SerializeField] private bool removeUnderWater;
     
     void Start () {
         radius = 1;
         
         // raycast to find the y-position of the masked collider at the transforms x/z
         RaycastHit hit;
         // note that the ray starts at 100 units
         Ray ray = new Ray (transform.position - Vector3.up * 20, Vector3.down);
         
         if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground)) {        
             if (hit.collider != null && hit.collider != gameObject.GetComponent<Collider>()) {
                 // this is where the gameobject is actually put on the ground
                 transform.position = new Vector3 (transform.position.x, hit.point.y + radius + downwardsOffset, transform.position.z);
                 if(transform.position.y <= 6 && removeUnderWater) Destroy(gameObject);
                }
            }
     }
}
