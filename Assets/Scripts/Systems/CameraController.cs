using UnityEngine;

// Camera ������Ʈ�� ������ �ڵ����� �߰��� �ݴϴ�
[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [Header("���� ���")]
    public Transform target;       // ī�޶� ����ٴ� ���(�÷��̾�)

    [Header("�ε巯�� ����")]
    public float smoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;
    // SmoothDamp ���ο��� ����ϴ� �ӵ� ���� ����

    [Header("ī�޶� ��� (�� ���� ��ǥ)")]
    public Vector2 minBounds;   // ���� ���� �ϴ� ��ǥ (��: (0, 0))
    public Vector2 maxBounds;   // ���� ������ ��� ��ǥ (��: (64, 36))

    float halfHeight;  // ī�޶� ���� ����
    float halfWidth;   // ī�޶� ���� �ʺ�

    void Awake()
    {
        // ī�޶� ������Ʈ ��������
        var cam = GetComponent<Camera>();

        // orthographicSize�� ���� ���̿� �ش�
        halfHeight = cam.orthographicSize;
        // ȭ�� ������ ���� ���� �ʺ� ���
        halfWidth = halfHeight * cam.aspect;
    }

    void LateUpdate()
    {
        // Ÿ���� �������� �ʾ����� �������� ����
        if (target == null) return;

        // 1) �÷��̾� ��ġ���� ī�޶� �ٶ� ��ǥ ��ǥ ��� (z ���� ī�޶� ����)
        Vector3 targetPos = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z
        );

        // 2) ���� ��� ������ ��ǥ�� Clamp
        float clampedX = Mathf.Clamp(
            targetPos.x,
            minBounds.x + halfWidth,   // ���� ��� + ī�޶� ���� �ʺ�
            maxBounds.x - halfWidth    // ������ ��� - ī�޶� ���� �ʺ�
        );
        float clampedY = Mathf.Clamp(
            targetPos.y,
            minBounds.y + halfHeight,  // �Ʒ� ��� + ī�޶� ���� ����
            maxBounds.y - halfHeight   // �� ��� - ī�޶� ���� ����
        );
        Vector3 clampedPos = new Vector3(clampedX, clampedY, targetPos.z);

        // 3) ���� ��ġ���� ��ǥ ��ġ�� �ε巴�� �̵�
        transform.position = Vector3.SmoothDamp(
            transform.position,  // ���� ��ġ
            clampedPos,          // ��ǥ ��ġ
            ref velocity,        // �ӵ� ����
            smoothTime           // �ε巯�� ������ �ɸ��� �ð�
        );
    }
}
