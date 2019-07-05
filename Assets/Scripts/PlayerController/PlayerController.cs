using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public enum Facing
{
    Left,
    Right,
    Up,
    Down
}

public class PlayerController : MonoBehaviour
{
    [Header("Player Health")]
    [Range(10, 100)]
    public float health;
    [Header("Damage Player Does")]
    [Range(10, 100)]
    public float damage;
    [Header("Speed at Which Player Moves")]
    [Range(1, 30)]
    public float moveSpeed;
    [Header("How high the player can jump")]
    public float jumpHeight;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    public float fireRate;
    public float slide;
    public int playerNumber;
    public bool isAI;
    public Projectile projectile;
    public float projectileSpeed;
    public Facing facing;


    private Rigidbody2D rigidbody;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        InvokeRepeating("CheckGround", 0.5f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        switch (playerNumber)
        {
            case 1:
                HorizontalMoveListener("PlayerOneHorizontal");
                VerticalMoveListener("PlayerOneVertical");
                JumpListener("PlayerOneJump");
                JumpJuice("PlayerOneJump");
                ShootListener("PlayerOneShoot");
                TauntListener("PlayerOneTaunt");
                break;
            case 2:
                HorizontalMoveListener("PlayerTwoHorizontal");
                VerticalMoveListener("PlayerTwoVertical");
                JumpListener("PlayerTwoJump");
                JumpJuice("PlayerTwoJump");
                ShootListener("PlayerTwoShoot");
                TauntListener("PlayerTwoTaunt");
                break;
        }
    }

    public void HorizontalMoveListener(String axis)
    {
        if (Input.GetAxisRaw(axis) >= 0.5f)
        {
            MoveRight();
            animator.SetBool("isFacingUp", false);
            animator.SetBool("isFacingDown", false);
            if (isGrounded())
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
        else if (Input.GetAxisRaw(axis) <= -0.5f)
        {
            MoveLeft();
            animator.SetBool("isFacingUp", false);
            animator.SetBool("isFacingDown", false);
            if (isGrounded())
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
        else
        {
            rigidbody.velocity = new Vector2(Mathf.Lerp(rigidbody.velocity.x, 0, slide), rigidbody.velocity.y);
            if (isGrounded())
            {
                animator.SetBool("isWalking", false);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
    }

    public void VerticalMoveListener(String axis)
    {
        if (Input.GetAxisRaw(axis) >= 0.5f)
        {
            facing = Facing.Up;
            animator.SetBool("isFacingUp", true);
            animator.SetBool("isFacingDown", false);
        }
        else if (Input.GetAxisRaw(axis) <= -0.5f)
        {
            facing = Facing.Down;
            animator.SetBool("isFacingDown", true);
            animator.SetBool("isFacingUp", false);
        }
    }

    public void Jump()
    {
        animator.SetBool("isJumping", true);
        rigidbody.AddForce(Vector2.up * jumpHeight);
        switch (facing)
        {
            case Facing.Down:
                break;
            case Facing.Up:
                break;
            case Facing.Left:
                break;
            case Facing.Right:
                break;
        }
    }

    public void JumpListener(String button)
    {
        if (Input.GetButtonDown(button) && isGrounded())
        {
            Jump();

        }
    }

    public void ShootListener(String button)
    {
        if (Input.GetButtonDown(button))
        {
            InvokeRepeating("Shoot", 0f, (1 / fireRate));
        }

        if (Input.GetButtonUp(button))
        {
            CancelInvoke("Shoot");
        }
    }

    public void Shoot()
    {
        animator.SetTrigger("ShootTrigger");
        switch (facing)
        {
            case (Facing.Right):
                Projectile projR = Instantiate(projectile, transform.position + new Vector3(0.5f, 0), Quaternion.identity);
                projR.GetComponent<Rigidbody2D>().AddForce(Vector2.right * (projectileSpeed + moveSpeed));
                break;
            case (Facing.Left):
                Projectile projL = Instantiate(projectile, transform.position + new Vector3(-0.5f, 0), Quaternion.identity);
                projL.GetComponent<Rigidbody2D>().AddForce(Vector2.left * (projectileSpeed + moveSpeed));
                break;
            case (Facing.Up):
                Projectile projUp = Instantiate(projectile, transform.position + Vector3.up, Quaternion.identity);
                projUp.GetComponent<Rigidbody2D>().AddForce(Vector2.up * (projectileSpeed + moveSpeed));
                break;
            case (Facing.Down):
                Projectile projDown = Instantiate(projectile, transform.position + Vector3.down, Quaternion.identity);
                projDown.GetComponent<Rigidbody2D>().AddForce(Vector2.down * (projectileSpeed + moveSpeed));
                break;
        }
    }

    public void Taunt()
    {
        animator.SetTrigger("TauntTrigger");
    }

    public void TauntListener(String button)
    {
        if (Input.GetButtonDown(button))
        {
            Taunt();
        }
    }

    public bool isGrounded()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position + new Vector3(0, -.75f), Vector2.down, 0.5f);
        if (ray.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CheckGround()
    {
        if (isGrounded())
        {
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isJumping", true);
        }
    }

    /**
     * Makes the player jump longer as the button is pressed longer
     * Also makes the player fall faster than they jump
     */

    public void JumpJuice(String button)
    {
        if (rigidbody.velocity.y < 0)
        {
            rigidbody.velocity += Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }

        if (rigidbody.velocity.y > 0 && !Input.GetButton(button))
        {
            rigidbody.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    /**
     * MoveRight and MoveLeft handle a couple of issues. One, uses
     * rays to detect collision to stop movement. This prevents
     * the player from getting stuck on a wall. Two, handles x-axis
     * flipping for player sprite, simplifying facing for animations
     * and logic.
     */
    public void MoveRight()
    {
        GetComponent<SpriteRenderer>().flipX = true;
        facing = Facing.Right;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0.5f, 0), Vector2.right, 0.05f);
        if (hit.collider == null)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
    }

    public void MoveLeft()
    {
        GetComponent<SpriteRenderer>().flipX = false;
        facing = Facing.Left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(-0.5f, 0), Vector2.left, 0.05f);
        if (hit.collider == null)
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            Debug.Log("clear");
        }
    }
    // TODO: Make the game play itself (save for last)
    public IEnumerator AIRoutine()
    {
        return null;
    }
}
