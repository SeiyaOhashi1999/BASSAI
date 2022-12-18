using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButtonUI : GUIBase
{
	#region インスペクター公開

	[SerializeField, InspectorName("点滅時間")]
	private float VisibleTime = 1.0f;

	#endregion // #region インスペクター公開

	#region フィールド

	private float ChangeTimer = 0.0f;

	private bool IsVisible = true;

	#endregion // #region フィールド

	void Awake()
	{
		initTime();
	}

    // Update is called once per frame
    void Update()
    {
		// タイトル遷移待ちは更新しない
		var scene = SceneChangeManager.Instance;
		if(scene != null)
		{
			if(scene.CurrentScene == SceneChangeManager.SceneType.Title)
			{
				if(scene.WaitChangeTitle == true)
				{
					return;
				}
			}
		}

		ChangeTimer -= Time.deltaTime;
		if(ChangeTimer <= 0.0f)
		{
			IsVisible = !IsVisible;
			setVisible(IsVisible);
			initTime();
		}
    }

	private void initTime()
	{
		ChangeTimer = VisibleTime;
	}
}
