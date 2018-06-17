using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
    : SingletonMono<GameManager>
{
    public const int MAX_ACTOR_COUNT = 3;

    public Actor originalActor = null;

    [System.NonSerialized]
    public Actor[] actors = new Actor[MAX_ACTOR_COUNT];


    private void Awake()
    {
        SetupOnAwake();
    }


    private void SetupOnAwake()
    {
        // 名前テーブル
        string[] objNameTable = new string[MAX_ACTOR_COUNT]
        {
            "Actor1",
            "Actor2",
            "Actor3",
        };

        // 生成
        for (int i = 0; i < actors.Length; ++i)
        {
            Actor clone = Instantiate(originalActor);
            clone.gameObject.SetActive(true);
            actors[i] = clone;

            // 名前
            string objName = objNameTable[i];
            clone.name = objName;

            // 位置
            const float START_Y = 4.0f;
            const float SPACE_Y = 2.5f;
            Vector3 pos = Vector3.zero;
            pos.y += i * -SPACE_Y + START_Y;
            clone.transform.position = pos;
        }
    }
}
