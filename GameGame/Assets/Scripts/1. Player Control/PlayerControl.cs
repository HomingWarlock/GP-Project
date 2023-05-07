using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //[SerializeField] This shows Private Variables for testing
    private Rigidbody p_rb;
    private Animator p_animator;

    private float p_move_speed;
    private float p_run_speed;
    private float p_jump_speed;
    private float p_velocityX;
    private float p_velocityZ;
    private float p_max_walk_velocity;
    private float p_max_run_velocity;
    private float p_acceleration;
    private float p_deceleration;
    private bool p_is_walking;
    private bool p_is_running;
    private bool p_is_jumping;
    private bool p_is_double_jumping;
    public bool p_grounded;
    private bool p_jump_input_check;
    public bool p_attack_input_check;
    public bool p_single_attack_check;
    private Vector3 p_back_dir;
    private Vector3 p_right_dir;
    private Vector3 p_true_dir;
    private GameObject p_cam_point;

    public int p_coins;
    private float p_boosted_speed;
    public bool p_extra_jump;
    public bool p_collected_Speed;
    public bool p_collected_Jump;
    public GameObject p_speed_effect;
    public GameObject p_jump_effect;

    private void Start()
    {
        p_rb = GetComponent<Rigidbody>();
        p_animator = GetComponent<Animator>();

        p_move_speed = 500;
        p_run_speed = 0;
        p_jump_speed = 10;
        p_velocityX = 0;
        p_velocityZ = 0;
        p_max_walk_velocity = 0.5f;
        p_max_run_velocity = 2;
        p_acceleration = 2;
        p_deceleration = 2;
        p_is_walking = false;
        p_is_running = false;
        p_is_jumping = false;
        p_is_double_jumping = false;
        p_grounded = false;
        p_jump_input_check = false;
        p_attack_input_check = false;
        p_single_attack_check = false;
        p_cam_point = GameObject.Find("CamPoint");

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

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKey(KeyCode.X))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        p_back_dir = Input.GetAxisRaw("Vertical") * p_cam_point.transform.forward;
        p_right_dir = Input.GetAxisRaw("Horizontal") * p_cam_point.transform.right;
        p_true_dir = p_back_dir + p_right_dir;

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            p_is_walking = true;
        }
        else if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            p_is_walking = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            p_is_running = true;
            p_run_speed = 500;
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift) && !Input.GetKey(KeyCode.JoystickButton3))
        {
            p_is_running = false;
            p_run_speed = 0;
        }

        float p_max_current_velocity = p_is_running ? p_max_run_velocity : p_max_walk_velocity;

        if (Input.GetAxisRaw("Horizontal") > 0 && p_velocityX < p_max_current_velocity)
        {
            p_velocityX += Time.deltaTime * p_acceleration;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && p_velocityX > -p_max_current_velocity)
        {
            p_velocityX -= Time.deltaTime * p_acceleration;
        }

        if (Input.GetAxisRaw("Horizontal") > 0 && p_is_running && p_velocityX > p_max_current_velocity)
        {
            p_velocityX = p_max_current_velocity;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0 && p_velocityX > p_max_current_velocity)
        {
            p_velocityX -= Time.deltaTime * p_deceleration;
            if (p_velocityX > p_max_current_velocity && p_velocityX < p_max_current_velocity + 0.05f)
            {
                p_velocityX = p_max_current_velocity;
            }
        }
        else if (Input.GetAxisRaw("Horizontal") > 0 && p_velocityX < p_max_current_velocity && p_velocityX > p_max_current_velocity - 0.05f)
        {
            p_velocityX = p_max_current_velocity;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && p_is_running && p_velocityX < -p_max_current_velocity)
        {
            p_velocityX = -p_max_current_velocity;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && p_velocityX < -p_max_current_velocity)
        {
            p_velocityX += Time.deltaTime * p_deceleration;
            if (p_velocityX < -p_max_current_velocity && p_velocityX > -p_max_current_velocity - 0.05f)
            {
                p_velocityX = -p_max_current_velocity;
            }
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && p_velocityX > -p_max_current_velocity && p_velocityX < -p_max_current_velocity + 0.05f)
        {
            p_velocityX = -p_max_current_velocity;
        }

        if (Input.GetAxisRaw("Horizontal") == 0 && p_velocityX > 0)
        {
            p_velocityX -= Time.deltaTime * p_deceleration;
        }
        else if (Input.GetAxisRaw("Horizontal") == 0 && p_velocityX < 0)
        {
            p_velocityX += Time.deltaTime * p_deceleration;
        }
        
        if (Input.GetAxisRaw("Horizontal") == 0 && p_velocityX != 0 && p_velocityX > -0.05f && p_velocityX < 0.05f)
        {
            p_velocityX = 0;
        }

        if (Input.GetAxisRaw("Vertical") > 0 && p_velocityZ < p_max_current_velocity)
        {
            p_velocityZ += Time.deltaTime * p_acceleration;
        }
        else if (Input.GetAxisRaw("Vertical") < 0 && p_velocityZ > -p_max_current_velocity)
        {
            p_velocityZ -= Time.deltaTime * p_acceleration;
        }

        if (Input.GetAxisRaw("Vertical") > 0 && p_is_running && p_velocityZ > p_max_current_velocity)
        {
            p_velocityZ = p_max_current_velocity;
        }
        else if (Input.GetAxisRaw("Vertical") > 0 && p_velocityZ > p_max_current_velocity)
        {
            p_velocityZ -= Time.deltaTime * p_deceleration;
            if (p_velocityZ > p_max_current_velocity && p_velocityZ < p_max_current_velocity + 0.05f)
            {
                p_velocityZ = p_max_current_velocity;
            }
        }
        else if (Input.GetAxisRaw("Vertical") > 0 && p_velocityZ < p_max_current_velocity && p_velocityZ > p_max_current_velocity - 0.05f)
        {
            p_velocityZ = p_max_current_velocity;
        }
        else if (Input.GetAxisRaw("Vertical") < 0 && p_is_running && p_velocityZ < -p_max_current_velocity)
        {
            p_velocityZ = -p_max_current_velocity;
        }
        else if (Input.GetAxisRaw("Vertical") < 0 && p_velocityZ < -p_max_current_velocity)
        {
            p_velocityZ += Time.deltaTime * p_deceleration;
            if (p_velocityZ < -p_max_current_velocity && p_velocityZ > -p_max_current_velocity - 0.05f)
            {
                p_velocityZ = -p_max_current_velocity;
            }
        }
        else if (Input.GetAxisRaw("Vertical") < 0 && p_velocityZ > -p_max_current_velocity && p_velocityZ < -p_max_current_velocity + 0.05f)
        {
            p_velocityZ = -p_max_current_velocity;
        }

        if (Input.GetAxisRaw("Vertical") == 0 && p_velocityZ > 0)
        {
            p_velocityZ -= Time.deltaTime * p_deceleration;
        }
        else if (Input.GetAxisRaw("Vertical") == 0 && p_velocityZ < 0)
        {
            p_velocityZ += Time.deltaTime * p_deceleration;
        }
        
        if (Input.GetAxisRaw("Vertical") == 0 && p_velocityZ != 0 && p_velocityZ > -0.05f && p_velocityZ < 0.05f)
        {
            p_velocityZ = 0;
        }

        p_animator.SetFloat("VelocityX", p_velocityX);
        p_animator.SetFloat("VelocityZ", p_velocityZ);
        p_animator.SetBool("isJumping", p_is_jumping);
        p_animator.SetBool("isGrounded", p_grounded);
        p_animator.SetBool("isDoubleJumping", p_is_double_jumping);

        if (p_collected_Speed)
        {
            p_boosted_speed = 500;
        }
        else if (!p_collected_Speed)
        {
            p_boosted_speed = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0) && !p_jump_input_check)
        {
            p_jump_input_check = true;
            StartCoroutine(JumpInputDelay());

            if (p_collected_Jump)
            {
                if (!p_grounded && p_extra_jump)
                {
                    p_extra_jump = false;
                    p_is_double_jumping = true;
                    p_rb.velocity = new Vector3(p_rb.velocity.x, p_jump_speed, p_rb.velocity.z);
                }
                else if (p_grounded)
                {
                    p_grounded = false;
                    p_is_jumping = true;
                    p_rb.velocity = new Vector3(p_rb.velocity.x, p_jump_speed, p_rb.velocity.z);
                }
            }
            else
            {
                if (p_grounded)
                {
                    p_grounded = false;
                    p_is_jumping = true;
                    p_rb.velocity = new Vector3(p_rb.velocity.x, p_jump_speed, p_rb.velocity.z);
                }
            }
        }

        if (Input.GetKey(KeyCode.Mouse0) && !p_attack_input_check)
        {
            p_attack_input_check = true;
            StartCoroutine(AttackInputDelay());
        }
    }

    private void FixedUpdate()
    {
        if (p_is_walking)
        {
            p_rb.velocity = new Vector3(p_true_dir.x * (p_move_speed + p_run_speed + p_boosted_speed) * Time.deltaTime, p_rb.velocity.y, p_true_dir.z * (p_move_speed + p_run_speed + p_boosted_speed) * Time.deltaTime);
        }
        else if (!p_is_walking)
        {
            p_rb.velocity = new Vector3(0, p_rb.velocity.y,0);
        }
    }

    private IEnumerator JumpInputDelay()
    {
        yield return new WaitForSeconds(0.2f);
        p_jump_input_check = false;
    }

    private IEnumerator AttackInputDelay()
    {
        yield return new WaitForSeconds(0.2f);
        p_attack_input_check = false;
        p_single_attack_check = false;
    }
}