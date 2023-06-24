using UnityEngine;

public class ArrowTeleport : MonoBehaviour
{
    public Transform teleportExit; // テレポートの出口
    public GameObject arrowPrefab; // 矢のプレハブ

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow")) // Arrowタグを持つオブジェクトが触れたら
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // 新しい矢をテレポートの出口でスポーンし、同じ速度と方向で飛ばす
                GameObject newArrow = Instantiate(arrowPrefab, teleportExit.position, Quaternion.identity);
                Rigidbody2D newRb = newArrow.GetComponent<Rigidbody2D>();
                newRb.velocity = rb.velocity;

                // 元の矢を破壊
                Destroy(collision.gameObject);
            }
        }
    }
}
