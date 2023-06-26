using UnityEngine;

public class FreezeHeight : MonoBehaviour
{
    public float freezeHeight; // ここで設定した高さ以下になったらフリーズする
    private Rigidbody2D rb; // Rigidbody2D コンポーネント

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D コンポーネントを取得
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= freezeHeight) // 現在の高さが freezeHeight 以下になったら
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll; // すべての動きをフリーズする
        }
    }
}
