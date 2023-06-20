using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerTr; // プレイヤーのTransformをInspectorから入れる

    private void Update() {
        // カメラをプレイヤーの場所へ
        transform.position = new Vector3(playerTr.position.x+3, playerTr.position.y+3, -50f);
    }
}