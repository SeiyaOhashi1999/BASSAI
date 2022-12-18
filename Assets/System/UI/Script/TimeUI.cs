using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
	private Slider _TimeGauge = null;

	private void Start()
	{
		_TimeGauge = GetComponent<Slider>();
	}

	// Update is called once per frame
	void Update()
    {
		updateGauge();

		//var dispTime = GameSystemManager.Instance.RestTime;
		//if(dispTime < 0.0f)
		//{
		//	dispTime = 0.0f;
		//}

		//dispTime++;
		//if(dispTime > GameSystemManager.Instance.LimitTime)
		//{
		//	dispTime = GameSystemManager.Instance.LimitTime;
		//}
		//if(GameSystemManager.Instance.RestTime <= 0.0f)
		//{
		//	dispTime = 0.0f;
		//}

		//setText("Time : " + (int)dispTime);
	}

	private void updateGauge()
	{
		var game = GameSystemManager.Instance;
		if (game == null)
			return;

		if (_TimeGauge == null)
			return;

		var baseTime = game.LimitTime;
		if(baseTime <= 0.0f)
		{
			baseTime = 1.0f;
		}

		var timeRate = game.RestTime / baseTime;

		// ‘‚¦‚é•\Ž¦‚É‚·‚é‚Ì‚Å”½“]
		timeRate = 1.0f - timeRate;

		_TimeGauge.value = timeRate;
	}
}
