using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �V���O���g�����B�V�[�����܂����ő��݁B���d�ɑ��݂���ꍇ�͎��M��j���B
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
			Debug.Log("�ySingleton�z���ɑ��݂���̂ō폜���܂����B");
			Destroy(gameObject);
		}
	}
}
