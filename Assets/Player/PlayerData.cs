using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "Data/Player Data", order = 0)]
public class PlayerData : ScriptableObject
{
    public float speed = 10f;
    public bool isLanded = true;
    public bool isInvulnerable = false;
    public int livesLeft = 3;
}
