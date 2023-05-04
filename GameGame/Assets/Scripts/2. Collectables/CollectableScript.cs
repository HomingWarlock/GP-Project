using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{
    private float c_rotate_speed;
    private float jb_jump_speed;
    private float jb_ground_location;
    private bool jb_jumped;
    private float s_rotate_speed;
    private bool s_rolled;

    private Rigidbody jb_rb;

    void Start()
    {
        c_rotate_speed = 50;

        if (this.gameObject.name == "Speed Boost")
        {
            s_rotate_speed = 50;
            s_rolled = false;
        }

        if (this.gameObject.name == "Jump Boost")
        {
            jb_jump_speed = 3;
            jb_ground_location = this.transform.position.y;
            jb_jumped = false;
            jb_rb = GetComponent<Rigidbody>();
            jb_rb.useGravity = false;
        }      
    }


    void Update()
    {
        if (this.gameObject.name == "Speed Boost")
        {
            transform.Rotate(s_rotate_speed * Time.deltaTime, s_rotate_speed * Time.deltaTime, s_rotate_speed * Time.deltaTime);

            if (!s_rolled)
            {
                s_rolled = true;
                s_rotate_speed = 200;
                StartCoroutine(S_Slow_Roll());
                StartCoroutine(S_Roll_Delay());
            }
        }
        else if (this.gameObject.name == "Jump Boost")
        {
            transform.Rotate(0, c_rotate_speed * Time.deltaTime, 0);

            if (this.transform.position.y < jb_ground_location)
            {
                this.transform.position = new Vector3(this.transform.position.x, jb_ground_location, this.transform.position.z);
                jb_rb.useGravity = false;
            }

            if (!jb_jumped)
            {
                jb_jumped = true;
                jb_rb.useGravity = true;
                jb_rb.velocity = new Vector3(0, jb_jump_speed, 0);
                StartCoroutine(JB_Jump_Delay());
            }
        }
        else
        {
            transform.Rotate(0, c_rotate_speed * Time.deltaTime, 0);
        }
    }

    private IEnumerator JB_Jump_Delay()
    {
        yield return new WaitForSeconds(3);
        jb_jumped = false;
    }

    private IEnumerator S_Roll_Delay()
    {
        yield return new WaitForSeconds(3);
        s_rolled = false;
    }

    private IEnumerator S_Slow_Roll()
    {
        yield return new WaitForSeconds(1);
        s_rotate_speed = 50;
    }
}


