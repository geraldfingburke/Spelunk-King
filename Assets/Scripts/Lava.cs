using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public GameObject lava;
    public ParticleSystem particles;
    public bool isFlowing;

    void Start()
    {
        InvokeRepeating("LavaFlow", 0.5f, 0.5f);
    }

    void LavaFlow()
    {
        if (isFlowing)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, -1f), Vector2.down, 0.05f);
            if (hit.collider == null || hit.collider.CompareTag("Player") || hit.collider.CompareTag("Ladder"))
            {
                Instantiate(lava, transform.position + Vector3.down, Quaternion.identity);
            }
        } 
    }
}

