using UnityEngine;

public class UserTeleport : MonoBehaviour
{
    public Transform teleportTarget; // 移動先の丸（テレポート先）

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Playerタグを持つオブジェクトが触れたら
        {
            collision.transform.position = teleportTarget.position; // プレイヤーをテレポート先に移動
        }
    }
}
