using UnityEngine;

[RequireComponent(typeof(InteractionTrigger))]
public class MiniGamePortal : MonoBehaviour
{
    [Header("������ �̴ϰ��� ������")]
    public MiniGameDataSO miniGameData;  // Inspector ���� Dino ���� ID ����

    // InteractionTrigger�� ȣ���մϴ�
    public void OnInteract()
    {
        if (miniGameData == null)
        {
            Debug.LogWarning("MiniGamePortal: �̴ϰ��� �����Ͱ� �Ҵ���� �ʾҽ��ϴ�!");
            return;
        }
        // �� ��ȯ
        Debug.Log("[MiniGamePortal] OnInteract ȣ��! gameData.id=" + miniGameData.id);
        SceneFlowManager.Instance.ToMiniGame(miniGameData.id);
    }
}
