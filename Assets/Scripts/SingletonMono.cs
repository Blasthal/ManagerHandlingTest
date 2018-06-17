using UnityEngine;
using System.Collections;

public class SingletonMono<T>
    : MonoBehaviour
    where T : SingletonMono<T>
{
    /// <summary>
    /// インスタンスを取得する。
    /// インスタンスが無い場合、作成する。
    /// </summary>
    public static T Instance
    {
        // Getter
        get
        {
            // インスタンスが無いなら
            if (_instance == null)
            {
                // Hierarchy内のオブジェクトから探す
                _instance = (T)FindObjectOfType(typeof(T));

                // 無い場合は生成する
                if (_instance == null)
                {
                    GameObject gameObject = new GameObject();
                    gameObject.name = typeof(T).Name;
                    _instance = gameObject.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    /// <summary>インスタンスの本体</summary>
    private static T _instance = null;
}