using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JournalManager : MonoBehaviour {

    public bool displayingJournal;
    public bool displayingMonsters;
    public bool displayingStats;

    public Image BGIMAGE;

    public int currentMonsterCard;
    public GameObject journalPage;
    public GameObject monsterCard;
    public GameObject statisticsPage;

    // Use this for initialization
    void Start () {
        displayJournal();
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

    public void displayJournal()
    {
        if (!displayingJournal)
        {
            displayingJournal = true;
            displayingMonsters = false;
            displayingStats = false;

            journalPage.SetActive(true);
            monsterCard.SetActive(false);
            statisticsPage.SetActive(false);

            BGIMAGE.overrideSprite = Resources.Load<Sprite>("Sprites/UI/journalBGjournal");
        }
    }

    public void displayMonsters()
    {
        if (!displayingMonsters)
        {
            displayingJournal = false;
            displayingMonsters = true;
            displayingStats = false;

            journalPage.SetActive(false);
            monsterCard.SetActive(true);
            statisticsPage.SetActive(false);

            BGIMAGE.overrideSprite = Resources.Load<Sprite>("Sprites/UI/journalBGmonster");
        }
    }

    public void displayStatistics()
    {
        if (!displayingStats)
        {
            displayingJournal = false;
            displayingMonsters = false;
            displayingStats = true;

            journalPage.SetActive(false);
            monsterCard.SetActive(false);
            statisticsPage.SetActive(true);

            BGIMAGE.overrideSprite = Resources.Load<Sprite>("Sprites/UI/journalBGstats");
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    
}
