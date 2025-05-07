using UnityEngine;

// ������ ������Ʈ���� ������ �ڵ����� �߰��� �ݴϴ�
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    [Header("�̵� �ӵ�")]
    public float moveSpeed = 5f;
    // �÷��̾� �̵� �ӵ�

    [Header("�浹 ���̾�")]
    public LayerMask collisionLayer;
    // �� Ÿ�ϸ��� ������ ���̾ �浹 �˻翡 ���

    Rigidbody2D rb;          // ���� �̵��� ����ϴ� ������ٵ�
    Animator anim;           // �޸���/��� �ִϸ�����
    SpriteRenderer sr;       // ��������Ʈ ������� ������
    BoxCollider2D bc;        // �浹 �˻�� �ڽ� �ݶ��̴�

    Vector2 moveInput;       // �Է°� ����� ����

    void Awake()
    {
        // �� ������Ʈ�� ĳ���Ͽ� ���� ����ȭ
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();

        // ž�ٿ� �����̹Ƿ� �߷� ��Ȱ��, ȸ�� ����
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    void Update()
    {
        // 1) �Է� ���� (�����)
        moveInput.x = Input.GetAxisRaw("Horizontal"); // -1,0,+1
        moveInput.y = Input.GetAxisRaw("Vertical");   // -1,0,+1
        moveInput.Normalize();  // �밢 �̵� �� �ӵ� ����

        // 2) �ִϸ��̼� ��ȯ: �Է��� ���� ���� �޸��� ���
        bool running = moveInput.sqrMagnitude > 0.001f;
        anim.SetBool("isRunning", running);

        // 3) ��������Ʈ �¿� ����: ���� �̵� �� ������
        if (moveInput.x < 0) sr.flipX = true;
        else if (moveInput.x > 0) sr.flipX = false;
    }

    void FixedUpdate()
    {
        // ���� ��ġ�� �̵� ��ǥġ ���
        Vector2 pos = rb.position;
        Vector2 delta = moveInput * moveSpeed * Time.fixedDeltaTime;
        Vector2 target = pos;

        // �ڽ�ĳ��Ʈ�� ����� �ݶ��̴� ũ��� ������
        Vector2 size = bc.size;
        Vector2 offset = bc.offset;

        // X�� �̵��� ���� �浹 �˻�
        if (Mathf.Abs(delta.x) > 0.0001f)
        {
            Vector2 dir = new Vector2(Mathf.Sign(delta.x), 0);
            float dist = Mathf.Abs(delta.x);

            // �̵� ��, �浹 ���̾ �ڽ�ĳ��Ʈ�� ���� �������� Ȯ��
            var hitX = Physics2D.BoxCast(
                pos + offset,   // �ݶ��̴� �߽� ��ġ
                size,           // �ݶ��̴� ũ��
                0f,             // ȸ�� ����
                dir,            // �̵� ����
                dist,           // �̵� �Ÿ�
                collisionLayer  // �˻��� ���̾� ����ũ
            );

            // �浹�� ���� ���� ��ǥ x ��ǥ�� ����
            if (hitX.collider == null)
                target.x += delta.x;
        }

        // Y�� �̵��� ���� �浹 �˻�
        if (Mathf.Abs(delta.y) > 0.0001f)
        {
            Vector2 dir = new Vector2(0, Mathf.Sign(delta.y));
            float dist = Mathf.Abs(delta.y);

            var hitY = Physics2D.BoxCast(
                pos + offset,
                size,
                0f,
                dir,
                dist,
                collisionLayer
            );

            if (hitY.collider == null)
                target.y += delta.y;
        }

        // ���� ��ġ�� ���� �̵� (�浹 ��� ����)
        rb.MovePosition(target);
    }
}
