using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSoundController : MonoBehaviour
{
	[SerializeField]
	private AudioClip _ClearSE = null;

	[SerializeField]
	private float _ClearSE_Volume = 0.5f;

	[SerializeField]
	private AudioClip _GameOverSE = null;

	[SerializeField]
	private float _GameOverSE_Volume = 0.5f;

	// Start is called before the first frame update
	void Start()
    {
		var game = GameSystemManager.Instance;
		if (game == null)
			return;

		var sound = SoundManager.Instance;
		if (sound == null)
			return;

		switch (game.CurrentGameState)
		{
			case GameSystemManager.GameState.Result_Clear:
				sound.playSE(_ClearSE, _ClearSE_Volume);
				break;

			case GameSystemManager.GameState.Result_GameOver:
				sound.playSE(_GameOverSE, _GameOverSE_Volume);
				break;
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
