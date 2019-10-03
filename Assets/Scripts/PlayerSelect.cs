using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour
{
    public int player1Pos = 0;
    public int player2Pos = 0;

    public Image aliceImage;
    public Image checkovImage;

    private bool p1HasChosen = false;
    private bool p2HasChosen = false;

    private bool p1vAxisInUse = false;
    private bool p2vAxisInUse = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ListenForInput", .1f, .1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (player1Pos != player2Pos)
        {
            switch (player1Pos)
            {
                case 0:
                    aliceImage.color = new Color(0,0,1);
                    
                    break;
                case 1:
                    checkovImage.color = new Color(0, 0, 1);
                    
                    break;
            }
            switch (player2Pos)
            {
                case 0:
                    aliceImage.color = new Color(1, 0, 0);
                    break;
                case 1:
                    checkovImage.color = new Color(1, 0, 0);
                    break;
            }
        }
        else
        {
            switch (player1Pos)
            {
                case 0:
                    aliceImage.color = new Color(.75f,0,1);
                    checkovImage.color = Color.white;
                    break;
                case 1:
                    checkovImage.color = new Color(.75f,0,1f);
                    aliceImage.color = Color.white;
                    break;
            }
        }

        if (Input.GetButtonDown("PlayerOneShoot"))
        {
            StartCoroutine("Selected", 0);
        }
        else if (Input.GetButtonDown("PlayerTwoShoot"))
        {
            StartCoroutine("Selected", 1);
        }
    }

    public void ListenForInput()
    {

        if (Input.GetAxisRaw("PlayerOneHorizontal") > 0 && player1Pos < 1 && p1vAxisInUse == false)
        {
            player1Pos++;
            p1vAxisInUse = true;
        }
        else if (Input.GetAxisRaw("PlayerOneHorizontal") < 0 && player1Pos > 0 && p1vAxisInUse == false)
        {
            player1Pos--;
            p1vAxisInUse = true;
        }
        else if (Input.GetAxisRaw("PlayerOneHorizontal") == 0)
        {
            p1vAxisInUse = false;
        }

        if (Input.GetAxisRaw("PlayerTwoHorizontal") > 0 && player2Pos < 1 && p2vAxisInUse == false)
        {
            player2Pos++;
            p2vAxisInUse = true;
        }
        else if (Input.GetAxisRaw("PlayerTwoHorizontal") < 0 && player2Pos > 0 && p2vAxisInUse == false)
        {
            player2Pos--;
            p2vAxisInUse = true;
        }
        else if (Input.GetAxisRaw("PlayerTwoHorizontal") == 0)
        {
            p2vAxisInUse = false;
        }
    }

    public IEnumerator Selected(int player)
    {
        Image selectedImage = aliceImage;
        if (player == 0)
        {
            switch (player1Pos)
            {
                case 0:
                    selectedImage = aliceImage;
                    p1HasChosen = true;
                    GameManager.player1Selection = "alice";
                    break;
                case 1:
                    selectedImage = checkovImage;
                    p1HasChosen = true;
                    GameManager.player1Selection = "checkov";
                    break;
            }
        }
        else if (player == 1)
        {
            switch (player2Pos)
            {
                case 0:
                    selectedImage = aliceImage;
                    p2HasChosen = true;
                    GameManager.player2Selection = "alice";
                    break;
                case 1:
                    selectedImage = checkovImage;
                    p2HasChosen = true;
                    GameManager.player2Selection = "checkov";
                    break;
            }
        }
        
        selectedImage.color = Color.white;
        yield return new WaitForSeconds(.05f);
        selectedImage.color = new Color(0, 0, 0, .75f);
        yield return new WaitForSeconds(.05f);
        selectedImage.color = Color.white;
        yield return new WaitForSeconds(.05f);
        selectedImage.color = new Color(0, 0, 0, .75f);
        yield return new WaitForSeconds(.05f);
        selectedImage.color = Color.white;
        yield return new WaitForSeconds(.05f);
        selectedImage.color = new Color(0, 0, 0, .75f);
        yield return new WaitForSeconds(.05f);
        if (p1HasChosen == true && p2HasChosen == true)
        {
            LevelManager.Load("M03_LevelSelect");
        }
    }
}
