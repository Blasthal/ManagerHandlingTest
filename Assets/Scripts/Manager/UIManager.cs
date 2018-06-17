using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
    : SingletonMono<UIManager>
{
    public ActorHealthUI prefabHealthUI = null;

    private ActorHealthUI[] healthUIs = new ActorHealthUI[GameManager.MAX_ACTOR_COUNT];


    private void Start()
    {
        SetupOnStart();
    }


    private void SetupOnStart()
    {
        string[] objNameTable = new string[GameManager.MAX_ACTOR_COUNT]
        {
            "HealthUI_1",
            "HealthUI_2",
            "HealthUI_3",
        };

        for (int i = 0; i < healthUIs.Length; ++i)
        {
            // AddComponentだけでは階層構造を維持するのに手間がかかるので、
            // prefabを利用するのがベターかも。
            ActorHealthUI clone = Instantiate(prefabHealthUI);
            clone.gameObject.SetActive(true);
            healthUIs[i] = clone;
            
            // 初期化
            GameManager gm = GameManager.Instance;
            Actor actor = gm.actors[i];
            healthUIs[i].transform.SetParent(actor.transform, false);

            ActorHealth health = actor.actorModel.health;
            healthUIs[i].Initialize(health);

            // 名前
            clone.name = objNameTable[i];
        }
    }
}
