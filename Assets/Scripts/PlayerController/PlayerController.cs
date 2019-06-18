using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float health;
    public float damage;
    public float moveSpeed;
    public float jumpHeight;
    public float fireRate;
    public float slide;
    public int playerNumber;
    public bool isAI;

    private Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (playerNumber)
        {
            case 1:
                MoveHorizontal("PlayerOneHorizontal");
                JumpListener("PlayerOneJump");
                break;
            case 2:
                MoveHorizontal("PlayerTwoHorizontal");
                JumpListener("PlayerTwoJump");
                break;
        }
    }

    public void MoveHorizontal(String axis)
    {
        if (Input.GetAxisRaw(axis) >= 0.5f || Input.GetAxisRaw(axis) <= -0.5f)
        {
            rigidbody.velocity = new Vector2(Input.GetAxisRaw(axis) * moveSpeed, 0);
        }
        else
        {
            rigidbody.velocity = new Vector2(Mathf.Lerp(rigidbody.velocity.x, 0, slide), 0);
        }
    }

    public void Jump()
    {
        rigidbody.AddForce(Vector2.up * jumpHeight);
    }

    public void JumpListener(String button)
    {
        if (Input.GetButtonDown(button))
        {
            Jump();
        }
    }

    public void Shoot()
    {

    }

    public void Taunt()
    {

    }

    public IEnumerator AIRoutine()
    {
        return null;
    }
}
