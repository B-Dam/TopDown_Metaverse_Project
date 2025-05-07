using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractionTrigger : MonoBehaviour
{
    // 이 필드에 NPCController, ShopSystem, MiniGamePortal 중 하나를 드래그해서 연결하세요.
    public MonoBehaviour interactable;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Debug.Log($"[Trigger] Player 범위 진입: {gameObject.name}");

        if (InteractionSystem.Instance != null)
            InteractionSystem.Instance.SetActiveTrigger(this);
        else
            Debug.LogWarning("InteractionSystem.Instance가 null입니다. InteractionSystem이 씬에 존재하는지 확인하세요.");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (InteractionSystem.Instance != null)
            InteractionSystem.Instance.ClearActiveTrigger(this);
        else
            Debug.LogWarning("InteractionSystem.Instance가 null입니다. InteractionSystem이 씬에 존재하는지 확인하세요.");
    }

    public void DoInteract()
    {
        Debug.Log($"[InteractionTrigger] DoInteract 호출, interactable 타입 = {interactable?.GetType().Name}");

        if (interactable == null)
        {
            Debug.LogWarning($"[{name}] interactable이 할당되지 않았습니다.");
            return;
        }

        if (interactable is NPCController npc)
            npc.OnInteract();
        else if (interactable is ShopSystem shop)
            shop.OpenShopUI();
        else if (interactable is MiniGamePortal portal)
            portal.OnInteract();
        else
            Debug.LogWarning($"[InteractionTrigger] 알 수 없는 interactable 타입: {interactable.GetType().Name}");
    }
}
