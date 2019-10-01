using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = System.Random;

public enum Facing
{
    Left,
    Right,
    Up,
    Down
}

public class PlayerController : MonoBehaviour
{
    #region Player Properties
    [SerializeField]
    [Header("Name used in UI")]
    private string name;
    [SerializeField]
    [Header("Player Health")]
    [Range(10, 100)]
    private float health;
    [SerializeField]
    [Header("Damage Player Does")]
    [Range(10, 100)]
    private float damage;
    [SerializeField]
    [Header("Speed at Which Player Moves")]
    [Range(1, 30)]
    private float moveSpeed;
    [SerializeField]
    [Header("How high the player can jump")]
    private float jumpHeight;
    [SerializeField]
    [Header("How fast the player falls after a jump")]
    private float fallMultiplier;
    [SerializeField]
    [Header("How high the player jumps when button is pressed, rather than held")]
    private float lowJumpMultiplier;
    [SerializeField]
    [Header("How fast the player shoots when the fire button is held")]
    private float fireRate;
    [SerializeField]
    [Header("Player slide while moving vertically")]
    private float slide;
    [SerializeField]
    [Header("Controller slot of this player, cannot be the same as any other player")]
    private int playerNumber;
    [SerializeField]
    [Header("Determines whether player is computer controlled")]
    private bool isAI;
    [SerializeField]
    [Header("Projectile this player uses")]
    private Projectile projectile;
    [SerializeField]
    [Header("Direction the player is currently facing")]
    private Facing facing;

    private bool isOnLadder;
    private bool canMoveDown;
    #endregion

    #region Components
    private Rigidbody2D rigidbody;
    private Animator animator;
    private PlayerController target;
    #endregion

    #region Start and Update
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        InvokeRepeating("CheckGround", 0.5f, 0.2f);
        StartCoroutine("Die");
        Debug.Log("Player 1: " + GameManager.player1Score + " Player 2: " + GameManager.player2Score);
        target = GameManager.player1;
        if (isAI)
        {
            StartCoroutine("AIRoutine");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (isOnLadder)
        {
            rigidbody.gravityScale = 0;
        }
        else
        {
            rigidbody.gravityScale = 1;
        }

        CheckLadder();

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
    #endregion

    #region Listeners
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
            MoveUp();
        }
        else if (Input.GetAxisRaw(axis) <= -0.5f)
        {
            facing = Facing.Down;
            animator.SetBool("isFacingDown", true);
            animator.SetBool("isFacingUp", false);
            MoveDown();
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

    public void CheckLadder()
    {
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position + new Vector3(0, 0.9f), Vector2.up, 0.5f);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position + new Vector3(0, -0.9f), Vector2.down, 0.5f);

        if (hitUp.collider != null)
        {
            if (hitUp.collider.CompareTag("Ladder"))
            {
                isOnLadder = true;
                rigidbody.gravityScale = 0;
            }
        }
        if (hitDown.collider != null)
        {
            if (hitDown.collider.CompareTag("Ladder"))
            {
                isOnLadder = true;
                rigidbody.gravityScale = 0;
                canMoveDown = true;
            }
        }
        else
        {
            isOnLadder = false;
            canMoveDown = false;
            rigidbody.gravityScale = 1;
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Lava"))
        {
            Instantiate(col.GetComponent<Lava>().particles, col.transform.position + new Vector3(0,1,0), Quaternion.Euler(-90,0,0));
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            health = 0;
        }
    }
    #endregion

    #region Actions
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

    public IEnumerator Die()
    {
        bool roundOver = false;
        while (health > 0)
        {
            yield return null;
        }
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(1);
        if (GameManager.player1Score < 3 && GameManager.player2Score < 3)
        {
            switch (playerNumber)
            {
                case 1:
                    GameManager.player2Score++;
                    break;
                case 2:
                    GameManager.player1Score++;
                    break;
            }
        } 
        else
        {
            roundOver = true;
        }
        if (roundOver)
        {
            GameManager.ResetScores();
            yield return new WaitForEndOfFrame();
            LevelManager.Reload();
        }
        else
        {
            LevelManager.Reload();
        }
    }

