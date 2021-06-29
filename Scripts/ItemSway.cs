using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSway : MonoBehaviour
{
    [SerializeField] private float amount;
    [SerializeField] private float smoothAmount;
    private Vector3 startingPos;
    private void Start() {
        startingPos = transform.localPosition;
    }
    private void Update() {
        float mX = Input.GetAxisRaw("Mouse X") * amount;
        float mY = Input.GetAxisRaw("Mouse Y") * amount;

        Vector3 desiredPos = new Vector3(mX, mY, 0) + startingPos;
        transform.localPosition = Vector3.Lerp(transform.localPosition, desiredPos, smoothAmount * Time.deltaTime);
    }
}
