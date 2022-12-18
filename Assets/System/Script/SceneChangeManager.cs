using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneChangeManager : SingletonBehaviorBase<SceneChangeManager>
{
	#region ��`

	public enum SceneType
	{
		Title,
		InGame,
		Result,
	}

	public static readonly string SceneName_Title = "MasterTitle";
	public static readonly string SceneName_InGame = "MasterGame_Stage2";
	public static readonly string SceneName_Result = "MasterResult";

	#endregion // #region ��`

	#region �C���X�y�N�^�[���J

	[SerializeField]
	private float _TitleChangeTimeDef = 1.0f;

	[SerializeField]
	private AudioClip _TitleClick = null;

	[SerializeField]
	private float _TitleClick_Volume = 0.5f;

	#endregion // #region �C���X�y�N�^�[���J

	#region �v���p�e�B

	public SceneType CurrentScene { get; private set; } = SceneType.Title;

	public bool WaitChangeTitle { get; private set; } = false;

	#endregion // #region �v���p�e�B

	#region �t�B�[���h

	private float _TitleChangeTimer = 1.0f;

	#endregion // #region �t�B�[���h

	// Start is called before the first frame update
	void Start()
    {
		SceneManager.sceneLoaded += OnSceneLoaded;

		var activeSceneName = SceneManager.GetActiveScene().name;
		if(name == SceneName_Title)
		{
			CurrentScene = SceneType.Title;
		}
		else if (name == SceneName_InGame)
		{
			CurrentScene = SceneType.InGame;
		}
		else if (name == SceneName_Result)
		{
			CurrentScene = SceneType.Result;
		}
	}

    // Update is called once per frame
    void Update()
    {
		// �V�[���؂�ւ��X�V����
		updateChangeScene();
    }

	/// <summary>
	/// �V�[���؂�ւ����̏���
	/// </summary>
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		// �Q�[���V�[�����ǂݍ��܂ꂽ�珉����
		if (scene.name == SceneChangeManager.SceneName_Title)
		{
			_TitleChangeTimer = _TitleChangeTimeDef;
			WaitChangeTitle = false;
		}
	}

	#region �V�[���؂�ւ�

	/// <summary>
	/// �V�[���؂�ւ��X�V����
	/// </summary>
	private void updateChangeScene()
	{
		switch (CurrentScene)
		{
			case SceneType.Title:
				if (Input.GetKeyDown(KeyCode.Space) == true)
				{
					if (SoundManager.Instance != null)
					{
						SoundManager.Instance.playSE(_TitleClick, _TitleClick_Volume);
					}
					WaitChangeTitle = true;
				}
				if(WaitChangeTitle == true)
				{
					_TitleChangeTimer -= Time.deltaTime;
					if (_TitleChangeTimer <= 0.0f)
					{
						changeScene(SceneType.InGame);
					}
				}
				break;

			case SceneType.InGame:
				if (GameSystemManager.Instance != null)
				{
					if (GameSystemManager.Instance.CurrentGameState == GameSystemManager.GameState.Result_Clear ||
						GameSystemManager.Instance.CurrentGameState == GameSystemManager.GameState.Result_GameOver)
					{
						var bgm = BGMManager.Instance;
						if(bgm != null)
						{
							bgm.stopBGM();
						}
						changeScene(SceneType.Result);
					}
				}
				break;

			case SceneType.Result:
				if (Input.GetKeyDown(KeyCode.Space) == true)
				{
					changeScene(SceneType.Title);
				}
				break;
		}
	}

	/// <summary>
	/// �V�[���؂�ւ�
	/// </summary>
	public void changeScene(SceneType type)
	{
		// �����V�[���Ȃ牽�����Ȃ�
		if (type == CurrentScene)
			return;

		CurrentScene = type;

		var sceneName = "";

		switch (type)
		{
			case SceneType.Title:
				sceneName = SceneName_Title;
				break;

			case SceneType.InGame:
				sceneName = SceneName_InGame;
				break;

			case SceneType.Result:
				sceneName = SceneName_Result;
				break;
		}

		SceneManager.LoadScene(sceneName);
	}

	#endregion // #region �V�[���؂�ւ�
}
