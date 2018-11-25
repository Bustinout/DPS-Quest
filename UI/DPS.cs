using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DPS : MonoBehaviour {

    public UnityEngine.UI.Text DamageDoneText;
    public UnityEngine.UI.Text enemyDamageDoneText;
    public UnityEngine.UI.Text DPSText;
    public UnityEngine.UI.Text enemyDPSText;

    public float battleStartTime;

    public Character player;
    public Character enemy;

    void Start()
    {
        battleStartTime = Time.time;
        StartCoroutine("DPSTick");
    }

    public void UpdateCall()
    {
        string damageDone = CurrencyConverter.Instance.convertDamage(player.totalDamageDealt);
        DamageDoneText.text = damageDone;
        string enemyDamageDone = CurrencyConverter.Instance.convertDamage(enemy.totalDamageDealt);
        enemyDamageDoneText.text = enemyDamageDone;
        PlayerPrefs.SetString("damageDone", damageDone);
        PlayerPrefs.SetString("enemyDamageDone", enemyDamageDone);
        setDPS();

        
    }

    public void setDPS()
    {
        float timeElapsed = Time.time - battleStartTime;

        if (timeElapsed > 0) {
            float DPSfloat = player.totalDamageDealt / timeElapsed;

            if (DPSfloat > Game.current.player.highestPeakDPS)
            {
                Game.current.player.highestPeakDPS = DPSfloat;
            }

            string DPS = CurrencyConverter.Instance.convertDamage(DPSfloat);
            string enemyDPS = CurrencyConverter.Instance.convertDamage((enemy.totalDamageDealt / timeElapsed));

            DPSText.text = DPS;
            enemyDPSText.text = enemyDPS;
            PlayerPrefs.SetString("DPS", DPS);
            PlayerPrefs.SetString("enemyDPS", enemyDPS);
            PlayerPrefs.SetString("timeElapsed", timeElapsed.ToString("f2"));

            PlayerPrefs.SetFloat("EndDPS", DPSfloat);
            PlayerPrefs.SetFloat("EndEnemyDamage", enemy.totalDamageDealt);
            PlayerPrefs.SetFloat("EndPlayerDamage", player.totalDamageDealt);
            PlayerPrefs.SetFloat("EndTimeElapsed", timeElapsed);
        }

    }

    IEnumerator DPSTick()
    {
        while (true)
        {
            setDPS();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
