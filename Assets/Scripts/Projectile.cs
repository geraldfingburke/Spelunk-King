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
                break;
            case "Unbreakable":
                break;
            case "Player":
                col.GetComponent<PlayerController>().TakeDamage(damage);
                break;
        }
        Destroy(gameObject);
    }
}
