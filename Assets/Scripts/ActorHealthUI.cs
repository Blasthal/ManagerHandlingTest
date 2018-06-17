using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class ActorHealthUI
    : MonoBehaviour
{
    public TextMesh health = null;

    private ActorHealth targetActorHealth = null;
    private int nowHealthValue = 0;
    private int maxHealthValue = 0;


    public void Initialize(ActorHealth actorHealth)
    {
        targetActorHealth = actorHealth;
        if (targetActorHealth != null)
        {
            // コールバック登録
            targetActorHealth.ChangeNowHealthEventHandler += updateNowHealth;
            targetActorHealth.ChangeMaxHealthEventHandler += updateMaxHealth;

            // 初期値設定
            nowHealthValue = targetActorHealth.NowHealth;
            maxHealthValue = targetActorHealth.MaxHealth;
        }

        updateHealthText();
    }

    private void updateNowHealth(object sender, int nowHealth)
    {
        nowHealthValue = nowHealth;
        updateHealthText();
    }
    private void updateMaxHealth(object sender, int maxHealth)
    {
        maxHealthValue = maxHealth;
        updateHealthText();
    }

    private void updateHealthText()
    {
        // 文字列設定
        string healthText = string.Empty;
        healthText = string.Format("{0:000}/{1:000}"
            , (int)nowHealthValue
            , (int)maxHealthValue
            );

        health.text = healthText;
    }
}

public partial class ActorHealthUI
{
    private bool isFirstOnGUI = true;
    private Rect windowRect = new Rect();
    

    private void OnGUI()
    {
        //
        GUIStyle buttonStyle = GUI.skin.button;
        buttonStyle.fontSize = 24;

        //
        {
            if (isFirstOnGUI)
            {
                Camera camera = Camera.main;

                // 初回位置調整
                const float OFFSET_POS_Y = 40.0f;
                Vector2 screenPos = camera.WorldToScreenPoint(this.transform.position);
                screenPos.y = camera.pixelHeight - screenPos.y;
                screenPos.y += OFFSET_POS_Y;
                windowRect.x = screenPos.x;
                windowRect.y = screenPos.y;
            }

            // Window作成
            windowRect = GUILayout.Window(
                GetInstanceID()
                , windowRect
                , WindowFunction
                , "HealthUI"
                );
        }

        // 初回OnGUIフラグを落とす
        if (isFirstOnGUI)
        {
            isFirstOnGUI = false;
        }
    }

    void WindowFunction(int windowID)
    {
        GUIStyle windowStyle = GUI.skin.window;
        windowStyle.fontSize = 15;

        GUIStyle labelStyle = GUI.skin.label;
        labelStyle.fontSize = 14;

        GUIStyle sliderStyle = GUI.skin.horizontalSlider;
        sliderStyle.fixedWidth = 150.0f;
        sliderStyle.fixedHeight = labelStyle.fontSize + 6.0f;

        GUIStyle sliderThumbStyle = GUI.skin.horizontalSliderThumb;
        sliderThumbStyle.fixedWidth = 16.0f;
        sliderThumbStyle.fixedHeight = sliderStyle.fixedHeight;


        //
        GUILayout.BeginVertical();
        {
            GUILayoutOption itemLabelWidth = GUILayout.Width(70.0f);
            GUILayoutOption hSliderWidth = GUILayout.Width(256.0f);

            //
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("現在HP", itemLabelWidth);

                nowHealthValue = (int)GUILayout.HorizontalSlider(
                    nowHealthValue
                    , 0.0f, 999.0f
                    );

                GUILayout.Label(
                    string.Format("{0, 9:000.00}", nowHealthValue)
                    , GUILayout.Width(100.0f)
                    );
            }
            GUILayout.EndHorizontal();

            //
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("最大HP", itemLabelWidth);

                maxHealthValue = (int)GUILayout.HorizontalSlider(
                    maxHealthValue
                    , 0.0f, 999.0f
                    );

                GUILayout.Label(
                    string.Format("{0, 9:000.00}", maxHealthValue)
                    , GUILayout.Width(100.0f)
                    );
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();

        //
        if (GUI.changed)
        {
            // GUI上の操作は実体側も上書きする
            if (targetActorHealth != null)
            {
                targetActorHealth.NowHealth = (int)nowHealthValue;
                targetActorHealth.MaxHealth = (int)maxHealthValue;
            }
        }

        // ドラッグ可能処理
        GUI.DragWindow(new Rect(0.0f, 0.0f, 10000.0f, 24.0f));
    }
}
