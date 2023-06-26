using UnityEngine;

public class RopeCut2 : MonoBehaviour
{
    public HingeJoint2D hinge; // ヒンジジョイント
    //private Rigidbody2D rb; // Rigidbody
    private Collider2D col; // Collider

    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>(); // Rigidbodyを取得
        col = GetComponent<Collider2D>(); // Colliderを取得
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow")) // 矢が当たったら
        {
            Destroy(hinge); // ヒンジジョイントを削除
            Destroy(collision.gameObject); // 矢を消す

            // RigidbodyとColliderを無効にする
            //rb.enabled = false;
            col.enabled = false;
        }
    }
}
