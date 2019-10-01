using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    [Header("Damage the projectile does to the player or environment")]
    private int damage;
    [SerializeField]
    [Header("Speed at which the projectile travels")]
    private float speed;
    [SerializeField]
    [Header("Plays when player is hit")]
    private AudioClip playerClip;
    [SerializeField]
    [Header("Plays when breakable block is hit")]
    private AudioClip blockClip;

    public float GetSpeed()
    {
        return speed;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        switch(col.tag)
        {
            case "Breakable":
                Destroy(col.gameObject);
                AudioSource.PlayClipAtPoint(blockClip, col.transform.position);
                break;
            case "Unbreakable":
                break;
            case "Player":
                col.GetComponent<PlayerController>().TakeDamage(damage);
                AudioSource.PlayClipAtPoint(playerClip, col.transform.position);
                break;
        }
        Destroy(gameObject);
    }
}
