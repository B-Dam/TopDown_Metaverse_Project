[System.Serializable]
public class MiniGameResult
{
    public string miniGameID;   // � �̴ϰ��� �������
    public int lastScore;       // ��� �÷����� ����
    public int bestScore;       // ���� �� �ְ� ����

    public MiniGameResult(string id, int last, int best)
    {
        miniGameID = id;
        lastScore = last;
        bestScore = best;
    }
}