    public void Shoot()
    {
        animator.SetTrigger("ShootTrigger");
        switch (facing)
        {
            case (Facing.Right):
                Projectile projR = Instantiate(projectile, transform.position + new Vector3(0.5f, 0), Quaternion.identity);
                projR.GetComponent<Rigidbody2D>().AddForce(Vector2.right * (projR.GetSpeed() + moveSpeed));
                break;
            case (Facing.Left):
                Projectile projL = Instantiate(projectile, transform.position + new Vector3(-0.5f, 0), Quaternion.identity);
                projL.GetComponent<Rigidbody2D>().AddForce(Vector2.left * (projL.GetSpeed() + moveSpeed));
                break;
            case (Facing.Up):
                Projectile projUp = Instantiate(projectile, transform.position + Vector3.up, Quaternion.identity);
                projUp.GetComponent<Rigidbody2D>().AddForce(Vector2.up * (projUp.GetSpeed() + moveSpeed));
                break;
            case (Facing.Down):
                Projectile projDown = Instantiate(projectile, transform.position + Vector3.down, Quaternion.identity);
                projDown.GetComponent<Rigidbody2D>().AddForce(Vector2.down * (projDown.GetSpeed() + moveSpeed));
                break;
        }
    }

    public void Taunt()
    {
        animator.SetTrigger("TauntTrigger");
    }

    public void MoveUp()
    {
        if (isOnLadder)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
    }

    public void MoveDown()
    {
        Debug.Log(canMoveDown);
        if (isOnLadder && canMoveDown)
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
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
        if (hit.collider == null || hit.collider.isTrigger)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
    }

    public void MoveLeft()
    {
        GetComponent<SpriteRenderer>().flipX = false;
        facing = Facing.Left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(-0.5f, 0), Vector2.left, 0.05f);
        if (hit.collider == null || hit.collider.isTrigger)
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            Debug.Log("clear");
        }
    }
    #endregion

    #region Juice
    /**
 * Makes the player jump longer as the button is pressed longer
 * Also makes the player fall faster than they jump
 */

    public void JumpJuice(String button)
    {
        if (rigidbody.velocity.y < 0 && !isOnLadder)
        {
            rigidbody.velocity += Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }

        if (rigidbody.velocity.y > 0 && !Input.GetButton(button) && !isOnLadder)
        {
            rigidbody.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }



    #endregion

    #region Getters and Setters
    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public string GetName ()
    {
        return name;
    }

    public int GetPlayerNumber()
    {
        return playerNumber;
    }

    public void SetPlayerNumber(int playerNumber)
    {
        this.playerNumber = playerNumber;
    }
    #endregion

    #region AI Routines

    // TODO: Make the game play itself (save for last)
    public IEnumerator AIRoutine()
    {
        while (true)
        {
            Debug.Log(target.transform.position.x - transform.position.x);
            while (target.transform.position.x > transform.position.x &&
                   target.transform.position.x - transform.position.x > 3 ||
                   target.transform.position.y - transform.position.y > 2 &&
                   target.transform.position.x > transform.position.x)
            {
                Debug.Log("Should be moving right");
                MoveRight();
                while (isOnLadder)
                {
                    if (target.transform.position.y > transform.position.y)
                    {
                        MoveUp();
                    }
                    else if (target.transform.position.y < transform.position.y)
                    {
                        MoveDown();
                    }

                    yield return null;
                }
                yield return null;
            }

            while (target.transform.position.x < transform.position.x &&
                   target.transform.position.x - transform.position.x < -3 ||
                   target.transform.position.y - transform.position.y > 2 &&
                   target.transform.position.x < transform.position.x)
            {
                Debug.Log("Should be moving left");
                MoveLeft();
                yield return null;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    #endregion
}