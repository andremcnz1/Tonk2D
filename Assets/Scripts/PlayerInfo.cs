using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{ 
    Local,
    EnemyAI,
    AllyAI,
    Enemy,
    Ally
};

public class PlayerInfo : MonoBehaviour
{
    public PlayerType playerType;
    public string Tag;

    private void Awake()
    {
        Tag = gameObject.tag;
    }
};
