using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{
    public PlayerControl p_script;

    private MeshRenderer c_rend;
    private BoxCollider c_box;
    private Rigidbody jb_rb;

    private float c_rotate_speed;
    private float jb_jump_speed;
    private float jb_ground_location;
    private float jb_boost_timer;
    private bool jb_jumped;
    private float s_rotate_speed;
    private float s_boost_timer;
    private bool s_rolled;
    private IEnumerator jb_jumpboosttimer;
    private IEnumerator s_speedboosttimer;

    void Start()
    {
        p_script = GameObject.Find("Player").GetComponent<PlayerControl>();

        c_rend = GetComponent<MeshRenderer>();
        c_box = GetComponent<BoxCollider>();

        c_rotate_speed = 50;

        if (this.gameObject.name == "Speed Boost")
        {
            s_rotate_speed = 50;
            s_rolled = false;
        }

        if (this.gameObject.name == "Jump Boost")
        {
            jb_rb = GetComponent<Rigidbody>();

            jb_jump_speed = 3;
            jb_ground_location = this.transform.position.y;
            jb_jumped = false;
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

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (this.gameObject.name == "Yellow Coin")
            {
                p_script.p_coins += 1;
                c_rend.enabled = false;
                c_box.enabled = false;
                StartCoroutine(Respawn_Delay());
            }

            if (this.gameObject.name == "Red Coin")
            {
                p_script.p_coins += 5;
                c_rend.enabled = false;
                c_box.enabled = false;
                StartCoroutine(Respawn_Delay());
            }

            if (this.gameObject.name == "Blue Coin")
            {
                p_script.p_coins += 10;
                c_rend.enabled = false;
                c_box.enabled = false;
                StartCoroutine(Respawn_Delay());
            }

            if (this.gameObject.name == "Diamond")
            {
                p_script.p_coins += 50;
                c_rend.enabled = false;
                c_box.enabled = false;
                StartCoroutine(Respawn_Delay());
            }

            if (this.gameObject.name == "Speed Boost")
            {
                p_script.p_collected_Speed = true;
                p_script.p_speed_effect.SetActive(true);
                c_rend.enabled = false;
                c_box.enabled = false;
                StartCoroutine(Respawn_Delay());
                s_boost_timer += 5;

                if (s_speedboosttimer != null)
                {
                    StopCoroutine(s_speedboosttimer);
                }

                s_speedboosttimer = S_SpeedBoostTimer();
                StartCoroutine(s_speedboosttimer);
            }

            if (this.gameObject.name == "Jump Boost")
            {
                p_script.p_collected_Jump = true;
                p_script.p_jump_effect.SetActive(true);
                p_script.p_extra_jump = true;
                c_rend.enabled = false;
                c_box.enabled = false;
                StartCoroutine(Respawn_Delay());
                jb_boost_timer += 5;

                if (jb_jumpboosttimer != null)
                {
                    StopCoroutine(jb_jumpboosttimer);
                }

                jb_jumpboosttimer = JB_JumpBoostTimer();
                StartCoroutine(jb_jumpboosttimer);
            }
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

    private IEnumerator S_SpeedBoostTimer()
    {
        for (; s_boost_timer > 0; s_boost_timer -= Time.deltaTime)
        {
            yield return null;
        }
        p_script.p_collected_Speed = false;
        p_script.p_speed_effect.SetActive(false);
    }

    private IEnumerator JB_JumpBoostTimer()
    {
        for (; jb_boost_timer > 0; jb_boost_timer -= Time.deltaTime)
        {
            yield return null;
        }
        p_script.p_collected_Jump = false;
        p_script.p_jump_effect.SetActive(false);
        p_script.p_extra_jump = false;
    }

    private IEnumerator Respawn_Delay()
    {
        yield return new WaitForSeconds(1);
        c_rend.enabled = true;
        c_box.enabled = true;
    }
}


