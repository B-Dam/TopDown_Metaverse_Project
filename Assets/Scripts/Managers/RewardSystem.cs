using UnityEngine;

public static class RewardSystem
{
    public static int CalculateGold(int score)
    {
        // ������ 10%�� ���� ���� (�Ҽ��� ����)
        return Mathf.FloorToInt(score * 0.1f);
    }
}