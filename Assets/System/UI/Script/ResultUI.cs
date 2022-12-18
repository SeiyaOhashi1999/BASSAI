using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultUI : GUIBase
{
	[SerializeField]
	private GameObject _Panel = null;

	protected override void doStart()
	{
		var resultText = "Result";

		if (GameSystemManager.Instance != null)
		{
			switch(GameSystemManager.Instance.CurrentGameState)
			{
				case GameSystemManager.GameState.Result_Clear:
					resultText = "Clear!!";
					if(_Panel != null)
					{
						_Panel.SetActive(true);
					}
					break;

				case GameSystemManager.GameState.Result_GameOver:
					resultText = "GameOver";
					if (_Panel != null)
					{
						_Panel.SetActive(false);
					}
					break;
			}
		}

		setText(resultText);
	}

	// Update is called once per frame
	void Update()
    {
    }
}
