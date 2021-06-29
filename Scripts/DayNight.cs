using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    public Light light;
    [SerializeField] private float speed = 2;
    void Update()
    {
        transform.Rotate(new Vector3(speed*Time.deltaTime,0,0));
    }
}
