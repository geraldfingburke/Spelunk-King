using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameManager : MonoBehaviour
{
    public static int player1Score { get; set; } = 0;
    public static int player2Score { get; set; } = 0;
    
    public static void ResetScores ()
    {
        player1Score = 0;
        player2Score = 0;
    }
}
