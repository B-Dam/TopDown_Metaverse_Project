using UnityEngine;

public class GroundLooper : MonoBehaviour
{
    [Header("이동 속도 및 루핑 폭 설정")]
    public float speed = 5f;      // 지면이 좌측으로 이동하는 속도
    public float tileWidth;       // 한 번 루핑할 때 기준이 되는 타일의 가로 폭

    private Vector3 startPos;     // 원래 시작 위치를 저장

    void Awake()
    {
        // 시작 위치를 캐싱
        startPos = transform.position;

        // 만약 SpriteRenderer가 붙어 있으면, 스프라이트의 실제 폭을 자동으로 가져와서 tileWidth에 설정
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            tileWidth = sr.bounds.size.x;
    }

    void Update()
    {
        // 1) 매 프레임 좌측으로 일정 속도로 이동
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // 2) 지정한 폭만큼 이동했으면 원래 위치(startPos)로 순간 이동시켜 루프 효과 연출
        if (transform.position.x <= startPos.x - tileWidth)
            transform.position = startPos;
    }
}
