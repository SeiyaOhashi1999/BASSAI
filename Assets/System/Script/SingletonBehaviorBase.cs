using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シングルトン基底。シーンをまたいで存在。多重に存在する場合は自信を破棄。
/// </summary>
public class SingletonBehaviorBase<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _Instance;

    public static T Instance
    {
        get
        {
            if(_Instance == null)
            {
                _Instance = FindObjectOfType<T>();
            }

            return _Instance;
        }
    }

	void Awake()
	{
		if(_Instance == null)
		{
			_Instance = this as T;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Debug.Log("【Singleton】既に存在するので削除しました。");
			Destroy(gameObject);
		}
	}
}
