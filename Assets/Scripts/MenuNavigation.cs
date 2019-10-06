using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    public int position = 0;

    public Image start;
    public Image credits;

    public AudioClip selectionClip;

    private bool vAxisInUse = false;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ListenForInput", .1f, .1f);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (position)
        {
            case 0:
                start.color = new Color(.75f, .75f, 1);
                credits.color = Color.white;
                break;
            case 1:
                start.color = Color.white;
                credits.color = new Color(.75f, .75f, 1);
                break;
        }
        if (Input.GetButtonDown("PlayerOneShoot"))
        {
            StartCoroutine("Selected");
        }

        Debug.Log(Input.GetAxis("PlayerOneHorizontal") + " : " + Input.GetAxis("PlayerOneVertical"));
    }

    public void ListenForInput ()
    {
        
        if (Input.GetAxisRaw("PlayerOneVertical") > 0 && position > 0 && vAxisInUse == false)
        {
            position--;
            vAxisInUse = true;
        }
        else if (Input.GetAxisRaw("PlayerOneVertical") < 0 && position < 1 && vAxisInUse == false)
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
        }
        audioSource.Play();
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
