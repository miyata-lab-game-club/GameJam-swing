using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleManager : MonoBehaviour
{
    public float eagleSpeed = 2f;       // 移動速度
    public float maxY = 3.82f;    // 上方向の最大Y座標
    public float maxX = 10f;       // 下方向の最小Y座標
    public bool isDeath = false;    //死んでいるか
    private bool movingRight = true;   // 右方向に移動中かどうか
    private bool canMove = true;    //動けるかどうか
    [SerializeField] private float myHP = 300f; //HP
    [SerializeField] private GameObject Rock; //落とす岩
    [SerializeField] private GameObject deathEffect; //死のエフェクト
    [SerializeField] private Animator eagleAnimator;
    [SerializeField] private GameObject gameClearPanel;

    private Rigidbody2D rb;
    private enum MoveDir //向いている方向enum
    {
        left,
        right,
    }
    private MoveDir moveDir = MoveDir.left; //今向いている方向

    void Start()
    {
        transform.position = new Vector2(10, maxY);
        rb = GetComponent<Rigidbody2D>();
        eagleAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Vector2 eaglePos = this.transform.position;
        Vector2 eagleScale = this.transform.localScale;

        if (canMove)
        {
            if (eagleScale.x > 0) //左向きなら
            {
                moveDir = MoveDir.left;
                rb.velocity = new Vector2(-1, 0) * eagleSpeed;
                if (eaglePos.x <= -maxX)
                {
                    FlipCharacter();
                }
            }
            else if (eagleScale.x < 0)
            {
                moveDir = MoveDir.right;
                rb.velocity = new Vector2(1, 0) * eagleSpeed;
                if (eaglePos.x >= maxX)
                {
                    FlipCharacter();
                }
            }
        }

    }

    private void Update()
    {
        if (myHP <= 0 && isDeath == false)
        {
            DestroyEvent();
            isDeath = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FallPoint")) //ポイントに来たら岩を降らせる
        {
            FallLocks(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Arrow")) //矢に当たったらダメージ
        {
            ArrowManager arrowManager = collision.GetComponent<ArrowManager>();
            float arrowDamage = arrowManager.arrowDamage;
            Debug.Log("aa" + arrowDamage);
            myHP -= arrowDamage;
        }
    }


    private void FlipCharacter()
    {
        // キャラクターの向きを反転
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void FallLocks(GameObject rk)
    {
        //岩を生成
        GameObject rock = Instantiate(Rock, rk.transform.position, Quaternion.identity);
    }

    private void DestroyEvent()
    {
        canMove = false;
        GameObject[] deathEffects = new GameObject[4];

        Destroy(this.gameObject,2);

        eagleAnimator.SetBool("isDeath",true);

        //デスエフェクトを表示
        for( int i = 0 ; i < 4; i++){
            GameObject deathEff  = Instantiate(deathEffect,this.transform.position,Quaternion.identity);
            deathEffects[i]  = deathEff;
            Vector2 effPos = deathEffects[i].transform.position;
            effPos += new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
            deathEffects[i].transform.position = effPos;
        }

        rb.gravityScale = 1;
    }

    void OnDestroy(){
        gameClearPanel.SetActive(true);
    }
}
