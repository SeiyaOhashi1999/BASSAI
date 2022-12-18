using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MikiManager : MonoBehaviour
{
    public Miki mikiPrefab;
    public bool spriteFlip; //Trueで反転
    public List<Sprite> sprites = new List<Sprite>();
    List<Miki> mikis = new List<Miki>();
    
    public bool playOn; //Trueで幹の生成ストップ
    float createTimeCount;
    public float createTime; //この時間で幹生成

    bool stop; //Trueで生成一時停止,時間経過で自動でTrueになる
    float stopTimeCount;
    public float stopTime; //停止解除までの時間

    public int mikiCount;

    void Start()
    {
           
    }

    void Update()
    {
        if(playOn)
        {
            if (stop)
            {
                stopTimeCount += Time.deltaTime;
                if (stopTimeCount >= stopTime)
                {
                    stop = false;
                }
            }
            else
            {
                createTimeCount += Time.deltaTime;
                if(createTimeCount >= createTime)
                {
                    createTimeCount = 0f;
                    AddMiki();
                }
            }
        }

    }

    public void AddMiki()
    {
        mikiCount++;
        Miki miki = Instantiate(this.mikiPrefab);
        miki.SetMikiManager(this);
        miki.alive = true;
        mikis.Add(miki);

        miki.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX|
            RigidbodyConstraints2D.FreezeRotation;

        miki.transform.SetParent(this.transform);
        float posX = 0;
        float posY = mikiCount - 1;
        miki.transform.localPosition = new Vector2(posX, posY);

        UpdateSprite();
    }

    //幹が消された時に呼ばれる
    public void DecreaseMiki()
    {
        mikiCount--;
        stop = true;
        stopTimeCount = 0f;
    }

    void UpdateSprite()
    {
        List<Miki> mikis = new List<Miki>();
        foreach(Transform child in this.transform)
        {
            mikis.Add(child.GetComponent<Miki>());
        }

        if(mikis.Count == 0)
        {

        }
        else if(mikis.Count == 1)
        {
            mikis[0].GetComponent<SpriteRenderer>().sprite = sprites[0];
            mikis[0].GetComponent<SpriteRenderer>().flipX = spriteFlip;
        }
        else
        {
            for(int i=0;i<mikis.Count;i++)
            {
                SpriteRenderer mikiSprite = mikis[i].GetComponent<SpriteRenderer>();
                if (i==mikis.Count-1)
                {
                    mikiSprite.sprite = sprites[2];
                    mikiSprite.flipX = spriteFlip;
                }
                else
                {
                    mikiSprite.sprite = sprites[1];
                    mikiSprite.flipX = spriteFlip;
                }
            }
        }


    }
}
