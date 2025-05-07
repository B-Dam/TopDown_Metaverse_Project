using UnityEngine;

[CreateAssetMenu(menuName = "Data/NPCData")]
public class NPCDataSO : ScriptableObject
{
    public string npcName;
    public DialogueLine[] dialogueLines; // struct �Ǵ� Ŭ������ ����
    public string miniGameID;            // ������ �̴ϰ��� ID
    public RewardRule[] rewardRules;     // ������ ���� ��Ģ
}