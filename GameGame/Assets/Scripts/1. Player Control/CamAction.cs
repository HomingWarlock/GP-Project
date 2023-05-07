using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAction : MonoBehaviour
{
    private GameObject c_cam_point;
    private float c_pos_lerp;
    private float c_rot_lerp;

    private void Start()
    {
        c_cam_point = GameObject.Find("CamPoint");
        c_pos_lerp = 1;
        c_rot_lerp = 1;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, c_cam_point.transform.position, c_pos_lerp);
        transform.rotation = Quaternion.Lerp(transform.rotation, c_cam_point.transform.rotation, c_rot_lerp);
    }
}
