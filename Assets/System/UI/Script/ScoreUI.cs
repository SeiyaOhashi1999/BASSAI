using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUI : GUIBase
{
    // Update is called once per frame
    void Update()
    {
		if (GameSystemManager.Instance)
		{
			setText("Score : " + GameSystemManager.Instance.Score);
		}
    }
}
