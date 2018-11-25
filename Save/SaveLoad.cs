using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public static class SaveLoad {

    public static List<Game> savedGames = new List<Game>();
    public static Game saveToDeleteOnSave;

    public static float lastTime = 0;

    public static void Save()
    {
        //addTime();

        setFavorite();
        overWrite();
        saveToDeleteOnSave = Game.current;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/DPSQuest.save"); //you can call it anything you want
        bf.Serialize(file, SaveLoad.savedGames);
        file.Close();
    }

    public static void saveEmpty() //for when deleting final save file and saving with no game.current
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/DPSQuest.save"); //you can call it anything you want
        bf.Serialize(file, SaveLoad.savedGames);
        file.Close();
    }
    public static void deleteSave()
    {
        setFavorite();
        saveToDeleteOnSave = Game.current;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/DPSQuest.save"); //you can call it anything you want
        bf.Serialize(file, SaveLoad.savedGames);
        file.Close();
    }


    public static void loadSave(Game loadingChar)
    {
        Game.current = loadingChar;
        setFavorite();
        saveToDeleteOnSave = Game.current;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/DPSQuest.save"); //you can call it anything you want
        bf.Serialize(file, SaveLoad.savedGames);
        file.Close();
        SceneManager.LoadScene("Menu");
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/DPSQuest.save"))
        {
            Debug.Log("not supposed to happen on reset");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/DPSQuest.save", FileMode.Open);
            SaveLoad.savedGames = (List<Game>)bf.Deserialize(file);
            file.Close();
            setGameAtStart();
        }
        else
        {
            NewGame();
        }
    }

    public static void NewGame()
    {
        SceneManager.LoadScene("CharacterCreation");
    }

    public static void setGameAtStart()
    {
        foreach (Game g in SaveLoad.savedGames)
        {
            if (g.favorite == true)
            {
                Game.current = g;
                saveToDeleteOnSave = g;
            }
        }
    }

    public static void setFavorite()
    {
        
        foreach (Game g in SaveLoad.savedGames)
        {
            g.favorite = false;
        }
        Game.current.favorite = true;
         
    }

    public static void overWrite()
    {
        int index = savedGames.IndexOf(saveToDeleteOnSave);
        if (index != -1)
        {
            savedGames[index] = Game.current;
        }
        else
        {
            savedGames.Add(Game.current);
        }
    }

    public static void addTime() //adds totalTimeElapsed
    {
        float snapTime = Time.time;
        float timeElapsed = (snapTime - lastTime);
        Game.current.player.timeSpent += timeElapsed;

        lastTime = snapTime;

        //Debug.Log("Elapsed: "+timeElapsed);
    }
}
