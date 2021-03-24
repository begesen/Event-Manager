using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    public TextMeshProUGUI score,coin,time;


    public GameData gameData;
    public PlayerData playerData;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnUpdateUI, OnUIUpdate);
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnUpdateUI, OnUIUpdate);
    }

    void OnUIUpdate()
    {
        score.SetText(gameData.score.ToString());
        coin.SetText("x" + gameData.coins.ToString());
        time.SetText(gameData.time.ToString("f0"));
    }
}
