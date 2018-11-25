using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game {

    public static Game current;
    public static Spell[] SpellLibrary;
    public CharacterSave player;
    public bool favorite;
    public int key;

    public Game()
    {
        player = new CharacterSave();
        favorite = false;
        key = generateKey();
    }

    public Game(CharacterSave newPlayer)
    {
        player = newPlayer;
        favorite = false;
        key = generateKey();
    }

    public int generateKey()
    {
        return Random.Range(1000000,10000000);
    }

    public void initializeSpellBook()
    {

    }
}
