using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    public static InteractionSystem Instance { get; private set; }

    // 플레이어가 현재 들어와 있는 상호작용 트리거
    private InteractionTrigger activeTrigger;

    void Awake()
    {
        // 싱글톤 패턴 설정: 최초 인스턴스만 유지
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 씬 전환 시 파괴 방지
        }
        else
        {
            Destroy(gameObject);  // 중복 생성된 경우 삭제
        }
    }

    void Update()
    {
        // 상호작용 가능한 트리거가 있고, E 키를 눌렀을 때
        if (activeTrigger != null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("[InteractionSystem] E 키 입력 감지 → DoInteract 호출");
            // 현재 활성화된 트리거의 상호작용 메서드 실행
            activeTrigger.DoInteract();
        }
    }

    // 플레이어가 트리거 영역에 진입할 때 InteractionTrigger에서 호출
    public void SetActiveTrigger(InteractionTrigger trigger)
    {
        activeTrigger = trigger;
        // TODO: 화면에 'E키를 눌러 상호작용' 같은 힌트 UI 표시
    }

    // 플레이어가 트리거 영역을 벗어날 때 InteractionTrigger에서 호출
    public void ClearActiveTrigger(InteractionTrigger trigger)
    {
        // 영역에서 벗어난 트리거가 현재 활성화된 것과 같으면 해제
        if (activeTrigger == trigger)
            activeTrigger = null;
        // TODO: 상호작용 힌트 UI 숨기기
    }
}
