using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public abstract class LevelManager
{
    public static void Reload ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void Load (String scene)
    {
        SceneManager.LoadScene(scene);
    }
    // Gets the scene index and returns a string value for class. Used primarily to determine which background music should be played.
    public static string SceneClass ()
    {
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                return "Splash";
            case int n when n >= 1 && n <= 4:
                return "Menu";
            case int n when n >= 5 && n <= 8:
                return "Level";
            case 9:
                return "Credits";
            default: return "";
        }
        
    }
}
