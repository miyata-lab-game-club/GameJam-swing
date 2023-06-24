using UnityEngine;

public class DestroyOnWater : MonoBehaviour
{
   public GameObject objectToDestroy; // これが衝突相手のオブジェクトになります

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == objectToDestroy) // オブジェクトが指定したobjectToDestroyと一致したら
        {
            Destroy(collision.gameObject); // 衝突したオブジェクトを消します
            Destroy(this.gameObject); // このオブジェクトも消します
        }
    }
}