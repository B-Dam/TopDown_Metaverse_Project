using System.Collections;
using UnityEngine;

// Rigidbody2D 컴포넌트가 없으면 자동으로 추가
[RequireComponent(typeof(Rigidbody2D))]
public class ObstacleMovement : MonoBehaviour
{
    [Header("기본 속도 & 가속도")]
    public float initialSpeed = 5f;               // 처음 스폰될 때 속도
    public float speedIncreasePerSecond = 0.1f;   // 매초 추가될 속도

    private Rigidbody2D rb;       // 물리 이동을 담당할 Rigidbody2D
    private float currentSpeed;   // 현재 이동 속도

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;  // 외부 힘 없이 직접 velocity 설정
        rb.freezeRotation = true;                 // 회전 방지
    }

    void OnEnable()
    {
        currentSpeed = initialSpeed;              // 활성화 시 속도 초기화
        StartCoroutine(Accelerate());             // 가속 코루틴 시작
        rb.velocity = Vector2.left * currentSpeed; // 첫 프레임부터 왼쪽으로 이동
    }

    // 매초 속도를 증가시키는 코루틴
    private IEnumerator Accelerate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);     // 1초 대기
            currentSpeed += speedIncreasePerSecond;  // 속도 증가
            rb.velocity = Vector2.left * currentSpeed; // 증가된 속도로 이동 방향 유지
        }
    }
}
