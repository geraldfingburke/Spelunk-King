using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameManager : MonoBehaviour
{
    public static int player1Score { get; set; } = 0;
    public static int player2Score { get; set; } = 0;

    public static PlayerController player1;
    public static PlayerController player2;

    public static string player1Selection;
    public static string player2Selection;

    public static string levelSelection;

    public static void ResetScores ()
    {
        player1Score = 0;
        player2Score = 0;
    }

    public static void FindPlayers()
    {
        PlayerController [] players = FindObjectsOfType<PlayerController>();

        foreach (PlayerController p in players)
        {
            if (p.GetPlayerNumber() == 1)
            {
                player1 = p;
            }
            else if (p.GetPlayerNumber() == 2)
            {
                player2 = p;
            }
        }
    }


}
