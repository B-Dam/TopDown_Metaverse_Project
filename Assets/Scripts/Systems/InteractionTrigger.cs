using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractionTrigger : MonoBehaviour
{
    // �� �ʵ忡 NPCController, ShopSystem, MiniGamePortal �� �ϳ��� �巡���ؼ� �����ϼ���.
    public MonoBehaviour interactable;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Debug.Log($"[Trigger] Player ���� ����: {gameObject.name}");

        if (InteractionSystem.Instance != null)
            InteractionSystem.Instance.SetActiveTrigger(this);
        else
            Debug.LogWarning("InteractionSystem.Instance�� null�Դϴ�. InteractionSystem�� ���� �����ϴ��� Ȯ���ϼ���.");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (InteractionSystem.Instance != null)
            InteractionSystem.Instance.ClearActiveTrigger(this);
        else
            Debug.LogWarning("InteractionSystem.Instance�� null�Դϴ�. InteractionSystem�� ���� �����ϴ��� Ȯ���ϼ���.");
    }

    public void DoInteract()
    {
        Debug.Log($"[InteractionTrigger] DoInteract ȣ��, interactable Ÿ�� = {interactable?.GetType().Name}");

        if (interactable == null)
        {
            Debug.LogWarning($"[{name}] interactable�� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        if (interactable is NPCController npc)
            npc.OnInteract();
        else if (interactable is ShopSystem shop)
            shop.OpenShopUI();
        else if (interactable is MiniGamePortal portal)
            portal.OnInteract();
        else
            Debug.LogWarning($"[InteractionTrigger] �� �� ���� interactable Ÿ��: {interactable.GetType().Name}");
    }
}
