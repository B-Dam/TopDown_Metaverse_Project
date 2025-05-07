using UnityEngine;

public class GroundLooper : MonoBehaviour
{
    [Header("�̵� �ӵ� �� ���� �� ����")]
    public float speed = 5f;      // ������ �������� �̵��ϴ� �ӵ�
    public float tileWidth;       // �� �� ������ �� ������ �Ǵ� Ÿ���� ���� ��

    private Vector3 startPos;     // ���� ���� ��ġ�� ����

    void Awake()
    {
        // ���� ��ġ�� ĳ��
        startPos = transform.position;

        // ���� SpriteRenderer�� �پ� ������, ��������Ʈ�� ���� ���� �ڵ����� �����ͼ� tileWidth�� ����
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            tileWidth = sr.bounds.size.x;
    }

    void Update()
    {
        // 1) �� ������ �������� ���� �ӵ��� �̵�
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // 2) ������ ����ŭ �̵������� ���� ��ġ(startPos)�� ���� �̵����� ���� ȿ�� ����
        if (transform.position.x <= startPos.x - tileWidth)
            transform.position = startPos;
    }
}
