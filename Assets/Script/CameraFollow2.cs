using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow2 : MonoBehaviour
{
    public GameObject playerObj; // プレイヤーオブジェクトを公開します
    Transform playerTransform;

    void Start()
    {
        // playerObj = GameObject.FindGameObjectWithTag("Player"); // タグでの検索は不要になります
        playerTransform = playerObj.transform;
    }

    void LateUpdate()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        //横方向だけ追従
        transform.position = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z);
    }
}
