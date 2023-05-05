using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    //[SerializeField] This shows Private Variables for testing
    private Rigidbody p_rb;

    [SerializeField] private float p_move_speed;
    private float p_movementX;
    private float p_movementZ;
    [SerializeField] private float p_run_speed;
    private float p_jump_speed;
    public bool p_grounded;
    private bool p_jump_input_check;
    private bool p_is_Running;

    private int p_coins;
    private float p_boosted_speed;
    public bool p_extra_jump;
    private bool p_collected_Speed;
    public bool p_collected_Jump;
    private GameObject p_speed_effect;
    private GameObject p_jump_effect;

    void Start()
    {
        p_rb = GetComponent<Rigidbody>();

        p_move_speed = 500;
        p_run_speed = 0;
        p_jump_speed = 4000;
        p_grounded = false;
        p_jump_input_check = false;
        p_is_Running = false;

        p_coins = 0;
        p_boosted_speed = 0;
        p_extra_jump = false;
        p_collected_Speed = false;
        p_collected_Jump = false;
        p_speed_effect = GameObject.Find("Speed Effect");
        p_speed_effect.SetActive(false);
        p_jump_effect = GameObject.Find("Jump Effect");
        p_jump_effect.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (p_collected_Speed)
        {
            p_boosted_speed = 500;
        }
        else if (!p_collected_Speed)
        {
            p_boosted_speed = 0;
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(p_movementX, 0.0f, p_movementZ);
        p_rb.AddForce(movement * (p_move_speed + p_run_speed + p_boosted_speed) * Time.deltaTime);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        p_movementX = movementVector.x;
        p_movementZ = movementVector.y;
    }

    private void OnRun()
    {
        if (p_is_Running)
        {
            p_run_speed = 500;
        }
        else if (!p_is_Running)
        {
            p_run_speed = 0;
        }
    }

    private void OnJump()
    {
        if (!p_jump_input_check)
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

    private void OnMouseLock()
    {
        if (Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
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

    private IEnumerator JumpInputDelay()
    {
        yield return new WaitForSeconds(0.2f);
        p_jump_input_check = false;
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
}