using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;



    public void Play()
    {
        SceneManager.LoadScene("SelectionLevel", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GoLevel(int nLevel)
    {
        string nameLevel = "Game" + nLevel.ToString();

        SceneManager.LoadScene(nameLevel, LoadSceneMode.Single);
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("MaxLevel", 0) >= 1)
        {
            button2.SetActive(true);
        }

        if (PlayerPrefs.GetInt("MaxLevel", 0) >= 2)
        {
            button3.SetActive(true);
        }

        if (PlayerPrefs.GetInt("MaxLevel", 0) >= 3)
        {
            button4.SetActive(true);
        }
    }
}
