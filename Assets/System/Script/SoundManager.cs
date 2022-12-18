using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonBehaviorBase<SoundManager>
{
	#region インスペクター公開

	[SerializeField]
	private AudioClip _AlertSE = null;

	[SerializeField]
	private float _AlertSE_Volume = 0.5f;

	[SerializeField]
	private AudioClip _DamageSE = null;

	[SerializeField]
	private float _DamageSE_Volume = 0.5f;

	[SerializeField]
	private AudioClip _TreeCreateSE = null;

	[SerializeField]
	private float _TreeCreateSE_Volume = 0.5f;

	[SerializeField]
	private AudioClip _TreeGrowSE = null;

	[SerializeField]
	private float _TreeGrowSE_Volume = 0.5f;

	#endregion // #region インスペクター公開

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

	#region 効果音

	public void playSE(AudioClip clip, float volume = 1.0f)
	{
		if(_Audio != null)
		{
			_Audio.PlayOneShot(clip, volume);
		}
	}

	/// <summary>
	/// 警告SE再生
	/// </summary>
	public void playAlertSE()
	{
		if(_Audio != null)
		{
			var game = GameSystemManager.Instance;
			if(game != null)
			{
				// クリア終了待ちなら何もしない
				if(game.CurrentGameState == GameSystemManager.GameState.Finish_ToClear)
				{
					return;
				}
			}

			_Audio.PlayOneShot(_AlertSE, _AlertSE_Volume);
		}
	}

	/// <summary>
	/// ダメージSE再生
	/// </summary>
	public void playDamageSE()
	{
		if (_Audio != null)
		{
			_Audio.PlayOneShot(_DamageSE, _DamageSE_Volume);
		}
	}

	/// <summary>
	/// 木生成SE開始
	/// </summary>
	public void playTreeCreateSE()
	{
		if (_Audio != null)
		{
			_Audio.PlayOneShot(_TreeCreateSE, _TreeCreateSE_Volume);
		}
	}

	/// <summary>
	/// 木成長SE再生
	/// </summary>
	public void playTreeGrowSE()
	{
		if (_Audio != null)
		{
			_Audio.PlayOneShot(_TreeGrowSE, _TreeGrowSE_Volume);
		}
	}

	#endregion // #region 効果音
}
