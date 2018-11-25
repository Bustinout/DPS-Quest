using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour {

    private bool paused;
    public Character self;
    public Image pauseImage;
    public UnityEngine.UI.Text PauseText;
    public GameObject popup;
    public Image playerModel;

    // Use this for initialization
    void Start () {
        paused = false;
        popup.SetActive(false);
        playerModel.overrideSprite = Resources.Load<Sprite>(Game.current.player.spriteLocation);
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                openPopup();
            }
        }
    }

    public void Lose()
    {
        unPause();
        Game.current.player.flees += 1; //FLEE STATS
        SceneManager.LoadScene("BattleEndScreen");
    }

    public void unPause()
    {
        Time.timeScale = 1;
        paused = false;
        self.paused = false;
        PauseText.text = "";
    }

    public void Pause()
    {
        if (paused)
        {
            unPause();
            pauseImage.overrideSprite = Resources.Load<Sprite>("Sprites/UI/Pause");
        }
        else
        {
            Time.timeScale = 0;
            paused = true;
            self.paused = true;
            PauseText.text = "Paused";
            pauseImage.overrideSprite = Resources.Load<Sprite>("Sprites/UI/Resume");
        }
    }

    public void openPopup()
    {
        popup.SetActive(true);
        if (paused)
        {

        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public void closePopup()
    {
        popup.SetActive(false);
        if (paused)
        {

        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
