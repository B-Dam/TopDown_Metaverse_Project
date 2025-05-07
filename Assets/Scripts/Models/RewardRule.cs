using UnityEngine;

[System.Serializable]
public class RewardRule
{
    public int minScore;      // �� ���� �̻��� ��
    public int maxScore;      // �� ���� �̸��� ��
    public int goldReward;    // ��� ����
    public ItemDataSO[] itemRewards;  // �߰� ������ ���� ���
}