using UnityEngine;

[RequireComponent(typeof(Collider2D))]  // Collider2D�� ������ �ڵ����� �߰�
public class InteractionTrigger : MonoBehaviour
{
    // �� �ʵ忡 NPCController, ShopSystem, MiniGamePortal �� �ϳ��� �巡���ؼ� �����ϼ���.
    public MonoBehaviour interactable;

    // �÷��̾ Ʈ���� ������ ������ �� ȣ��
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;                              // �÷��̾ �ƴϸ� ����

        Debug.Log($"[Trigger] Player ���� ����: {gameObject.name}");

        // InteractionSystem�� �ڽ��� Ȱ�� Ʈ���ŷ� ���
        if (InteractionSystem.Instance != null)
            InteractionSystem.Instance.SetActiveTrigger(this);
        else
            Debug.LogWarning("InteractionSystem.Instance�� null�Դϴ�. InteractionSystem�� ���� �����ϴ��� Ȯ���ϼ���.");
    }

    // �÷��̾ Ʈ���� ������ ����� �� ȣ��
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;                              // �÷��̾ �ƴϸ� ����

        // InteractionSystem���� Ȱ�� Ʈ���� ����
        if (InteractionSystem.Instance != null)
            InteractionSystem.Instance.ClearActiveTrigger(this);
        else
            Debug.LogWarning("InteractionSystem.Instance�� null�Դϴ�. InteractionSystem�� ���� �����ϴ��� Ȯ���ϼ���.");
    }

    // InteractionSystem�� E Ű �Է��� �����ϸ� ȣ���ϴ� �޼���
    public void DoInteract()
    {
        Debug.Log($"[InteractionTrigger] DoInteract ȣ��, interactable Ÿ�� = {interactable?.GetType().Name}");

        if (interactable == null)
        {
            Debug.LogWarning($"[{name}] interactable�� �Ҵ���� �ʾҽ��ϴ�.");
            return;                              // �Ҵ�� ����� ������ �ߴ�
        }

        // �Ҵ�� ��ũ��Ʈ Ÿ�Կ� ���� �б� ó��
        if (interactable is NPCController npc)
            npc.OnInteract();                   // NPC ��ȣ�ۿ�
        else if (interactable is ShopSystem shop)
            shop.OpenShopUI();                  // ���� UI ����
        else if (interactable is MiniGamePortal portal)
            portal.OnInteract();                // �̴ϰ��� ���� ����
        else
            Debug.LogWarning($"[InteractionTrigger] �� �� ���� interactable Ÿ��: {interactable.GetType().Name}");
    }
}
