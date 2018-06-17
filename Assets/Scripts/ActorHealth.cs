using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// デリゲート
public delegate void ChangeHealthEventHandler(object sender, int value);

[System.Serializable]
public class ActorHealth
{
    /// <summary>現在HP</summary>
    [SerializeField]
    private int nowHealth = 0;

    /// <summary>最大HP</summary>
    [SerializeField]
    private int maxHealth = 999;


    /// <summary>現在HP</summary>
    public int NowHealth
    {
        get { return nowHealth; }
        set
        {
            int preNowHealth = nowHealth;
            nowHealth = value;

            // 変更があったならオブザーバーに通知
            if (preNowHealth != nowHealth)
            {
                OnChangeNowHealth(this, nowHealth);
            }
        }
    }

    /// <summary>最大HP</summary>
    public int MaxHealth
    {
        get { return maxHealth; }
        set
        {
            int preMaxHealth = maxHealth;
            maxHealth = value;

            // 変更があったならオブザーバーに通知
            if (preMaxHealth != maxHealth)
            {
                OnChangeNowHealth(this, nowHealth);
            }
        }
    }


    /// <summary>現在HP変動時のイベントハンドラ</summary>
    public ChangeHealthEventHandler ChangeNowHealthEventHandler = null;

    /// <summary>最大HP変動時のイベントハンドラ</summary>
    public ChangeHealthEventHandler ChangeMaxHealthEventHandler = null;


    /// <summary>現在HP変動時のコールバック</summary>
    private void OnChangeNowHealth(object sender, int health)
    {
        if (ChangeNowHealthEventHandler != null)
        {
            ChangeNowHealthEventHandler.Invoke(sender, health);
        }
    }

    /// <summary>最大HP変動時のコールバック</summary>
    private void OnChangeMaxHealth(object sender, int health)
    {
        if (ChangeMaxHealthEventHandler != null)
        {
            ChangeMaxHealthEventHandler.Invoke(sender, health);
        }
    }
}
