using UnityEngine;

[RequireComponent(typeof(Collider2D))]  // Collider2D가 없으면 자동으로 추가
public class InteractionTrigger : MonoBehaviour
{
    // 이 필드에 NPCController, ShopSystem, MiniGamePortal 중 하나를 드래그해서 연결하세요.
    public MonoBehaviour interactable;

    // 플레이어가 트리거 영역에 들어왔을 때 호출
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;                              // 플레이어가 아니면 무시

        Debug.Log($"[Trigger] Player 범위 진입: {gameObject.name}");

        // InteractionSystem에 자신을 활성 트리거로 등록
        if (InteractionSystem.Instance != null)
            InteractionSystem.Instance.SetActiveTrigger(this);
        else
            Debug.LogWarning("InteractionSystem.Instance가 null입니다. InteractionSystem이 씬에 존재하는지 확인하세요.");
    }

    // 플레이어가 트리거 영역을 벗어났을 때 호출
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;                              // 플레이어가 아니면 무시

        // InteractionSystem에서 활성 트리거 해제
        if (InteractionSystem.Instance != null)
            InteractionSystem.Instance.ClearActiveTrigger(this);
        else
            Debug.LogWarning("InteractionSystem.Instance가 null입니다. InteractionSystem이 씬에 존재하는지 확인하세요.");
    }

    // InteractionSystem이 E 키 입력을 감지하면 호출하는 메서드
    public void DoInteract()
    {
        Debug.Log($"[InteractionTrigger] DoInteract 호출, interactable 타입 = {interactable?.GetType().Name}");

        if (interactable == null)
        {
            Debug.LogWarning($"[{name}] interactable이 할당되지 않았습니다.");
            return;                              // 할당된 대상이 없으면 중단
        }

        // 할당된 스크립트 타입에 따라 분기 처리
        if (interactable is NPCController npc)
            npc.OnInteract();                   // NPC 상호작용
        else if (interactable is ShopSystem shop)
            shop.OpenShopUI();                  // 상점 UI 열기
        else if (interactable is MiniGamePortal portal)
            portal.OnInteract();                // 미니게임 포털 실행
        else
            Debug.LogWarning($"[InteractionTrigger] 알 수 없는 interactable 타입: {interactable.GetType().Name}");
    }
}
