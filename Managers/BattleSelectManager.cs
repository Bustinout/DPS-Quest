using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleSelectManager : MonoBehaviour {

    //IMAGE THAT IS OVERRIDED DURING REFRESH
    public UnityEngine.UI.Text zoneNameText;
    public int currentPage; //zone number (FOR NOW IS 1)
    public int selectedZone;

    public GameObject page0;
    public GameObject page1;
    public GameObject page2;
    public GameObject page3;
    public GameObject page4;
    public GameObject page5;

    public GameObject[] pages;
    public string[] zoneNames;

    public int numberOfPages = 6;

    void Start()
    {
        pages = new GameObject[] { page0, page1, page2, page3, page4, page5 };
        zoneNames = new string[] { "Mysterious Island", "Desert Continent", "Mountain Continent", "Lava Continent", "Deap Sea", "Crashed Ship" };
        currentPage = PlayerPrefs.GetInt("LastPage");
        refreshPage();
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                ReturnToMenu();
            }
        }
    }

    public void nextPage()
    {
        currentPage = (currentPage + 1) % numberOfPages;
        refreshPage();
    }

    public void prevPage()
    {
        currentPage = (currentPage + numberOfPages - 1) % numberOfPages;
        refreshPage();
    }

    public void refreshPage()
    {
        for (int x = 0; x < 6; x++)
        {
            if (x == currentPage)
            {
                pages[x].SetActive(true);
                if (Game.current.player.zoneUnlock[x*4])
                {
                    zoneNameText.text = zoneNames[x];
                }
                else
                {
                    zoneNameText.text = "???";
                }
            }
            else
            {
                pages[x].SetActive(false);
            }
        }
        
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void toBossBattle()
    {
        PlayerPrefs.SetInt("LastPage", currentPage);
        PlayerPrefs.SetInt("Zone", selectedZone);
        PlayerPrefs.SetInt("Battle", 4);
        SceneManager.LoadScene("Battle");
    }

    public void toBattle()
    {
        PlayerPrefs.SetInt("LastPage", currentPage);
        PlayerPrefs.SetInt("Zone", selectedZone);
        randomBattle();
        SceneManager.LoadScene("Battle");
    }

    public void randomBattle()
    {
        float rand = Random.value;

        if (rand < .3)
        {
            PlayerPrefs.SetInt("Battle", 0);
        }
        else if (rand < .6)
        {
            PlayerPrefs.SetInt("Battle", 1);
        }
        else if (rand < .9)
        {
            PlayerPrefs.SetInt("Battle", 2);
        }
        else
        {
            PlayerPrefs.SetInt("Battle", 3);
        }
    }




}
