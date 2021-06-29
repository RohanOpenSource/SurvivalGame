using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float sensitivity;
    [SerializeField] private float clampValue;
    private float xRot;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float mX = Input.GetAxisRaw("Mouse X")*sensitivity;
        float mY = Input.GetAxisRaw("Mouse Y")*sensitivity;
        xRot -= mY;
        xRot = Mathf.Clamp(xRot, -clampValue, clampValue);

        player.Rotate(Vector3.up * mX);
        transform.localEulerAngles = new Vector3(xRot, 0, 0);

    }
}
