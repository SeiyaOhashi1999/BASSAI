using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

/// <summary>
/// �Q�[���S�̊Ǘ��N���X
/// </summary>
public class GameSystemManager : SingletonBehaviorBase<GameSystemManager>
{
	#region ��`

	public enum GameState
	{
		Ready,
		Play,
		Finish_ToClear,
		Finish_ToGameOver,
		Result_Clear,
		Result_GameOver,
	}

	#endregion // #region ��`

	#region �C���X�y�N�^�[���J

	[SerializeField, InspectorName("��������")]
	public float LimitTime = 30.0f;

	[SerializeField, InspectorName("�J�n�ҋ@����")]
	private float ReadyTimeDef = 2.0f;

	[SerializeField]
	private AudioClip _ReadySE = null;

	[SerializeField]
	private float _ReadySE_Volume = 0.5f;

	[SerializeField]
	private AudioClip _StartSE = null;

	[SerializeField]
	private float _StartSE_Volume = 0.5f;

	[SerializeField]
	private AudioClip _FinishSE = null;

	[SerializeField]
	private float _FinishSE_Volume = 0.5f;

	#endregion // #region �C���X�y�N�^�[���J

	#region �v���p�e�B

	public GameState CurrentGameState { get; private set; }

	/// <summary>
	/// �c�莞��
	/// </summary>
	public float RestTime { get; private set; }

	/// <summary>
	/// ���݂̃X�R�A
	/// </summary>
	public int Score { get; private set; }

	/// <summary>
	/// �J�n�ҋ@����
	/// </summary>
	public float ReadyTime { get; private set; } = 1.0f;

	/// <summary>
	/// ���U���g�J�ڂ܂ł̎���
	/// </summary>
	public float FinishTime { get; private set; } = 2.0f;

	#endregion // #region �v���p�e�B

	#region �t�B�[���h

	private bool _IsCalledReadySE = false;

	#endregion // #region �t�B�[���h

	// Start is called before the first frame update
	void Start()
    {
		SceneManager.sceneLoaded += OnSceneLoaded;

		// ������
		initGameParams();
	}

    // Update is called once per frame
    void Update()
    {
		// �c�莞�ԍX�V
		updateTime();

		// �Q�[���X�e�[�g�X�V
		updateGameState();

		// �f�o�b�O�p�X�V
		updateDebug();
	}

	#region ������

	/// <summary>
	/// �e��p�����[�^������
	/// </summary>
	private void initGameParams()
	{
		initRestTime();
		initScore();
		initGameState();
		FinishTime = 2.0f;
		ReadyTime = ReadyTimeDef;
		_IsCalledReadySE = false;
	}

	/// <summary>
	/// �V�[���؂�ւ����̏���
	/// </summary>
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		// �Q�[���V�[�����ǂݍ��܂ꂽ�珉����
		if(scene.name == SceneChangeManager.SceneName_InGame)
		{
			initGameParams();
		}
	}

	#endregion //#region ������

	#region �X�R�A

	private object _LockObj_Score = new object();

	private void initScore()
	{
		Score = 0;
	}

	/// <summary>
	/// �X�R�A���Z
	/// </summary>
	public void addScore(int value)
	{
		// �v���C���̂݉��Z
		if (CurrentGameState == GameState.Play)
		{
			lock (_LockObj_Score)
			{
				Score += value;
			}
		}
	}

	#endregion // #region �X�R�A

	#region ����

	/// <summary>
	/// �c�莞�ԃ��Z�b�g
	/// </summary>
	private void initRestTime()
	{
		RestTime = LimitTime;
	}

	/// <summary>
	/// �c�莞�ԍX�V
	/// </summary>
	private void updateTime()
	{
		switch (CurrentGameState)
		{
			case GameState.Ready:
				ReadyTime -= Time.deltaTime;
				break;

			case GameState.Play:
				ReadyTime -= Time.deltaTime;
				RestTime -= Time.deltaTime;
				break;

			case GameState.Finish_ToClear:
			case GameState.Finish_ToGameOver:
				FinishTime -= Time.deltaTime;
				break;
		}
	}

	#endregion // #region ����

	#region �X�e�[�g

	private void initGameState()
	{
		CurrentGameState = GameState.Ready;
	}

	private void updateGameState()
	{
		switch (CurrentGameState)
		{
			case GameState.Ready:
				if(_IsCalledReadySE == false)
				{
					SoundManager.Instance.playSE(_ReadySE, _ReadySE_Volume);
					_IsCalledReadySE = true;
				}
				if(ReadyTime <= 0.0f)
				{
					ReadyTime = ReadyTimeDef;
					CurrentGameState = GameState.Play;
					BGMManager.Instance.playInGameBGM();
					SoundManager.Instance.playSE(_StartSE, _StartSE_Volume);
				}
				break;

			case GameState.Play:
				if (RestTime <= 0.0f)
				{
					CurrentGameState = GameState.Finish_ToClear;
					SoundManager.Instance.playSE(_FinishSE, _FinishSE_Volume);
				}
				break;

			case GameState.Finish_ToClear:
				if(FinishTime <= 0.0f)
				{
					CurrentGameState = GameState.Result_Clear;
				}
				break;

			case GameState.Finish_ToGameOver:
				if (FinishTime <= 0.0f)
				{
					CurrentGameState = GameState.Result_GameOver;
				}
				break;
		}
	}

	/// <summary>
	/// �Q�[���I�[�o�[��Ԃɂ���
	/// </summary>
	public void setGameOver()
	{
		// �܂��I����Ԃ���Ȃ��ꍇ�̂�
		if (CurrentGameState != GameState.Finish_ToClear &&
			CurrentGameState != GameState.Finish_ToGameOver)
		{
			CurrentGameState = GameState.Finish_ToGameOver;
			SoundManager.Instance.playSE(_FinishSE, _FinishSE_Volume);
		}
	}

	#endregion // #region �X�e�[�g

	#region �f�o�b�O

	/// <summary>
	/// �f�o�b�O�p�X�V
	/// </summary>
	private void updateDebug()
	{
		//// �uO�v�ŃN���A
		//if (Input.GetKeyDown(KeyCode.O))
		//{
		//	CurrentGameState = GameState.Finish_ToClear;
		//}
		//// �uP�v�ŃQ�[���I�[�o�[
		//else if (Input.GetKeyDown(KeyCode.P))
		//{
		//	CurrentGameState = GameState.Finish_ToGameOver;
		//}
		//// �uI�v�ŃQ�[���I�[�o�[
		//else if (Input.GetKeyDown(KeyCode.I))
		//{
		//	RestTime = 0.0f;
		//}
	}

	#endregion // #region �f�o�b�O
}
