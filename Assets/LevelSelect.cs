using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Vector2 player1Pos = new Vector2(0, 0);
    public Vector2 player2Pos = new Vector2(0, 0);

    public Image level1Image;
    public Image level2Image;
    public Image level3Image;
    public Image level4Image;

    private bool p1HasChosen = false;
    private bool p2HasChosen = false;

    private bool p1vAxisInUse = false;
    private bool p2vAxisInUse = false;

    private bool p1hAxisInUse = false;
    private bool p2hAxisInUse = false;

    private string p1SelectedLevel = "";
    private string p2SelectedLevel = "";

    void Start()
    {
        InvokeRepeating("ListenForInput", .1f, .1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (player1Pos != player2Pos)
        {
            switch (player1Pos.x)
            {
                case 0:
                    switch (player1Pos.y)
                    {
                        case 0:
                            level1Image.color = Color.blue;
                            if (player2Pos != new Vector2(0,1))
                            {
                                level2Image.color = Color.white;
                            }
                            if (player2Pos != new Vector2(1, 0))
                            {
                                level3Image.color = Color.white;
                            }
                            if (player2Pos != new Vector2(1, 1))
                            {
                                level4Image.color = Color.white;
                            }
                            break;
                        case 1:
                            level3Image.color = Color.blue;
                            if (player2Pos != new Vector2(0, 0))
                            {
                                level1Image.color = Color.white;
                            }
                            if (player2Pos != new Vector2(1, 0))
                            {
                                level2Image.color = Color.white;
                            }
                            if (player2Pos != new Vector2(1, 1))
                            {
                                level4Image.color = Color.white;
                            }
                            break;
                    }
                    break;
                case 1:
                    switch (player1Pos.y)
                    {
                        case 0:
                            level2Image.color = Color.blue;
                            if (player2Pos != new Vector2(0, 0))
                            {
                                level1Image.color = Color.white;
                            }
                            if (player2Pos != new Vector2(0, 1))
                            {
                                level3Image.color = Color.white;
                            }
                            if (player2Pos != new Vector2(1, 1))
                            {
                                level4Image.color = Color.white;
                            }
                            break;
                        case 1:
                            level4Image.color = Color.blue;
                            if (player2Pos != new Vector2(0, 0))
                            {
                                level1Image.color = Color.white;
                            }
                            if (player2Pos != new Vector2(1, 0))
                            {
                                level2Image.color = Color.white;
                            }
                            if (player2Pos != new Vector2(0, 1))
                            {
                                level3Image.color = Color.white;
                            }
                            break;
                    }
                    break;
            }
            switch (player2Pos.x)
            {
                case 0:
                    switch (player2Pos.y)
                    {
                        case 0:
                            level1Image.color = Color.red;
                            if (player1Pos != new Vector2(1, 0))
                            {
                                level2Image.color = Color.white;
                            }
                            if (player1Pos != new Vector2(0, 1))
                            {
                                level3Image.color = Color.white;
                            }
                            if (player1Pos != new Vector2(1, 1))
                            {
                                level4Image.color = Color.white;
                            }
                            break;
                        case 1:
                            level3Image.color = Color.red;
                            if (player1Pos != new Vector2(1, 0))
                            {
                                level2Image.color = Color.white;
                            }
                            if (player1Pos != new Vector2(0, 0))
                            {
                                level1Image.color = Color.white;
                            }
                            if (player1Pos != new Vector2(1, 1))
                            {
                                level4Image.color = Color.white;
                            }
                            break;
                    }
                    break;
                case 1:
                    switch (player2Pos.y)
                    {
                        case 0:
                            level2Image.color = Color.red;
                            if (player1Pos != new Vector2(0, 0))
                            {
                                level1Image.color = Color.white;
                            }
                            if (player1Pos != new Vector2(0, 1))
                            {
                                level3Image.color = Color.white;
                            }
                            if (player1Pos != new Vector2(1, 1))
                            {
                                level4Image.color = Color.white;
                            }
                            break;
                        case 1:
                            level4Image.color = Color.red;
                            if (player1Pos != new Vector2(1, 0))
                            {
                                level2Image.color = Color.white;
                            }
                            if (player1Pos != new Vector2(0, 1))
                            {
                                level3Image.color = Color.white;
                            }
                            if (player1Pos != new Vector2(0, 0))
                            {
                                level1Image.color = Color.white;
                            }
                            break;
                    }
                    break;
            }
        }
        else
        {
            switch (player1Pos.x)
            {
                case 0:
                    switch(player1Pos.y)
                    {
                        case 0:
                            level1Image.color = new Color(.75f, 0, 1);
                            level2Image.color = Color.white;
                            level3Image.color = Color.white;
                            level4Image.color = Color.white;
                            break;
                        case 1:
                            level3Image.color = new Color(.75f, 0, 1);
                            level2Image.color = Color.white;
                            level4Image.color = Color.white;
                            level1Image.color = Color.white;
                            break;
                    }
                    break;
                case 1:
                    switch(player1Pos.y)
                    {
                        case 0:
                            level2Image.color = new Color(.75f, 0, 1);
                            level3Image.color = Color.white;
                            level4Image.color = Color.white;
                            level1Image.color = Color.white;
                            break;
                        case 1:
                            level4Image.color = new Color(.75f, 0, 1);
                            level2Image.color = Color.white;
                            level3Image.color = Color.white;
                            level1Image.color = Color.white;
                            break;
                    }
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

        if (Input.GetAxisRaw("PlayerOneHorizontal") > 0 && player1Pos.x < 1 && p1hAxisInUse == false)
        {
            player1Pos.x++;
            p1hAxisInUse = true;
        }
        else if (Input.GetAxisRaw("PlayerOneHorizontal") < 0 && player1Pos.x > 0 && p1hAxisInUse == false)
        {
            player1Pos.x--;
            p1hAxisInUse = true;
        }
        else if (Input.GetAxisRaw("PlayerOneHorizontal") == 0)
        {
            p1hAxisInUse = false;
        }

        if (Input.GetAxisRaw("PlayerTwoHorizontal") > 0 && player2Pos.x < 1 && p2hAxisInUse == false)
        {
            player2Pos.x++;
            p2hAxisInUse = true;
        }
        else if (Input.GetAxisRaw("PlayerTwoHorizontal") < 0 && player2Pos.x > 0 && p2hAxisInUse == false)
        {
            player2Pos.x--;
            p2hAxisInUse = true;
        }
        else if (Input.GetAxisRaw("PlayerTwoHorizontal") == 0)
        {
            p2hAxisInUse = false;
        }
        if (Input.GetAxisRaw("PlayerOneVertical") > 0 && player1Pos.y > 0 && p1vAxisInUse == false)
        {
            player1Pos.y--;
            p1vAxisInUse = true;
        }
        else if (Input.GetAxisRaw("PlayerOneVertical") < 0 && player1Pos.y < 1 && p1vAxisInUse == false)
        {
            player1Pos.y++;
            p1vAxisInUse = true;
        }
        else if (Input.GetAxisRaw("PlayerOneVertical") == 0)
        {
            p1vAxisInUse = false;
        }

        if(Input.GetAxisRaw("PlayerTwoVertical") > 0 && player2Pos.y > 0 && p2vAxisInUse == false)
        {
            player2Pos.y--;
            p2vAxisInUse = true;
        }
        else if (Input.GetAxisRaw("PlayerTwoVertical") < 0 && player2Pos.y < 1 && p2vAxisInUse == false)
        {
            player2Pos.y++;
            p2vAxisInUse = true;
        }
        else if (Input.GetAxisRaw("PlayerTwoVertical") == 0)
        {
            p2vAxisInUse = false;
        }
    }

    public IEnumerator Selected(int player)
    {
        Image selectedImage = level1Image;

        if (player == 0)
        {
            switch (player1Pos.x)
            {
                case 0:
                    switch (player1Pos.y)
                    {
                        case 0:
                            p1SelectedLevel = "04A_Level1";
                            p1HasChosen = true;
                            selectedImage = level1Image;
                            break;
                        case 1:
                            p1SelectedLevel = "04C_Level3";
                            p1HasChosen = true;
                            selectedImage = level3Image;
                            break;
                    }
                    break;
                case 1:
                    switch (player1Pos.y)
                    {
                        case 0:
                            p1SelectedLevel = "04B_Level2";
                            p1HasChosen = true;
                            selectedImage = level2Image;
                            break;
                        case 1:
                            p1SelectedLevel = "04D_Level4";
                            p1HasChosen = true;
                            selectedImage = level4Image;
                            break;
                    }
                    break;
            }
        }
        else if (player == 1)
        {
            switch (player2Pos.x)
            {
                case 0:
                    switch (player2Pos.y)
                    {
                        case 0:
                            p2SelectedLevel = "04A_Level1";
                            p2HasChosen = true;
                            selectedImage = level1Image;
                            break;
                        case 1:
                            p2SelectedLevel = "04C_Level3";
                            p2HasChosen = true;
                            selectedImage = level3Image;
                            break;
                    }
                    break;
                case 1:
                    switch (player2Pos.y)
                    {
                        case 0:
                            p2SelectedLevel = "04B_Level2";
                            p2HasChosen = true;
                            selectedImage = level2Image;
                            break;
                        case 1:
                            p2SelectedLevel = "04D_Level4";
                            p2HasChosen = true;
                            selectedImage = level4Image;
                            break;
                    }
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
        if (p1HasChosen && p2HasChosen)
        {
            if (p1SelectedLevel == p2SelectedLevel)
            {
                LevelManager.Load(p1SelectedLevel);
            }
            else
            {
                int coinToss = Random.Range(0, 2);
                switch (coinToss)
                {
                    case 0:
                        LevelManager.Load(p1SelectedLevel);
                        break;
                    case 1:
                        LevelManager.Load(p2SelectedLevel);
                        break;
                }
            }
        }
    }
}
