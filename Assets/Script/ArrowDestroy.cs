using UnityEngine;

public class ArrowDestroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 矢が他のオブジェクトに当たった場合
        if (collision.gameObject.CompareTag("Target"))
        {
            // 当たったオブジェクトを消す
            Destroy(collision.gameObject);
        }
        
        // 矢自体を消す
        Destroy(this.gameObject);
    }
}