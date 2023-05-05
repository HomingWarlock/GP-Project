using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamAction : MonoBehaviour
{
    private GameObject p_object;
    private float m_movementX;
    private float m_movementZ;

    void Start()
    {
        p_object = GameObject.Find("Player");
    }

    private void OnLook(Vector2 CameraRotation)
    {
        Debug.Log(CameraRotation);
        //Vector2 mouseVector = mouseValue.Get<Vector2>();

        //m_movementX = mouseVector.x;
        //m_movementZ = mouseVector.y;
    }

    void FixedUpdate()
    {
        //Vector3 mousemovement = new Vector3(m_movementX, 0.0f, m_movementZ);
        //transform.RotateAround(p_object.transform.position, Vector3.up, mousemovement.x * 1000 * Time.deltaTime);
    }
}
