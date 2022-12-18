using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : SingletonBehaviorBase<BGMManager>
{
	[SerializeField]
	private AudioClip _InGameBGM = null;

	[SerializeField]
	private float _InGameBGM_Volume = 0.2f;

	#region フィールド

	private AudioSource _Audio = null;

	#endregion // #region フィールド

	// Start is called before the first frame update
	void Start()
    {
		_Audio = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
    {
        
    }


	#region BGM

	public void playInGameBGM()
	{
		if (_Audio != null)
		{
			_Audio.clip = _InGameBGM;
			_Audio.volume = _InGameBGM_Volume;
			_Audio.Play();
		}
	}

	public void stopBGM()
	{
		if (_Audio != null)
		{
			_Audio.Stop();
		}
	}

	#endregion // #region BGM
}
