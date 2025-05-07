using UnityEngine;

// Camera 컴포넌트가 없으면 자동으로 추가해 줍니다
[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [Header("추적 대상")]
    public Transform target;       // 카메라가 따라다닐 대상(플레이어)

    [Header("부드러운 추적")]
    public float smoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;
    // SmoothDamp 내부에서 사용하는 속도 참조 변수

    [Header("카메라 경계 (맵 월드 좌표)")]
    public Vector2 minBounds;   // 맵의 왼쪽 하단 좌표 (예: (0, 0))
    public Vector2 maxBounds;   // 맵의 오른쪽 상단 좌표 (예: (64, 36))

    float halfHeight;  // 카메라 절반 높이
    float halfWidth;   // 카메라 절반 너비

    void Awake()
    {
        // 카메라 컴포넌트 가져오기
        var cam = GetComponent<Camera>();

        // orthographicSize는 절반 높이에 해당
        halfHeight = cam.orthographicSize;
        // 화면 비율에 맞춰 절반 너비 계산
        halfWidth = halfHeight * cam.aspect;
    }

    void LateUpdate()
    {
        // 타겟이 지정되지 않았으면 실행하지 않음
        if (target == null) return;

        // 1) 플레이어 위치에서 카메라가 바라볼 목표 좌표 계산 (z 축은 카메라 고정)
        Vector3 targetPos = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z
        );

        // 2) 지도 경계 안으로 좌표를 Clamp
        float clampedX = Mathf.Clamp(
            targetPos.x,
            minBounds.x + halfWidth,   // 왼쪽 경계 + 카메라 절반 너비
            maxBounds.x - halfWidth    // 오른쪽 경계 - 카메라 절반 너비
        );
        float clampedY = Mathf.Clamp(
            targetPos.y,
            minBounds.y + halfHeight,  // 아래 경계 + 카메라 절반 높이
            maxBounds.y - halfHeight   // 위 경계 - 카메라 절반 높이
        );
        Vector3 clampedPos = new Vector3(clampedX, clampedY, targetPos.z);

        // 3) 현재 위치에서 목표 위치로 부드럽게 이동
        transform.position = Vector3.SmoothDamp(
            transform.position,  // 현재 위치
            clampedPos,          // 목표 위치
            ref velocity,        // 속도 참조
            smoothTime           // 부드러운 추적에 걸리는 시간
        );
    }
}
