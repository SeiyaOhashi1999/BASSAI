using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miki : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxCollider2D;
    MikiManager mikiManager;

    public AudioClip sound1;

    public float moveSpeed;
    public float rotatePower;
    float playerPosX;

    public bool alive; //Trueで生存
    float lifeTimeCount; //生存時間カウント
    public float lifeTime; //ヒットした後の生存時間

    public void SetMikiManager(MikiManager mikiManager)
    {
        this.mikiManager = mikiManager;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        if(!alive)
        {
            lifeTimeCount += Time.deltaTime;
            float myPosX = transform.position.x;
            int dir = (playerPosX < myPosX) ? -1 : 1;
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, lifeTimeCount*500*dir);
            if (lifeTimeCount >= lifeTime)
            {
                Remove();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ono")
        {
            playerPosX = collision.transform.root.position.x;

            Hit();
        }
    }

    void Hit()
    {
        alive = false;
        transform.SetParent(null);
        mikiManager.DecreaseMiki();
        Move();
        SoundManager.Instance.playSE(sound1, 0.2f);
        GameSystemManager.Instance.addScore(10);
    }


    void Move()
    {
        boxCollider2D.enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.None;

        float myPosX = transform.position.x;
        int dir = (playerPosX < myPosX) ? 1 : -1;

        //画面外に飛ばす
        rb.velocity = Vector2.right * moveSpeed * dir;
    }

    void Remove()
    {
        Destroy(this.gameObject);
    }

}
