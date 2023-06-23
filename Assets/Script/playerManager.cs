using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isJump;
    public float playerSpeed = 200;
    public GameObject gameClearPanel;
    public GameObject gameOverPanel;
    public GameObject arrowPrefab;  // 矢のPrefab
    public float arrowSpeed = 10f;  // 矢の速度

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {            
            isJump = true;
            rb.AddForce(new Vector2(0, 7), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            ShootArrow();
        }
        Debug.Log("isJump: " + isJump);
    }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        rb.velocity += new Vector2(x, 0) * playerSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")){
            isJump = false;
        }
        if (collision.gameObject.CompareTag("Goal")){
            gameClearPanel.SetActive(true);
        }
        if (collision.gameObject.CompareTag("Enemy")){
            gameOverPanel.SetActive(true);
            Destroy(this.gameObject);
        }
    }

void ShootArrow()
{
    // 矢のインスタンスを生成
    GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
    // Rigidbodyを取得し、前方に力を加える
    Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();

    // プレイヤーの右方向と上方向の合成方向に力を加える
    Vector2 force = (transform.up + transform.right).normalized * arrowSpeed;
    rb.velocity = force;
}
}