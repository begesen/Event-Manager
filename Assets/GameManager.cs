using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameData gameData;
    public PlayerData playerData;

    private void Start() {
        InvokeRepeating("UpdateSeconds",0f,1f);
        ClearData();
    }

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnInvulnerable, OnInvulnerable);
        EventManager.AddHandler(GameEvent.OnIncreaseScore, OnIncreaseScore);
        EventManager.AddHandler(GameEvent.OnSlimeKilled, OnSlimeKilled);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnInvulnerable, OnInvulnerable);
        EventManager.RemoveHandler(GameEvent.OnIncreaseScore, OnIncreaseScore);
        EventManager.RemoveHandler(GameEvent.OnSlimeKilled, OnSlimeKilled);
    }
    void OnSlimeKilled(){
        gameData.score += 555;
        EventManager.Broadcast(GameEvent.OnUpdateUI);
    }
    void OnInvulnerable()
    {
        playerData.isInvulnerable = true;
        gameData.score += 1000;
    
        EventManager.Broadcast(GameEvent.OnUpdateUI);
    }

    void OnIncreaseScore()
    {
        gameData.coins += 1;
        gameData.score += 50;

        EventManager.Broadcast(GameEvent.OnUpdateUI);
    }

    void UpdateSeconds(){
        gameData.time += 1;
        EventManager.Broadcast(GameEvent.OnUpdateUI);
    }
    void ClearData(){
        gameData.coins = 0;
        gameData.score = 0;
        gameData.time = 0;
        gameData.world = new Vector2(1,1);
    }
}
