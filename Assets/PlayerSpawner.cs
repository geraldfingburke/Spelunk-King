using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject p1SpawnNode;
    public GameObject p2SpawnNode;

    public PlayerController alice;
    public PlayerController checkov;

    // Start is called before the first frame update
    void Start()
    {
        switch (GameManager.player1Selection)
        {
            case "alice":
                PlayerController p1Alice = Instantiate(alice, p1SpawnNode.transform.position, Quaternion.identity);
                p1Alice.SetPlayerNumber(1);
                break;
            case "checkov":
                PlayerController p1Checkov = Instantiate(checkov, p1SpawnNode.transform.position, Quaternion.identity);
                p1Checkov.SetPlayerNumber(1);
                break;
        }
        switch (GameManager.player2Selection)
        {
            case "alice":
                PlayerController p2Alice = Instantiate(alice, p2SpawnNode.transform.position, Quaternion.identity);
                p2Alice.SetPlayerNumber(2);
                break;
            case "checkov":
                PlayerController p2Checkov = Instantiate(checkov, p2SpawnNode.transform.position, Quaternion.identity);
                p2Checkov.SetPlayerNumber(2);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
