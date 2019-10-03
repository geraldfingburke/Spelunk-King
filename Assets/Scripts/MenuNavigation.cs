using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    public int position = 0;

    public Image start;
    public Image credits;
    public Image options;

    private bool vAxisInUse = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ListenForInput", .1f, .1f);
    }

    // Update is called once per frame
    void Update()
    {
        switch (position)
        {
            case 0:
                start.color = new Color(1, 1, 1, 0.5f);
                credits.color = Color.white;
                options.color = Color.white;
                break;
            case 1:
                start.color = Color.white;
                credits.color = new Color(1, 1, 1, 0.5f);
                options.color = Color.white;
                break;
            case 2:
                start.color = Color.white;
                credits.color = Color.white;
                options.color = new Color(1, 1, 1, 0.5f);
                break;
        }
        if (Input.GetButtonDown("PlayerOneShoot"))
        {
            StartCoroutine("Selected");
        }
    }

    public void ListenForInput ()
    {
        
        if (Input.GetAxisRaw("PlayerOneVertical") > 0 && position > 0 && vAxisInUse == false)
        {
            position--;
            vAxisInUse = true;
        }
        else if (Input.GetAxisRaw("PlayerOneVertical") < 0 && position < 2 && vAxisInUse == false)
        {
            position++;
            vAxisInUse = true;
        }
        else
        {
            vAxisInUse = false;
        }
    }

    public IEnumerator Selected ()
    {
        Image selectedImage = start;
        string selectedLevel = "";

        switch (position)
        {
            case 0:
                selectedImage = start;
                selectedLevel = "M02_PlayerSelect";
                break;
            case 1:
                selectedImage = credits;
                selectedLevel = "05_Credits";
                break;
            case 2:
                selectedImage = options;
                selectedLevel = "01B_Options";
                break;
        }
        selectedImage.color = Color.white;
        yield return new WaitForSeconds(.05f);
        selectedImage.color = new Color(0,0,0,.75f);
        yield return new WaitForSeconds(.05f);
        selectedImage.color = Color.white;
        yield return new WaitForSeconds(.05f);
        selectedImage.color = new Color(0, 0, 0, .75f);
        yield return new WaitForSeconds(.05f);
        selectedImage.color = Color.white;
        yield return new WaitForSeconds(.05f);
        selectedImage.color = new Color(0, 0, 0, .75f);
        yield return new WaitForSeconds(.05f);
        LevelManager.Load(selectedLevel);
    }
}
