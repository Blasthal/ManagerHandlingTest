using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActorModel
{
    /// <summary>HP</summary>
    public ActorHealth health;


    /// <summary>コンストラクタ</summary>
    public ActorModel()
    {
        health = new ActorHealth();
    }
}
