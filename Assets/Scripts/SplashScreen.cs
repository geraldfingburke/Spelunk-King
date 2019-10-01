using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FadeIn");   
    }

    public IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(2);
        LevelManager.Load("01A_Start");
    }
}
