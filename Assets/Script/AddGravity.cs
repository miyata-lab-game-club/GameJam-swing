using UnityEngine;

public class AddGravity : MonoBehaviour
{
    
    public GameObject objectToFall; // 重力が付与されて落ちるオブジェクトをインスペクタから指定します

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow")) // タグを"Arrow"に設定した矢が当たった時
        {
            Rigidbody2D rb = objectToFall.GetComponent<Rigidbody2D>(); 
            if (rb != null)
            {
                rb.gravityScale = 1; // 重力を付与します（事前に重力のスケールを0に設定しておく）
            }
            Destroy(collision.gameObject); // 矢を消します
            Destroy(this.gameObject); // 当たったオブジェクトも消します
        }
    }
}
