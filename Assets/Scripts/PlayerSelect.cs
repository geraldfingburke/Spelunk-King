using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour
{
    public int player1Pos = 0;
    public int player2Pos = 0;

    public Image p1AliceImage;
    public Image p1CheckovImage;
    public Image p2AliceImage;
    public Image p2CheckovImage;

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
        switch (player1Pos)
        {
            case 0:
                p1AliceImage.gameObject.SetActive(true);
                p1CheckovImage.gameObject.SetActive(false);
                break;
            case 1:
                p1AliceImage.gameObject.SetActive(false);
                p1CheckovImage.gameObject.SetActive(true);
                break;
        }
        switch (player2Pos)
        {
            case 0:
                p2AliceImage.gameObject.SetActive(true);
                p2CheckovImage.gameObject.SetActive(false);
                break;
            case 1:
                p2AliceImage.gameObject.SetActive(false);
                p2CheckovImage.gameObject.SetActive(true);
                break;
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
        Image selectedImage = p1AliceImage;
        if (player == 0)
        {
            switch (player1Pos)
            {
                case 0:
                    selectedImage = p1AliceImage;
                    p1HasChosen = true;
                    GameManager.player1Selection = "alice";
                    break;
                case 1:
                    selectedImage = p1CheckovImage;
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
                    selectedImage = p2AliceImage;
                    p2HasChosen = true;
                    GameManager.player2Selection = "alice";
                    break;
                case 1:
                    selectedImage = p2CheckovImage;
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
