using UnityEngine;

public class RopeCut : MonoBehaviour
{
    public HingeJoint2D hinge; // ヒンジジョイント

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow")) // 矢が当たったら
        {
            Destroy(hinge); // ヒンジジョイントを削除
            Destroy(collision.gameObject); // 矢を消す

            Destroy(this.gameObject); // ロープ（このスクリプトがアタッチされているオブジェクト）を消す
        }
    }
}
