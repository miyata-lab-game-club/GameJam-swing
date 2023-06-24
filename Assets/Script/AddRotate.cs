using UnityEngine;

public class AddRotate : MonoBehaviour
{
    public GameObject rotateObject; // 回転させるオブジェクトをインスペクタから指定します
    public float rotationAmount = 90f; // オブジェクトが回転する角度をインスペクタから指定します

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow")) // タグを"Arrow"に設定した矢が当たった時
        {

            // rotateObjectが指定されていたら、それをrotationAmountだけ回転させます
            if (rotateObject != null)
            {
                rotateObject.transform.Rotate(0, 0, rotationAmount);
            }

            Destroy(collision.gameObject); // 矢を消します
            Destroy(this.gameObject); // 当たったオブジェクトも消します
        }
    }
}
