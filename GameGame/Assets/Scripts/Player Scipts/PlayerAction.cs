using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    //[SerializeField] This shows Private Variables for testing
    private float p_move_speed;
    private float p_run_speed;
    private float p_boosted_speed;
    private float p_jump_speed;
    public bool p_grounded;
    private bool p_jump_input_check;
    public bool p_extra_jump;
    private int p_coins;

    private bool p_is_Running;
    private bool p_collected_Speed;
    public bool p_collected_Jump;

    private GameObject p_speed_effect;
    private GameObject p_jump_effect;
    private Rigidbody p_rb;

    void Start()
    {
        p_move_speed = 2000;
        p_run_speed = 0;
        p_boosted_speed = 0;
        p_jump_speed = 2000;
        p_grounded = false;
        p_jump_input_check = false;
        p_extra_jump = false;
        p_coins = 0;

        p_is_Running = false;
        p_collected_Speed = false;
        p_collected_Jump = false;

        p_speed_effect = GameObject.Find("Speed Effect");
        p_speed_effect.SetActive(false);
        p_jump_effect = GameObject.Find("Jump Effect");
        p_jump_effect.SetActive(false);
        p_rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKey(KeyCode.X))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        transform.Rotate(0, Input.GetAxisRaw("Mouse X") * 1000 * Time.deltaTime, 0);

        if (p_is_Running)
        {
            p_run_speed = 1000;
        }
        else if (!p_is_Running)
        {
            p_run_speed = 0;
        }

        if (p_collected_Speed)
        {
            p_boosted_speed = 1000;
        }
        else if (!p_collected_Speed)
        {
            p_boosted_speed = 0;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            //p_rb.velocity = transform.forward * (p_move_speed + p_run_speed + p_boosted_speed) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                p_rb.velocity = transform.forward * (p_move_speed + p_run_speed + p_boosted_speed) * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                p_rb.velocity = transform.forward * (p_move_speed + p_run_speed + p_boosted_speed) * Time.deltaTime + -transform.right * (p_move_speed + p_run_speed + p_boosted_speed) * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                p_rb.velocity = transform.forward * (p_move_speed + p_run_speed + p_boosted_speed) * Time.deltaTime + transform.right * (p_move_speed + p_run_speed + p_boosted_speed) * Time.deltaTime;
            }
        }
        /*
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                p_rb.velocity = new Vector3(0, p_rb.velocity.y, -1 * (p_move_speed + p_run_speed + p_boosted_speed) * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                p_rb.velocity = new Vector3(-1 * (p_move_speed + p_run_speed + p_boosted_speed) * Time.deltaTime, p_rb.velocity.y, -1 * (p_move_speed + p_run_speed + p_boosted_speed) * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                p_rb.velocity = new Vector3(1 * (p_move_speed + p_run_speed + p_boosted_speed) * Time.deltaTime, p_rb.velocity.y, -1 * (p_move_speed + p_run_speed + p_boosted_speed) * Time.deltaTime);
            }
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                p_rb.velocity = new Vector3(-1 * (p_move_speed + p_run_speed + p_boosted_speed) * Time.deltaTime, p_rb.velocity.y, 0);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                p_rb.velocity = new Vector3(1 * (p_move_speed + p_run_speed + p_boosted_speed) * Time.deltaTime, p_rb.velocity.y, 0);
            }
        }
        */

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            p_rb.velocity = new Vector3(0, p_rb.velocity.y, 0);
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            p_is_Running = true;
        }
        else
        {
            p_is_Running = false;
        }

        if (Input.GetKey(KeyCode.Space) && !p_jump_input_check)
        {
            p_jump_input_check = true;
            StartCoroutine(JumpInputDelay());
            if (p_collected_Jump)
            {
                if (!p_grounded && p_extra_jump)
                {
                    p_extra_jump = false;
                    p_rb.velocity = new Vector3(p_rb.velocity.x, transform.up.y * p_jump_speed * Time.deltaTime, p_rb.velocity.z);
                }
                else if (p_grounded)
                {
                    p_rb.velocity = new Vector3(p_rb.velocity.x, transform.up.y * p_jump_speed * Time.deltaTime, p_rb.velocity.z);
                }
            }
            else
            {
                if (p_grounded)
                {
                    p_rb.velocity = new Vector3(p_rb.velocity.x, transform.up.y * p_jump_speed * Time.deltaTime, p_rb.velocity.z);
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Yellow Coin")
        {
            p_coins += 1;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name == "Red Coin")
        {
            p_coins += 5;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name == "Blue Coin")
        {
            p_coins += 10;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name == "Diamond")
        {
            p_coins += 50;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name == "Speed Boost")
        {
            p_collected_Speed = true;
            p_speed_effect.SetActive(true);
            StartCoroutine(SpeedBoostTimer());
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name == "Jump Boost")
        {
            p_collected_Jump = true;
            p_jump_effect.SetActive(true);
            p_extra_jump = true;
            StartCoroutine(JumpBoostTimer());
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator SpeedBoostTimer()
    {
        yield return new WaitForSeconds(5);
        p_collected_Speed = false;
        p_speed_effect.SetActive(false);
    }

    private IEnumerator JumpBoostTimer()
    {
        yield return new WaitForSeconds(5);
        p_collected_Jump = false;
        p_jump_effect.SetActive(false);
        p_extra_jump = false;
    }

    private IEnumerator JumpInputDelay()
    {
        yield return new WaitForSeconds(0.2f);
        p_jump_input_check = false;
    }
}