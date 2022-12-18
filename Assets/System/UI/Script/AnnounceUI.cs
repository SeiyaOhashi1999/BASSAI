using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnounceUI : GUIBase
{
    // Update is called once per frame
    void Update()
    {
		switch (GameSystemManager.Instance.CurrentGameState)
		{
			case GameSystemManager.GameState.Ready:
				setText("Ready...");
				break;

			case GameSystemManager.GameState.Finish_ToClear:
			case GameSystemManager.GameState.Finish_ToGameOver:
				setText("Finish!!");
				setVisible(true);
				break;

			default:
				setText("Go!!");
				if(GameSystemManager.Instance.ReadyTime <= 0.0f)
				{
					setVisible(false);
				}
				break;
		}
    }
}
