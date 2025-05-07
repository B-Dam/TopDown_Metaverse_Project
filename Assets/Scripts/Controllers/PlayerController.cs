using UnityEngine;

// 지정한 컴포넌트들이 없으면 자동으로 추가해 줍니다
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    [Header("이동 속도")]
    public float moveSpeed = 5f;
    // 플레이어 이동 속도

    [Header("충돌 레이어")]
    public LayerMask collisionLayer;
    // 벽 타일맵이 설정된 레이어만 충돌 검사에 사용

    Rigidbody2D rb;          // 물리 이동을 담당하는 리지드바디
    Animator anim;           // 달리기/대기 애니메이터
    SpriteRenderer sr;       // 스프라이트 뒤집기용 렌더러
    BoxCollider2D bc;        // 충돌 검사용 박스 콜라이더

    Vector2 moveInput;       // 입력값 저장용 벡터

    void Awake()
    {
        // 각 컴포넌트를 캐싱하여 성능 최적화
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();

        // 탑다운 게임이므로 중력 비활성, 회전 고정
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    void Update()
    {
        // 1) 입력 수집 (사방향)
        moveInput.x = Input.GetAxisRaw("Horizontal"); // -1,0,+1
        moveInput.y = Input.GetAxisRaw("Vertical");   // -1,0,+1
        moveInput.Normalize();  // 대각 이동 시 속도 보정

        // 2) 애니메이션 전환: 입력이 있을 때만 달리기 모션
        bool running = moveInput.sqrMagnitude > 0.001f;
        anim.SetBool("isRunning", running);

        // 3) 스프라이트 좌우 반전: 왼쪽 이동 시 뒤집기
        if (moveInput.x < 0) sr.flipX = true;
        else if (moveInput.x > 0) sr.flipX = false;
    }

    void FixedUpdate()
    {
        // 현재 위치와 이동 목표치 계산
        Vector2 pos = rb.position;
        Vector2 delta = moveInput * moveSpeed * Time.fixedDeltaTime;
        Vector2 target = pos;

        // 박스캐스트에 사용할 콜라이더 크기와 오프셋
        Vector2 size = bc.size;
        Vector2 offset = bc.offset;

        // X축 이동에 대한 충돌 검사
        if (Mathf.Abs(delta.x) > 0.0001f)
        {
            Vector2 dir = new Vector2(Mathf.Sign(delta.x), 0);
            float dist = Mathf.Abs(delta.x);

            // 이동 전, 충돌 레이어에 박스캐스트를 쏴서 막히는지 확인
            var hitX = Physics2D.BoxCast(
                pos + offset,   // 콜라이더 중심 위치
                size,           // 콜라이더 크기
                0f,             // 회전 각도
                dir,            // 이동 방향
                dist,           // 이동 거리
                collisionLayer  // 검사할 레이어 마스크
            );

            // 충돌이 없을 때만 목표 x 좌표에 더함
            if (hitX.collider == null)
                target.x += delta.x;
        }

        // Y축 이동에 대한 충돌 검사
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

        // 최종 위치로 물리 이동 (충돌 계산 포함)
        rb.MovePosition(target);
    }
}
