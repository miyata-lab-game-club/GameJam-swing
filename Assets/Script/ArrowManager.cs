using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    public NewplayerManager newplayerManager;
    public NewplayerManager.PlayerDir playerDir;
    public float arrowDamage = 10f;
    [SerializeField] private GameObject breakEffect;

    // Start is called before the first frame update
    void onEnable()
    {
        playerDir = newplayerManager.playerDir;

        if(playerDir == NewplayerManager.PlayerDir.left){
            //スケールを変えて矢を左向きにする
            Vector2 arrowScale = this.transform.localScale;
            arrowScale.x *= -1;
            this.transform.localScale = arrowScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //矢を進行方向へ回転
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        Vector2 velocity = rb.velocity;
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag != "Player"){
        GameObject eff = Instantiate(breakEffect,collider.transform.position,Quaternion.identity);
        Destroy(this.gameObject);
        }
    }
}
