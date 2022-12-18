using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonBehaviorBase<SoundManager>
{
	#region �C���X�y�N�^�[���J

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

	#endregion // #region �C���X�y�N�^�[���J

	#region �t�B�[���h

	private AudioSource _Audio = null;

	#endregion // #region �t�B�[���h

	// Start is called before the first frame update
	void Start()
    {
		_Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	#region ���ʉ�

	public void playSE(AudioClip clip, float volume = 1.0f)
	{
		if(_Audio != null)
		{
			_Audio.PlayOneShot(clip, volume);
		}
	}

	/// <summary>
	/// �x��SE�Đ�
	/// </summary>
	public void playAlertSE()
	{
		if(_Audio != null)
		{
			var game = GameSystemManager.Instance;
			if(game != null)
			{
				// �N���A�I���҂��Ȃ牽�����Ȃ�
				if(game.CurrentGameState == GameSystemManager.GameState.Finish_ToClear)
				{
					return;
				}
			}

			_Audio.PlayOneShot(_AlertSE, _AlertSE_Volume);
		}
	}

	/// <summary>
	/// �_���[�WSE�Đ�
	/// </summary>
	public void playDamageSE()
	{
		if (_Audio != null)
		{
			_Audio.PlayOneShot(_DamageSE, _DamageSE_Volume);
		}
	}

	/// <summary>
	/// �ؐ���SE�J�n
	/// </summary>
	public void playTreeCreateSE()
	{
		if (_Audio != null)
		{
			_Audio.PlayOneShot(_TreeCreateSE, _TreeCreateSE_Volume);
		}
	}

	/// <summary>
	/// �ؐ���SE�Đ�
	/// </summary>
	public void playTreeGrowSE()
	{
		if (_Audio != null)
		{
			_Audio.PlayOneShot(_TreeGrowSE, _TreeGrowSE_Volume);
		}
	}

	#endregion // #region ���ʉ�
}
