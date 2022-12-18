using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertSSE : MonoBehaviour
{
    public MikiGameManager mikiGameManager;

    bool flg = false;

    Material Barmat;
    void Start()
    {
        Barmat = this.gameObject.GetComponent<Renderer>().material;
    }
    // Update is called once per frame
    void Update()
    {
        if (flg)
        {//Barmat‚Ì“§–¾“x‚ð•Ï‚¦‚Ä“_–Å‚³‚¹‚é
            Barmat.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Miki miki))
        {
            SoundManager.Instance.playAlertSE();
            flg = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D collision)
    {
        flg = false;
        Barmat.color = new Color(1, 1, 1, 1);
    }
}
