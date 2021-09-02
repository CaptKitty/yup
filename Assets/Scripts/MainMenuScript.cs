using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public void Launch()
    {
        DeleteSaveFile();
        SceneManager.LoadScene("Faction");
    }

    public void DeleteSaveFile()
    {
        if(File.Exists(Application.persistentDataPath + "/Savefile.jay"))
        {
            File.Delete(Application.persistentDataPath + "/Savefile.jay");
            //print("Savefile Deleted");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Continue()
    {
        SceneManager.LoadScene("Main");
    }
}
