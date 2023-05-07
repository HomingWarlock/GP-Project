using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSpin : MonoBehaviour
{
    public GameObject p_object;

    void Start()
    {
        p_object = GameObject.Find("Player");
    }

    void Update()
    {
        transform.RotateAround(p_object.transform.position, Vector3.up, Input.GetAxisRaw("Mouse X") * 1000 * Time.deltaTime);
    }
}
