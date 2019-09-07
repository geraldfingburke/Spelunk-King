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
}
