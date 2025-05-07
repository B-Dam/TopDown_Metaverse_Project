using UnityEngine;

[CreateAssetMenu(menuName = "Data/NPCData")]
public class NPCDataSO : ScriptableObject
{
    public string npcName;
    public DialogueLine[] dialogueLines; // struct 또는 클래스로 정의
    public string miniGameID;            // 연동할 미니게임 ID
    public RewardRule[] rewardRules;     // 점수별 보상 규칙
}