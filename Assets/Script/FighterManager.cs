using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FighterManager : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isDeath = false;
    private float nextExplosionTime;

    public float fighterSpeed = 3.0f;
    public float turnX = 9.5f;
    public GameObject firePoint;
    public GameObject missile;
    public float missileSpeed = 15f;
    public float missileInterval = 1f; //ミサイルのインターバル

    [SerializeField] private float maxHP = 300f; //maxHP
    [SerializeField] private float nowHP; //nowHP
    [SerializeField] private GameObject granede; //グレネードオブジェクト
    [SerializeField] private float basicGranedeFallProbability = 0.5f; //グレネードを落とす確率
    [SerializeField] private GameObject deathEffect; //デスエフェクト
    [SerializeField] private GameObject gameClearPanel; //ゲームクリアパネル
    [SerializeField] private AudioClip deathSound;

    private enum MoveDir //向いている方向enum
    {
        left,
        right,
    }

    // Start is called before the first frame update
    private void Start()
    {
        nowHP = maxHP;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("FireMissilesRepeatedly"); //一定時間ごとにミサイルを発射
        nextExplosionTime = Time.time;
    }

    private void FixedUpdate()
    {
        Vector2 fighterPos = transform.position;
        Vector2 fighterScale = transform.localScale;

        if (isDeath != true)//生きていれば
        {
            NormalAction(fighterPos, fighterScale); //通常アクションを行う
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (nowHP <= 0 && isDeath == false) //死んだとき
        {
            isDeath = true;
            StartCoroutine(DestroyEvent());
        }
        else if (nowHP <= 0 && isDeath == true) //死んでデストロイされるまで
        {
            if (Time.time >= nextExplosionTime) //一転時間ごとにデスエフェクトを生成
            {
                DestroyEffect();
                AudioSource.PlayClipAtPoint(deathSound, new Vector3(0, 0, -5));
                nextExplosionTime = Time.time + 0.1f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (nowHP >= (maxHP / 2))//体力が半分以上なら
        {
            if (collision.gameObject.CompareTag("FallPoint")) //ポイントに来たら確率で岩を降らせる
            {
                if (Random.value < basicGranedeFallProbability)  // 確立で処理を実行
                {
                    FallGranede(collision.gameObject);
                }
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("FallPoint")) //ポイントに来たら確率で岩を降らせる
            {
                if (Random.value < 1.0)  // 100%の確立で処理を実行
                {
                    FallGranede(collision.gameObject);
                }
            }
        }

        if (collision.gameObject.CompareTag("Arrow")) //矢に当たったらダメージ
        {
            ArrowManager arrowManager = collision.GetComponent<ArrowManager>();
            float arrowDamage = arrowManager.arrowDamage;
            nowHP -= arrowDamage;
        }
    }

    private void NormalAction(Vector2 fighterPos, Vector2 fighterScale)
    {
        if (fighterScale.x < 0) //左向きなら
        {
            rb.velocity = new Vector2(-1, 0) * fighterSpeed; //左向きに移動
            if (fighterPos.x <= -turnX)
            {
                FlipCharacter();
            }
        }
        else if (fighterScale.x > 0) //右向きなら
        {
            rb.velocity = new Vector2(1, 0) * fighterSpeed; //右向きに移動
            if (fighterPos.x >= turnX)
            {
                FlipCharacter();
            }
        }
    }

    private void FlipCharacter()
    {
        // キャラクターの向きを反転
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void FallGranede(GameObject gr)//グレネードを落下
    {
        GameObject gran = Instantiate(granede, gr.transform.position, Quaternion.identity);
    }

    private void fireMissile(GameObject firePoint, Vector2 fighterScale)//ミサイル発射
    {
        GameObject mis = Instantiate(missile, firePoint.transform.position, Quaternion.identity);

        Rigidbody2D rb = mis.GetComponent<Rigidbody2D>();

        MissileManager mm = mis.GetComponent<MissileManager>();
        mm.fighter = this.gameObject;

        if (fighterScale.x < 0)
        {
            Vector2 force = new Vector2(-5.5f, -4f).normalized * missileSpeed;
            rb.AddForce(force, ForceMode2D.Impulse);
        }
        else if (fighterScale.x > 0)
        {
            Vector2 force = new Vector2(5.5f, -4f).normalized * missileSpeed;
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }

    private IEnumerator DestroyEvent()//死イベント
    {
        //Destroy(this.gameObject, 2);//2秒後にデストロイ

        rb.gravityScale = 1;
        yield return new WaitForSeconds(2);
        this.GetComponent<SpriteRenderer>().enabled = false;

        if (gameClearPanel != null)
        {
            gameClearPanel.SetActive(true);
        }
        StartCoroutine(loadNextScene());
    }

    private void DestroyEffect() //自分の周りにランダムでエフェクト生成
    {
        //デスエフェクトを表示

        GameObject deathEff = Instantiate(deathEffect, this.transform.position, Quaternion.identity);

        Vector2 effPos = deathEff.transform.position;
        effPos += new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        deathEff.transform.position = effPos;
    }

    /*
    void OnDestroy()
    {
        if(gameClearPanel != null){
        gameClearPanel.SetActive(true);
        }
    }*/

    private IEnumerator FireMissilesRepeatedly()//ミサイルを一定時間ごとに発射するコルーチン
    {
        while (true)
        {
            if (isDeath)
            {
                yield break;
            }

            Vector2 fighterScale = transform.localScale;

            fireMissile(firePoint, fighterScale);  // ミサイルを発射

            yield return new WaitForSeconds(missileInterval);  // 数秒待機
        }
    }

    private IEnumerator loadNextScene()
    {
        yield return new WaitForSeconds(2);
        GameManager gameManager = GameManager.Instance;
        if (gameManager.gameMode == GameMode.Story)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(++currentSceneIndex);
        }
        else
        {
            SceneManager.LoadScene("SelectStage");
        }
    }
}