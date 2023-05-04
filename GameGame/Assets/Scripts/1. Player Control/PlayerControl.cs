using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    //[SerializeField] This shows Private Variables for testing
    private float movementX;
    private float movementZ;
    [SerializeField] private float p_move_speed;

    private Rigidbody p_rb;

    void Start()
    {
        p_move_speed = 10;

        p_rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementZ);
        p_rb.AddForce(movement * p_move_speed * Time.deltaTime);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementZ = movementVector.y;
    }
}