using UnityEngine;

// �ʿ��� ������Ʈ�� �ڵ����� �߰�
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class DinoPlayerController : MonoBehaviour
{
    [Header("�̵� & ���� ����")]
    public float jumpForce = 10f;    // ���� �� ������ ��
    public float runSpeed = 5f;      // �޸��� �ӵ� (�ʿ� �� ���)

    [Header("Ground Check")]
    public Transform groundCheck;        // �ٴ� üũ ��ġ
    public float groundCheckRadius = 0.1f; // �ٴ� üũ �ݰ�
    public LayerMask groundLayer;        // �ٴ����� �ν��� ���̾�

    private Rigidbody2D rb;           // ���� ���� Rigidbody2D ������Ʈ
    private Animator anim;            // �ִϸ����� ������Ʈ

    private bool isGrounded = false;  // ���� �ٴڿ� ��� �ִ���
    private bool isAlive = true;      // ��� �ִ� ��������

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();        // Rigidbody2D ��������
        anim = GetComponent<Animator>();         // Animator ��������

        rb.gravityScale = 3f;                    // �߷� ����
        rb.freezeRotation = true;                // ȸ�� ����
    }

    void Update()
    {
        if (!isAlive)
            return;                              // ���� ���¸� �Է� ����

        // �ٴ� �浹 üũ 
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,                // �˻� ��ġ
            groundCheckRadius,                   // �˻� �ݰ�
            groundLayer                          // �˻��� ���̾�
        );
        anim.SetBool("isJumping", !isGrounded);  // �ٴڿ� ������ ���� �ִϸ��̼�

        // ���� �Է� ó��
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // ���� �ӵ� �����ϸ鼭 y������ ������ ����
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // ��ֹ��� �ε����� ���� ����
        if (col.collider.CompareTag("Obstacle"))
        {
            HandleGameOver();
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        // Ground �±׿��� ����� ���� ���·� ��ȯ
        if (col.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // ���� ���� ó��
    private void HandleGameOver()
    {
        // 1) ���� ���� ��������
        int finalScore = FindObjectOfType<ScoreTracker>().GetScore();

        // 2) MiniGameManager���� ��� ó�� ��û
        MiniGameManager.Instance.EndMiniGame(finalScore);

        // 3) �÷��̾� ���� ����
        isAlive = false;
        rb.velocity = Vector2.zero;

        // 4) ���� ���� ���߱�
        foreach (var looper in FindObjectsOfType<GroundLooper>())
            looper.enabled = false;

        // 5) ��ֹ� ������ ����
        foreach (var spawner in FindObjectsOfType<ObstacleSpawner>())
            spawner.enabled = false;

        // 6) �̹� ������ ��ֹ� �̵� ����
        foreach (var move in FindObjectsOfType<ObstacleMovement>())
            move.enabled = false;

        // 7) ���� ���� Ʈ��Ŀ ��Ȱ��ȭ
        var tracker = FindObjectOfType<ScoreTracker>();
        if (tracker != null)
            tracker.enabled = false;
    }
}
