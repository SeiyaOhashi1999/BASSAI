using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemTest : MonoBehaviour
{
	public AudioClip _clip = null;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.U))
		{
			BGMManager.Instance.playInGameBGM();
		}
		if (Input.GetKeyDown(KeyCode.I))
		{
			BGMManager.Instance.stopBGM();
		}
	}
}
