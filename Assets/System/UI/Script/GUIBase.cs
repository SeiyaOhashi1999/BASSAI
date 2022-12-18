using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

/// <summary>
/// GUI基底
/// </summary>
public class GUIBase : MonoBehaviour
{
	#region フィールド

	private TextMeshProUGUI _TextUI = null;

	#endregion // #region フィールド

	// Start is called before the first frame update
	public void Start()
    {
		_TextUI = gameObject.GetComponent<TextMeshProUGUI>();

		doStart();
	}

    // Update is called once per frame
    void Update()
    {
    }

	protected virtual void doStart()
	{
	}

	#region UI操作

	/// <summary>
	/// テキスト設定
	/// </summary>
	public void setText(string text)
	{
		if(_TextUI != null)
		{
			_TextUI.text = text;
		}
	}

	/// <summary>
	/// テキスト表示設定
	/// </summary>
	public void setVisible(bool visible)
	{
		if (_TextUI == null)
			return;

		if(visible == true)
		{
			_TextUI.alpha = 1.0f;
		}
		else
		{
			_TextUI.alpha = 0.0f;
		}
	}

	#endregion // #region UI操作
}
