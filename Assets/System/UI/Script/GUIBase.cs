using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

/// <summary>
/// GUI���
/// </summary>
public class GUIBase : MonoBehaviour
{
	#region �t�B�[���h

	private TextMeshProUGUI _TextUI = null;

	#endregion // #region �t�B�[���h

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

	#region UI����

	/// <summary>
	/// �e�L�X�g�ݒ�
	/// </summary>
	public void setText(string text)
	{
		if(_TextUI != null)
		{
			_TextUI.text = text;
		}
	}

	/// <summary>
	/// �e�L�X�g�\���ݒ�
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

	#endregion // #region UI����
}
