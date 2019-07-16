using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private int damage;

    public void OnTriggerEnter2D(Collider2D col)
    {
        switch(col.tag)
        {
            case "Breakable":
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
