using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3f; // 敵の移動スピード
    public bool isMoving = true; // 敵が移動するかどうか

    private bool isMovingRight = true; // 初期の移動方向

    void Update()
    {
        if (isMoving)
        {
            if (isMovingRight)
            {
                transform.Translate(2 * Time.deltaTime * speed, 0, 0); // 右へ移動
            }
            else
            {
                transform.Translate(-2 * Time.deltaTime * speed, 0, 0); // 左へ移動
            }
        }
    }

    // 敵が何かに衝突したときの処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow")) // 矢に衝突したら
        {
            Destroy(this.gameObject); // 敵を消去
        }
        else if (collision.gameObject.CompareTag("wall")) // 壁に衝突したら
        {
            isMovingRight = !isMovingRight; // 移動方向を反転させる
        }
    }
}
