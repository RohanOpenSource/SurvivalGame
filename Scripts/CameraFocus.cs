using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraFocus : MonoBehaviour
{
    public Volume volume;
    private DepthOfField d;
    [SerializeField] private LayerMask ground;
    private void Start() {
        volume.profile.TryGet<DepthOfField>(out d);
    }

    void Update()
    {
       RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit, ground, 1000);
        float targetDistance;
        if (hit.collider != null)
        {
            targetDistance = hit.distance;
        }
        else
        {
            targetDistance = 10f;
        }
        d.focusDistance.SetValue(new NoInterpMinFloatParameter(targetDistance, 0, true));
        
    }
    
}
