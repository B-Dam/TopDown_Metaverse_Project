using System.Linq;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    // NPC�� ���� �����Ͱ� ����ִ� ScriptableObject
    public NPCDataSO npcData;

    // �̹� ������ ����� üũ�ؼ� �ߺ� ���� ����
    private bool rewardGiven = false;

    // �÷��̾ ��ȣ�ۿ� Ű�� ������ �� ȣ��Ǵ� �޼���
    public void OnInteract()
    {
        // ��ȭâ�� ���� npcData�� ������ ��� �迭�� ����
        DialogManager.Instance.StartDialog(npcData.dialogueLines);

        // �ѹ��� ���� ó������ �ʾҰ�, ���� ��Ģ�� ������, �̴ϰ��� ������ 0���� Ŭ ���� ����
        int score = GameSession.Instance.LastMiniGameScore;
        if (!rewardGiven
            && npcData.rewardRules.Length > 0
            && score > 0)
        {
            // �÷��̾� ������ �ش��ϴ� ���� ���� ã�ƿ�
            var rule = npcData.rewardRules
                .FirstOrDefault(r => score >= r.minScore && score < r.maxScore);

            if (rule != null)
            {
                // �꿡 ������ ��带 �÷��̾�� �߰�
                GameSession.Instance.AddGold(rule.goldReward);

                // �꿡 ������ �����۵��� �÷��̾� ���� ��Ͽ� �߰�
                foreach (var item in rule.itemRewards)
                    GameSession.Instance.PlayerData.ownedItemIDs.Add(item.id);

                // ���� �˾� UI�� ���� ��� ȹ�� �޽��� ǥ��
                UIManager.Instance.ShowRewardPopup(rule.goldReward);
            }

            // �ٽô� ���� ó������ �ʵ��� �÷��� ����
            rewardGiven = true;

            // ���� ó�� �� ���� �ʱ�ȭ�ؼ� ���ߺ� ����
            GameSession.Instance.LastMiniGameScore = 0;
        }
    }
}
