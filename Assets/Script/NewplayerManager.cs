using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewplayerManager : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isJump;
    private bool canMove = false;
    public float playerSpeed = 200;
    public GameObject gameClearPanel;
    public GameObject gameOverPanel;
    public GameObject arrowPrefab;  // 矢のPrefab
    public float arrowSpeed = 10f;  // 矢の速度

    // 追加：鍵を持っているかどうか
    public bool hasKey = false;

    public enum PlayerDir
    {
        left,
        right,
    }

    public PlayerDir playerDir;

    [SerializeField] private Animator archerAnimator;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        archerAnimator = GetComponent<Animator>();
        playerDir = PlayerDir.right;
    }

    private void Awake()
    {
        canMove = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            isJump = true;
            archerAnimator.SetBool("isJump", true);
            rb.AddForce(new Vector2(0, 7), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ShootArrow();
        }
        //Debug.Log("isJump: " + isJump);

        //追加：ユーザが落ちたらゲームオーバーになる
        if (this.transform.position.y <= -10)
        {
            gameOverPanel.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        if (canMove == false)
        {
            return;
        }

        float x = Input.GetAxisRaw("Horizontal");

        Vector2 playerScale = this.gameObject.transform.localScale;

        if ((playerScale.x > 0 && x < 0))
        {
            playerDir = PlayerDir.left;
            playerScale.x *= -1;
            this.gameObject.transform.localScale = playerScale;
        }
        else if (playerScale.x < 0 && x > 0)
        {
            playerDir = PlayerDir.right;
            playerScale.x *= -1;
            this.gameObject.transform.localScale = playerScale;
        }

        if (x != 0)
        {
            archerAnimator.SetBool("isMove", true);
            rb.velocity += new Vector2(x, 0) * playerSpeed * Time.deltaTime;
        }
        else
        {
            archerAnimator.SetBool("isMove", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
            archerAnimator.SetBool("isJump", false);
        }
        if (collision.gameObject.CompareTag("Goal"))
        {
            Debug.Log("Goal!");
            gameClearPanel.SetActive(true);
            StartCoroutine(loadNextScene());
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("敵に当たった");
            gameOverPanel.SetActive(true);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("EnemyAttack"))
        {
            gameOverPanel.SetActive(true);
            Destroy(this.gameObject);
        }

        // 追加：鍵を拾ったとき、hasKeyをTrueにする
        if (collider.gameObject.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(collider.gameObject);
        }
        // 追加：敵と当たったら死ぬ
        if (collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("Fire"))
        {
            gameOverPanel.SetActive(true);
            Destroy(this.gameObject);
        }
    }

    private void ShootArrow()
    {
        // 矢のインスタンスを生成
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        // Rigidbodyを取得
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();

        if (playerDir == PlayerDir.left)
        {
            // プレイヤーの左方向と上方向のに力を加える
            Vector2 force = new Vector2(-4.5f, 3.0f).normalized * arrowSpeed;
            rb.AddForce(force, ForceMode2D.Impulse);
        }
        else if (playerDir == PlayerDir.right)
        {
            // プレイヤーの右方向と上方向に力を加える
            Vector2 force = new Vector2(4.5f, 3.0f).normalized * arrowSpeed;
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }

    private IEnumerator loadNextScene()
    {
        yield return new WaitForSeconds(2);
        GameManager gameManager = GameManager.Instance;
        Debug.Log(gameManager.gameMode);
        if (gameManager.gameMode == GameMode.Story)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(++currentSceneIndex);
        }
        else
        {
            SceneManager.LoadScene("SelectStage");
        }
    }
}