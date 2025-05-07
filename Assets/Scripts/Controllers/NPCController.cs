using System.Linq;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    // NPC의 설정 데이터가 들어있는 ScriptableObject
    public NPCDataSO npcData;

    // 이미 보상을 줬는지 체크해서 중복 지급 방지
    private bool rewardGiven = false;

    // 플레이어가 상호작용 키를 눌렀을 때 호출되는 메서드
    public void OnInteract()
    {
        // 대화창을 띄우고 npcData에 설정된 대사 배열을 전달
        DialogManager.Instance.StartDialog(npcData.dialogueLines);

        // 한번도 보상 처리하지 않았고, 보상 규칙이 있으며, 미니게임 점수가 0보다 클 때만 실행
        int score = GameSession.Instance.LastMiniGameScore;
        if (!rewardGiven
            && npcData.rewardRules.Length > 0
            && score > 0)
        {
            // 플레이어 점수에 해당하는 보상 룰을 찾아옴
            var rule = npcData.rewardRules
                .FirstOrDefault(r => score >= r.minScore && score < r.maxScore);

            if (rule != null)
            {
                // 룰에 지정된 골드를 플레이어에게 추가
                GameSession.Instance.AddGold(rule.goldReward);

                // 룰에 지정된 아이템들을 플레이어 소유 목록에 추가
                foreach (var item in rule.itemRewards)
                    GameSession.Instance.PlayerData.ownedItemIDs.Add(item.id);

                // 보상 팝업 UI를 통해 골드 획득 메시지 표시
                UIManager.Instance.ShowRewardPopup(rule.goldReward);
            }

            // 다시는 보상 처리되지 않도록 플래그 설정
            rewardGiven = true;

            // 보상 처리 후 점수 초기화해서 재중복 방지
            GameSession.Instance.LastMiniGameScore = 0;
        }
    }
}
