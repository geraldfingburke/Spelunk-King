using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text player1Score;
    public Text player2Score;

    private PlayerController player1;
    private PlayerController player2;

    private PlayerController[] players;

    private void Start()
    {
        players = FindObjectsOfType<PlayerController>();
        for (int i = 0; i < players.Length; i++)
        {
            switch (players[i].GetPlayerNumber())
            {
                case 1:
                    player1 = players[i];
                    break;
                case 2:
                    player2 = players[i];
                    break;
            }
        }
    }

    void Update()
    {
        player1Score.text = player1.name + ": " + GameManager.player1Score;
        player2Score.text = player2.name + ": " + GameManager.player2Score;

    }
}
