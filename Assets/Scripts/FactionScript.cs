using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FactionScript : MonoBehaviour
{
    public string faction;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Play()
    {
        SceneManager.LoadScene("Main");
    }

    public void OptA()
    {
        faction = "A";
        Play();
    }
    public void OptB()
    {
        faction = "B";
        Play();
    }
    public void OptC()
    {
        faction = "C";
        Play();
    }
}
