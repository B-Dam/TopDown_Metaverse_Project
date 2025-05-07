using UnityEngine;

// 필요한 컴포넌트를 자동으로 추가
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class DinoPlayerController : MonoBehaviour
{
    [Header("이동 & 점프 세팅")]
    public float jumpForce = 10f;    // 점프 시 적용할 힘
    public float runSpeed = 5f;      // 달리기 속도 (필요 시 사용)

    [Header("Ground Check")]
    public Transform groundCheck;        // 바닥 체크 위치
    public float groundCheckRadius = 0.1f; // 바닥 체크 반경
    public LayerMask groundLayer;        // 바닥으로 인식할 레이어

    private Rigidbody2D rb;           // 물리 계산용 Rigidbody2D 컴포넌트
    private Animator anim;            // 애니메이터 컴포넌트

    private bool isGrounded = false;  // 현재 바닥에 닿아 있는지
    private bool isAlive = true;      // 살아 있는 상태인지

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();        // Rigidbody2D 가져오기
        anim = GetComponent<Animator>();         // Animator 가져오기

        rb.gravityScale = 3f;                    // 중력 세팅
        rb.freezeRotation = true;                // 회전 방지
    }

    void Update()
    {
        if (!isAlive)
            return;                              // 죽은 상태면 입력 무시

        // 바닥 충돌 체크 
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,                // 검사 위치
            groundCheckRadius,                   // 검사 반경
            groundLayer                          // 검사할 레이어
        );
        anim.SetBool("isJumping", !isGrounded);  // 바닥에 없으면 점프 애니메이션

        // 점프 입력 처리
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // 현재 속도 유지하면서 y축으로 점프력 적용
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // 장애물과 부딪히면 게임 오버
        if (col.collider.CompareTag("Obstacle"))
        {
            HandleGameOver();
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        // Ground 태그에서 벗어나면 공중 상태로 전환
        if (col.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // 게임 오버 처리
    private void HandleGameOver()
    {
        // 1) 최종 점수 가져오기
        int finalScore = FindObjectOfType<ScoreTracker>().GetScore();

        // 2) MiniGameManager에게 결과 처리 요청
        MiniGameManager.Instance.EndMiniGame(finalScore);

        // 3) 플레이어 제어 중지
        isAlive = false;
        rb.velocity = Vector2.zero;

        // 4) 지면 루핑 멈추기
        foreach (var looper in FindObjectsOfType<GroundLooper>())
            looper.enabled = false;

        // 5) 장애물 스포너 중지
        foreach (var spawner in FindObjectsOfType<ObstacleSpawner>())
            spawner.enabled = false;

        // 6) 이미 생성된 장애물 이동 중지
        foreach (var move in FindObjectsOfType<ObstacleMovement>())
            move.enabled = false;

        // 7) 점수 증가 트래커 비활성화
        var tracker = FindObjectOfType<ScoreTracker>();
        if (tracker != null)
            tracker.enabled = false;
    }
}
