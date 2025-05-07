using UnityEngine;

[System.Serializable]
public class RewardRule
{
    public int minScore;      // 이 점수 이상일 때
    public int maxScore;      // 이 점수 미만일 때
    public int goldReward;    // 골드 보상
    public ItemDataSO[] itemRewards;  // 추가 아이템 보상 목록
}