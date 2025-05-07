using System.Collections;
using UnityEngine;

// Rigidbody2D ������Ʈ�� ������ �ڵ����� �߰�
[RequireComponent(typeof(Rigidbody2D))]
public class ObstacleMovement : MonoBehaviour
{
    [Header("�⺻ �ӵ� & ���ӵ�")]
    public float initialSpeed = 5f;               // ó�� ������ �� �ӵ�
    public float speedIncreasePerSecond = 0.1f;   // ���� �߰��� �ӵ�

    private Rigidbody2D rb;       // ���� �̵��� ����� Rigidbody2D
    private float currentSpeed;   // ���� �̵� �ӵ�

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;  // �ܺ� �� ���� ���� velocity ����
        rb.freezeRotation = true;                 // ȸ�� ����
    }

    void OnEnable()
    {
        currentSpeed = initialSpeed;              // Ȱ��ȭ �� �ӵ� �ʱ�ȭ
        StartCoroutine(Accelerate());             // ���� �ڷ�ƾ ����
        rb.velocity = Vector2.left * currentSpeed; // ù �����Ӻ��� �������� �̵�
    }

    // ���� �ӵ��� ������Ű�� �ڷ�ƾ
    private IEnumerator Accelerate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);     // 1�� ���
            currentSpeed += speedIncreasePerSecond;  // �ӵ� ����
            rb.velocity = Vector2.left * currentSpeed; // ������ �ӵ��� �̵� ���� ����
        }
    }
}
