using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShotManager : MonoBehaviour
{

    public GameObject explosionEffect;

    // Start is called before the first frame update
    void onEnable(){
        
    }

    // Update is called once per frame
    void Update()
    {
        //ミサイルを進行方向へ回転
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        Vector2 velocity = rb.velocity;
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg ;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    void OnTriggerEnter2D(Collider2D collider){
        GameObject eff = Instantiate(explosionEffect,transform.position,Quaternion.identity);
        Destroy(this.gameObject);
    }
}
