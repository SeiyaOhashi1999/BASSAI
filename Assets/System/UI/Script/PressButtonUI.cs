using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButtonUI : GUIBase
{
	#region �C���X�y�N�^�[���J

	[SerializeField, InspectorName("�_�Ŏ���")]
	private float VisibleTime = 1.0f;

	#endregion // #region �C���X�y�N�^�[���J

	#region �t�B�[���h

	private float ChangeTimer = 0.0f;

	private bool IsVisible = true;

	#endregion // #region �t�B�[���h

	void Awake()
	{
		initTime();
	}

    // Update is called once per frame
    void Update()
    {
		// �^�C�g���J�ڑ҂��͍X�V���Ȃ�
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
