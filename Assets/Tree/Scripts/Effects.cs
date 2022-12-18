using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Effects : MonoBehaviour
{
    //プレイヤーがジャンプした時に出すエフェクト
    public GameObject jumpEffect;
    public GameObject moveEffect;
    GameObject player;

    int cnt = 0;
    float posx = 0;

    AudioSource audioSource;
    public AudioClip SEjump;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーがジャンプした時にエフェクトを出す
        if (player.GetComponent<Player>().isJump)
        {
            SoundManager.Instance.playSE(SEjump, 0.1f);
            Instantiate(jumpEffect, player.transform.position, Quaternion.identity);
            player.GetComponent<Player>().isJump = false;
        }
        //プレイヤーが移動した時にエフェクトを出す
        if(cnt == 1)
        {
            posx = player.transform.position.x;
        }
        else if(cnt == 10)
        {
            if (Mathf.Abs(posx - player.transform.position.x) > 0.2f)
            {
                Debug.Log("move");
                Instantiate(moveEffect, player.transform.position, Quaternion.identity);
                cnt = 0;
            }
        }

        cnt++;
    }
}
