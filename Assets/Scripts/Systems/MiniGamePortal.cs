using UnityEngine;

[RequireComponent(typeof(InteractionTrigger))]
public class MiniGamePortal : MonoBehaviour
{
    [Header("연결할 미니게임 데이터")]
    public MiniGameDataSO miniGameData;  // Inspector 에서 Dino 같은 ID 선택

    // InteractionTrigger가 호출합니다
    public void OnInteract()
    {
        if (miniGameData == null)
        {
            Debug.LogWarning("MiniGamePortal: 미니게임 데이터가 할당되지 않았습니다!");
            return;
        }
        // 씬 전환
        Debug.Log("[MiniGamePortal] OnInteract 호출! gameData.id=" + miniGameData.id);
        SceneFlowManager.Instance.ToMiniGame(miniGameData.id);
    }
}
