using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public int currentGold;
    public List<int> ownedItemIDs = new List<int>();      // 보유 아이템 ID 목록
    public List<int> equippedItemIDs = new List<int>();   // 장착 중인 아이템 ID 목록
    public Dictionary<string, int> bestScores = new Dictionary<string, int>();
    // miniGameID → 최고점수 매핑

    //ID 방식을 사용하는 이유는 에셋 참조보다 세이브 파일 크기를 줄일 수 있고, 에셋과 분리해서 관리할 수 있기 때문

    // (선택) 편의 메서드 예시
    public void AddGold(int amount) => currentGold += amount; // 보상 획득 등으로 골드를 더할 때 사용
    public bool SpendGold(int amount) // 상점 구매 등에 골드 소모시 사용 (bool로 처리해 돈이 부족할 땐 구매 실패처리)
    {
        if (currentGold < amount) return false;
        currentGold -= amount;
        return true;
    }
    public void RecordScore(string miniGameID, int score) // 미니 게임 플레이 후 호출, 기존 최고점수보다 높으면 갱신하고, 첫 기록이면 새로 추가.
    {
        if (!bestScores.ContainsKey(miniGameID) || bestScores[miniGameID] < score)
            bestScores[miniGameID] = score;
    }
}
