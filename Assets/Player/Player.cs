using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    public float buffLength = 5f;
    public PlayerData data;
    SpriteRenderer playerSprite;
    Material playerMat;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnInvulnerable, OnInvulnerable);
        EventManager.AddHandler(GameEvent.OnVulnerable, OnVulnerable);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnInvulnerable, OnInvulnerable);
        EventManager.RemoveHandler(GameEvent.OnVulnerable, OnVulnerable);
    }

    private void Start()
    {
        ClearData();
        playerSprite = GetComponent<SpriteRenderer>();
        playerMat = playerSprite.material;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        switch (other.tag)
        {
            case "Ground":
                EventManager.Broadcast(GameEvent.OnPlayerLanded);
                break;
            case "Enemy":
                if (data.livesLeft >= 2)
                {
                    data.livesLeft -= 1;
                    EventManager.Broadcast(GameEvent.OnDeath);
                }
                else
                {
                    EventManager.Broadcast(GameEvent.OnGameOver);
                }
                break;
            case "Star":
                    EventManager.Broadcast(GameEvent.OnInvulnerable);
                    Destroy(other.gameObject);
                break;

            case "Coin":
                    EventManager.Broadcast(GameEvent.OnIncreaseScore);
                    Destroy(other.gameObject);
                break;
            default: break;
        }
    }

    void OnInvulnerable()
    {
        data.isInvulnerable = true;
        StartCoroutine("Buff");
    }
    void OnVulnerable()
    {
        data.isInvulnerable = false;
    }

    IEnumerator Buff()
    {
        float temp = 0f;
        while (temp <= buffLength)
        {
            playerMat.color = Random.ColorHSV(0, 1, 0, 1, 0, 1);
            temp += Time.deltaTime;
            yield return null;
        }
        playerMat.color = new Color(1, 1, 1, 1);
        EventManager.Broadcast(GameEvent.OnVulnerable);
    }

    void ClearData(){
        data.isInvulnerable = false;
        data.isLanded = true;
        data.livesLeft = 3;
        data.speed = 0;
    }
}
