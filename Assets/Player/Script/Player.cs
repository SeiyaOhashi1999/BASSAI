using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject attackRange;
    public GameObject ono;
    Collider2D onoCollider;

    AudioSource audioSource;
    public AudioClip swingSE;

    float input_x; //キーボードのx座標入力の値
    bool right; //Trueなら右向き

    public float gravityScale; //Playerにかかる重力の大きさ

    public float moveSpeed; //プレイヤーの横移動のスピード

    public bool isGround; //地面に触れていたらTrue
    public float jumpPower; //ジャンプの強さ
    public float jumpStopPower; //ジャンプの減速する強さ

    public bool isJump; //ジャンプしたらTrue

    public bool isDamaged;//トゲに当たったらtrue

    #region 攻撃時間関係
    bool isAttack; //攻撃中ならTrue
    bool isJumpAttack; //ジャンプ攻撃中ならTrue
    float attackWaitTimeCount; //攻撃継続時間のカウント用
    public float attackWaitTime; //ボタンを押してから攻撃判定が出るまでの待機時間
    float attackTimeCount; //攻撃継続時間のカウント用
    public float attackTime; //攻撃継続時間
    float attackIntervalCount; //次の攻撃可能になるまでのカウント用
    public float attackIntervalTime; //次の攻撃可能になるまでの時間
    #endregion

    #region Attack State
    public enum AttackState
    {
        Noemal,
        AttackStart,
        IsAttack,
        AttackEnd
    }
    public AttackState attackState;
    #endregion

    float angleZ; //PlayerのZ回転軸の値
    float jumpTime = 2f;
    float jumpTimeCount;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Rigidbody2Dコンポーネント取得
        rb.gravityScale = gravityScale;

        audioSource = GetComponent<AudioSource>();

        onoCollider = ono.GetComponent<Collider2D>();

        attackState = AttackState.Noemal;

        ono.SetActive(false);
    }

    #region 毎フレーム行われる処理
    void Update()
    {
		// Ready状態では操作を受け付けない
		if (GameSystemManager.Instance != null && GameSystemManager.Instance.CurrentGameState == GameSystemManager.GameState.Ready)
			return;

		if (isDamaged == true)//ダメージを受けたら抜ける
        {
			setVelocityZero();
			return;
        }


        input_x = Input.GetAxis("Horizontal"); //キーに応じて-1〜1の範囲で取得
        if(input_x > 0) //右入力の場合
        {
            right = true;
            attackRange.transform.localScale = new Vector3(1, 1, 1);
        }
        else if(input_x < 0) //左入力の場合
        {
            right = false;
            attackRange.transform.localScale = new Vector3(-1, 1, 1);
        }


        //上矢印キーでジャンプ
        if (Input.GetKeyDown(KeyCode.UpArrow)) Jump();
        else if (Input.GetKeyUp(KeyCode.UpArrow)) JumpStop();

        if (Input.GetKeyDown(KeyCode.W)) Jump();
        else if (Input.GetKeyUp(KeyCode.W)) JumpStop();

        if (Input.GetMouseButtonDown(1)) Jump();
        else if (Input.GetMouseButtonUp(1)) JumpStop();

        //Jキーでアタック
        if (Input.GetKeyDown(KeyCode.J) && !isAttack) AttackStart();
        else if (Input.GetKeyDown(KeyCode.F) && !isAttack) AttackStart();
        else if (Input.GetMouseButtonDown(0) && !isAttack) AttackStart();


        #region 攻撃状態に応じた処理
        if (attackState == AttackState.AttackStart)
        {
            attackWaitTimeCount += Time.deltaTime;
            if (attackWaitTimeCount >= attackWaitTime)
            {
                attackState = AttackState.IsAttack;
                Attack();

            }
        }
        else if(attackState == AttackState.IsAttack)
        {
            attackTimeCount += Time.deltaTime;
            //attackCountが継続時間を超えたらAttackLastを呼ぶ
            if (attackTimeCount >= attackTime)
            {
                AttackLast();
            }
        }
        else if(attackState == AttackState.AttackEnd)
        {
            attackIntervalCount += Time.deltaTime;
            if(attackIntervalCount >= attackIntervalTime)
            {
                attackState = AttackState.Noemal;
                AttackEnd();
            }
            
        }
        #endregion

        //if (isJumpAttack) Rolling();
        //if (isGround) SetGround();

        if(isJump)
        {
            jumpTimeCount += Time.deltaTime;
            if(jumpTimeCount > jumpTime)
            {
                isJump = false;
                isGround = true;
                jumpTimeCount = 0;
            }
        }

        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angleZ);

    }
    #endregion

    void FixedUpdate()
    {
		if (isDamaged == true)//ダメージを受けたら抜ける
		{
			setVelocityZero();
			return;
		}

		float veloxity_x = moveSpeed * input_x;
        float velocity_y = rb.velocity.y;
        Vector2 velocity = new Vector2(veloxity_x, velocity_y);
        rb.velocity = velocity;
    }

    //ジャンプ処理
    void Jump()
    {
        if(isGround)
        {
            Vector2 jump = Vector2.up * jumpPower;
            rb.AddForce(jump, ForceMode2D.Impulse);
            isJump = true;
        }
    }

    //ジャンプ停止処理
    void JumpStop()
    {
        Debug.Log("JumpStop");
        Vector2 jump = -Vector2.up * jumpStopPower;
        rb.AddForce(jump, ForceMode2D.Impulse);
    }

    //攻撃オブジェクトのオン
    void AttackStart()
    {
        Debug.Log("AttackStart");
        if (!isGround) isJumpAttack = true;
        attackWaitTimeCount = 0;
        attackState = AttackState.AttackStart;
        isAttack = true;
        ono.SetActive(true);
        onoCollider.enabled = false;
    }

    //攻撃中
    void Attack()
    {
        Debug.Log("Attack");
        audioSource.PlayOneShot(swingSE);
        attackTimeCount = 0;
        onoCollider.enabled = true;
        int dir = (right) ? 1 : -1;
        rb.AddForce(Vector2.right * dir * 1, ForceMode2D.Impulse);
    }

    //ジャンプ中回転
    public void RollingAttack()
    {
        Debug.Log("RollingAttack");
        audioSource.PlayOneShot(swingSE);
        attackTimeCount = 0;
        onoCollider.enabled = true;
        int dir = (right) ? 1 : -1;
        rb.AddForce(Vector2.right * dir * 1, ForceMode2D.Impulse);
    }

    //攻撃終了時に呼ばれる
    void AttackLast()
    {
        Debug.Log("AttackLast");
        attackIntervalCount = 0;
        attackState = AttackState.AttackEnd;
        //isAttack = false;
        ono.SetActive(false);
    }

    //攻撃処理の最後に呼ばれる
    void AttackEnd()
    {
        Debug.Log("AttackEnd");
        attackState = AttackState.Noemal;
        isAttack = false;
    }

    //ミス時に呼ばれる
    public void Miss()
    {
        Debug.Log("Miss");
        this.gameObject.SetActive(false);
    }

	//速度を0にする
	public void setVelocityZero()
	{
		if(rb != null)
		{
			rb.velocity = Vector2.zero;
		}
	}

    //これを読んでいる間Z加算
    public void Rolling()
    {
        angleZ += Time.deltaTime * 500f;
    }

    //地面に着いた時
    public void SetGround()
    {
        isJumpAttack = false;
        angleZ = 0f;
    }

}
