using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム全体管理クラス
/// </summary>
public class GameSystemManager : SingletonBehaviorBase<GameSystemManager>
{
	#region 定義

	public enum GameState
	{
		Ready,
		Play,
		Finish_ToClear,
		Finish_ToGameOver,
		Result_Clear,
		Result_GameOver,
	}

	#endregion // #region 定義

	#region インスペクター公開

	[SerializeField, InspectorName("制限時間")]
	public float LimitTime = 30.0f;

	[SerializeField, InspectorName("開始待機時間")]
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

	#endregion // #region インスペクター公開

	#region プロパティ

	public GameState CurrentGameState { get; private set; }

	/// <summary>
	/// 残り時間
	/// </summary>
	public float RestTime { get; private set; }

	/// <summary>
	/// 現在のスコア
	/// </summary>
	public int Score { get; private set; }

	/// <summary>
	/// 開始待機時間
	/// </summary>
	public float ReadyTime { get; private set; } = 1.0f;

	/// <summary>
	/// リザルト遷移までの時間
	/// </summary>
	public float FinishTime { get; private set; } = 2.0f;

	#endregion // #region プロパティ

	#region フィールド

	private bool _IsCalledReadySE = false;

	#endregion // #region フィールド

	// Start is called before the first frame update
	void Start()
    {
		SceneManager.sceneLoaded += OnSceneLoaded;

		// 初期化
		initGameParams();
	}

    // Update is called once per frame
    void Update()
    {
		// 残り時間更新
		updateTime();

		// ゲームステート更新
		updateGameState();

		// デバッグ用更新
		updateDebug();
	}

	#region 初期化

	/// <summary>
	/// 各種パラメータ初期化
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
	/// シーン切り替え時の処理
	/// </summary>
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		// ゲームシーンが読み込まれたら初期化
		if(scene.name == SceneChangeManager.SceneName_InGame)
		{
			initGameParams();
		}
	}

	#endregion //#region 初期化

	#region スコア

	private object _LockObj_Score = new object();

	private void initScore()
	{
		Score = 0;
	}

	/// <summary>
	/// スコア加算
	/// </summary>
	public void addScore(int value)
	{
		// プレイ中のみ加算
		if (CurrentGameState == GameState.Play)
		{
			lock (_LockObj_Score)
			{
				Score += value;
			}
		}
	}

	#endregion // #region スコア

	#region 時間

	/// <summary>
	/// 残り時間リセット
	/// </summary>
	private void initRestTime()
	{
		RestTime = LimitTime;
	}

	/// <summary>
	/// 残り時間更新
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

	#endregion // #region 時間

	#region ステート

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
	/// ゲームオーバー状態にする
	/// </summary>
	public void setGameOver()
	{
		// まだ終了状態じゃない場合のみ
		if (CurrentGameState != GameState.Finish_ToClear &&
			CurrentGameState != GameState.Finish_ToGameOver)
		{
			CurrentGameState = GameState.Finish_ToGameOver;
			SoundManager.Instance.playSE(_FinishSE, _FinishSE_Volume);
		}
	}

	#endregion // #region ステート

	#region デバッグ

	/// <summary>
	/// デバッグ用更新
	/// </summary>
	private void updateDebug()
	{
		//// 「O」でクリア
		//if (Input.GetKeyDown(KeyCode.O))
		//{
		//	CurrentGameState = GameState.Finish_ToClear;
		//}
		//// 「P」でゲームオーバー
		//else if (Input.GetKeyDown(KeyCode.P))
		//{
		//	CurrentGameState = GameState.Finish_ToGameOver;
		//}
		//// 「I」でゲームオーバー
		//else if (Input.GetKeyDown(KeyCode.I))
		//{
		//	RestTime = 0.0f;
		//}
	}

	#endregion // #region デバッグ
}
