using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class journalPage : MonoBehaviour {

    public UnityEngine.UI.Text titleText;
    public UnityEngine.UI.Text paragraphText;
    public UnityEngine.UI.Text pageNumberText;

    public int currentIndex;
    public int[] journalIndexes;

    public string[] JournalEntries;

    public bool unlocked;
    public GameObject controls;

    // Use this for initialization
    void Start () {
        currentIndex = 0;
        getJournalIndexes();
        writeEntries();
        refresh();
    }
	
    public void refresh()
    {
        if (journalIndexes.Length <= 1)
        {
            controls.SetActive(false);
        }
        else
        {
            titleText.text = "Entry " + (journalIndexes[currentIndex] + 1);
            paragraphText.text = JournalEntries[journalIndexes[currentIndex]];
            pageNumberText.text = (currentIndex+1) + "/" + journalIndexes.Length;
        }
        
    }

    public void nextPage()
    {
        currentIndex = (currentIndex + 1) % journalIndexes.Length;
        refresh();
    }

    public void prevPage()
    {
        currentIndex = (currentIndex + journalIndexes.Length - 1) % journalIndexes.Length;
        refresh();
    }

    public void getJournalIndexes()
    {
        List<int> indexList = new List<int>();

        for (int x = 0; x < Game.current.player.journalEntryUnlock.Length; x++)
        {
            if (Game.current.player.journalEntryUnlock[x])
            {
                indexList.Add(x);
            }
        }
        journalIndexes = indexList.ToArray();
    }

    public void writeEntries()
    {
        JournalEntries = new string[Game.current.player.journalEntryUnlock.Length];
        JournalEntries[0] = "Work is da poop.";
        JournalEntries[1] = "Zug zug.";
    }


}
