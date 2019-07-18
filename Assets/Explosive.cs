using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Explosive : MonoBehaviour
{
    #region Properties
    // Timer for automatic explosions
    private float countDown;
    // Animated object for explosion
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    [Header("How soon after the start of the match boxes will randomly explode.")]
    [Range(11, 120)]
    private float explosionFrequency;
    #endregion

    #region Start and Update
    public void Start()
    {
        countDown = Random.Range(10, explosionFrequency);
    }

    public void Update()
    {
        countDown -= Time.deltaTime;

        if (countDown <= 0)
        {
            Explode();
        }
    }
    #endregion

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Projectile"))
        {
            Explode();
        }
    }

    public void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        for (int i = 0; i < 5; i++)
        {
            List<RaycastHit2D> hits = new List<RaycastHit2D>();

            hits.Add(Physics2D.Raycast(transform.position + new Vector3(0, 1), new Vector2(0, 1), Random.Range(0, 5)));
            hits.Add(Physics2D.Raycast(transform.position + new Vector3(1, 1), new Vector2(1, 1), Random.Range(0, 5)));
            hits.Add(Physics2D.Raycast(transform.position + new Vector3(1, 0), new Vector2(1, 0), Random.Range(0, 5)));
            hits.Add(Physics2D.Raycast(transform.position + new Vector3(1, -1), new Vector2(1, -1), Random.Range(0, 5)));
            hits.Add(Physics2D.Raycast(transform.position + new Vector3(0, -1), new Vector2(0, -1), Random.Range(0, 5)));
            hits.Add(Physics2D.Raycast(transform.position + new Vector3(-1, -1), new Vector2(-1, -1), Random.Range(0, 5)));
            hits.Add(Physics2D.Raycast(transform.position + new Vector3(-1, 0), new Vector2(-1, 0), Random.Range(0, 5)));
            hits.Add(Physics2D.Raycast(transform.position + new Vector3(-1, 1), new Vector2(-1, 1), Random.Range(0, 5)));
            foreach (var hit in hits)
            {
                try
                {
                    switch (hit.collider.tag)
                    {
                        case "Player":
                            hit.collider.GetComponent<PlayerController>().TakeDamage(100);
                            break;
                        case "Breakable":
                            Destroy(hit.collider.gameObject);
                            break;
                    }
                }
                catch (NullReferenceException nre)
                {

                }
            }
        }
        Destroy(gameObject);
    }
}