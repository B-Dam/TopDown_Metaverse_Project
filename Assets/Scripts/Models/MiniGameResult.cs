[System.Serializable]
public class MiniGameResult
{
    public string miniGameID;   // 어떤 미니게임 결과인지
    public int lastScore;       // 방금 플레이한 점수
    public int bestScore;       // 세션 내 최고 점수

    public MiniGameResult(string id, int last, int best)
    {
        miniGameID = id;
        lastScore = last;
        bestScore = best;
    }
}