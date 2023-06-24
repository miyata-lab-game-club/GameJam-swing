using UnityEngine;

public class MoveStage : MonoBehaviour
{
    public float speed = 2.0f; // 移動速度
    public float distance = 2.0f; // 移動距離

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // 初期位置を保存
    }

    void Update()
    {
        float movement = Mathf.PingPong(Time.time * speed, distance); // PingPong関数で上下に動く値を取得
        transform.position = startPosition + Vector3.up * movement; // 初期位置に上下の動きを追加
    }
}
