using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTree : MonoBehaviour
{
    [SerializeField] int HP = 2;
    [SerializeField] int time; //消えるまでの時間

    [SerializeField] GameObject Treecutparticle;

    AudioSource audioSource;
    [SerializeField] AudioClip sound1;
    [SerializeField] AudioClip sound2;

    GameObject Ono;
    bool flg = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        Destroy(this.gameObject, time);
    }

    // Update is called once per frame
    void Update()
    {
        if (flg)
        {
            HP -= 1;
            if(HP == 1)
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0.4f, 0, 0.5f);
            }
            
            Debug.Log(HP);
            if (HP <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        
        flg = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //他のオブジェクトのタグが"Ono"だったら
        if (collision.gameObject.tag == "Ono")
        {
            Ono = GameObject.Find("Ono");
            Instantiate(Treecutparticle, Ono.transform.position, Quaternion.identity);
            HP--;
            if (HP == 1)
            {
                SoundManager.Instance.playSE(sound1, 0.2f);
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0.4f, 0, 0.5f);
            }

            Debug.Log(HP);
            if (HP <= 0)
            {
                SoundManager.Instance.playSE(sound2, 0.2f);
                GameSystemManager.Instance.addScore(10);
                Destroy(this.gameObject);
            }         
        }
    }
}
