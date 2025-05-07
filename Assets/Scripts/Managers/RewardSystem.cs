using UnityEngine;

public static class RewardSystem
{
    public static int CalculateGold(int score)
    {
        // 점수의 10%를 골드로 지급 (소수점 버림)
        return Mathf.FloorToInt(score * 0.1f);
    }
}