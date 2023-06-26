using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOManager : MonoBehaviour
{
    public Transform[] targetPositions; // UFOが移動する目標位置の配列
    public float moveInterval = 2f; // 移動間隔のインターバル（デフォルトは2秒）
    public float UFOSpeed = 5f;

    [SerializeField] private float maxHP = 300f; //maxHP
    [SerializeField] private float nowHP; //nowHP
    [SerializeField] private GameObject energyShot;
    [SerializeField] private float shotSpeed = 15f;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject gameClearPanel;
    [SerializeField] private float shotProbability = 0.6f;
    [SerializeField] private AudioClip deathSound;

    private Rigidbody2D rb;
    private int currentIndex = 1; // 現在の目標位置のインデックス
    private bool isHpUnder50per = false;
    private bool isDeath = false;
    private float nextExplosionTime;

    private void Start()
    {
        nowHP = maxHP;
        transform.position = targetPositions[currentIndex].position;// 初期位置にUFOを配置
        rb = GetComponent<Rigidbody2D>();
        nextExplosionTime = Time.time;
        StartCoroutine(MoveUFO());// 移動コルーチンを開始
    }

    private void Update()
    {
        if (nowHP <= maxHP / 2)
        {
            isHpUnder50per = true;
        }

        if (nowHP <= 0 && isDeath == false) //死んだとき
        {
            isDeath = true;
            DestroyEvent();
        }
        else if (nowHP <= 0 && isDeath == true) //死んでデストロイされるまで
        {
            if (Time.time >= nextExplosionTime) //一転時間ごとにデスエフェクトを生成
            {
                DestroyEffect();
                AudioSource.PlayClipAtPoint(deathSound,new Vector3(0,0,-5));
                nextExplosionTime = Time.time + 0.1f;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Arrow")) //矢に当たったらダメージ
        {
            ArrowManager arrowManager = collision.GetComponent<ArrowManager>();
            float arrowDamage = arrowManager.arrowDamage;
            nowHP -= arrowDamage;
        }
    }

    private void ShotEnergy()
    {
        if (player == null)
        {
            return;
        }

        if (isHpUnder50per == false)
        {
            GameObject shot = Instantiate(energyShot, this.transform.position, Quaternion.identity);
            Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
            Vector2 playerPos = player.transform.position;
            Vector2 myPos = this.transform.position;

            Vector2 force = (playerPos - myPos).normalized * shotSpeed;
            rb.AddForce(force, ForceMode2D.Impulse);
        }else{
            shotProbability = 0.9f;
            UFOSpeed += 2;

            GameObject[] shots = new GameObject[5];
            Rigidbody2D[] rbs = new Rigidbody2D[5];

            for(int i = 0; i < 5; i++){
                shots[i] = Instantiate(energyShot, this.transform.position, Quaternion.identity);
                rbs[i] = shots[i].GetComponent<Rigidbody2D>();
            }

            Vector2 force1 = new Vector2(-1,-1).normalized * shotSpeed;
            rbs[0].AddForce(force1,ForceMode2D.Impulse);
            Vector2 force2 = new Vector2(0,-1).normalized * shotSpeed;
            rbs[1].AddForce(force2,ForceMode2D.Impulse);
            Vector2 force3 = new Vector2(1,-1).normalized * shotSpeed;
            rbs[2].AddForce(force3,ForceMode2D.Impulse);
            Vector2 force4 = new Vector2(3,-1).normalized * shotSpeed;
            rbs[3].AddForce(force4,ForceMode2D.Impulse);
            Vector2 force5 = new Vector2(-3,-1).normalized * shotSpeed;
            rbs[4].AddForce(force5,ForceMode2D.Impulse);
        }

    }


    private void DestroyEvent()//死イベント
    {
        rb.gravityScale = 1;
        Destroy(this.gameObject, 2);//2秒後にデストロイ

    }

    private void DestroyEffect() //自分の周りにランダムでエフェクト生成
    {

        //デスエフェクトを表示

        GameObject deathEff = Instantiate(deathEffect, this.transform.position, Quaternion.identity);

        Vector2 effPos = deathEff.transform.position;
        effPos += new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        deathEff.transform.position = effPos;

    }

    void OnDestroy()
    {
        if (gameClearPanel != null)
        {
            gameClearPanel.SetActive(true);
        }
    }

    private Vector2 GetRandomTargetPosition()
    {
        int randomIndex;

        if (isHpUnder50per == false)
        {
            do
            {
                randomIndex = Random.Range(0, targetPositions.Length);
            } while (randomIndex == currentIndex);
        }
        else
        {
            do
            {
                randomIndex = Random.Range(0, targetPositions.Length);
            } while (randomIndex == currentIndex || randomIndex == 3 || randomIndex == 4 || randomIndex == 5);
        }

        currentIndex = randomIndex;
        Debug.Log(currentIndex);
        return targetPositions[randomIndex].position;
    }

    private IEnumerator MoveUFO()
    {
        while (true)
        {
            if (isDeath)
            {
                yield break;
            }

            // 目標位置にUFOを移動
            yield return StartCoroutine(MoveToTargetPosition(GetRandomTargetPosition()));

            if (Random.value < shotProbability)  // 確立で処理を実行
            {
                ShotEnergy();
            }

            // インターバル待ち
            yield return new WaitForSeconds(moveInterval);

        }
    }


    private IEnumerator MoveToTargetPosition(Vector2 targetPosition)
    {
        // 目標位置までの移動時間を計算
        float distance = Vector2.Distance(transform.position, targetPosition);
        float moveTime = distance / UFOSpeed;

        // 目標位置に移動
        float elapsedTime = 0f;
        Vector2 startingPos = transform.position;

        while (elapsedTime < moveTime)
        {
            if (isDeath)
            {
                yield break;
            }

            transform.position = Vector2.Lerp(startingPos, targetPosition, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 移動が終わったら目標位置に正確に配置
        transform.position = targetPosition;
    }
}
