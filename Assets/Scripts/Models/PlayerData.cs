using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public int currentGold;
    public List<int> ownedItemIDs = new List<int>();      // ���� ������ ID ���
    public List<int> equippedItemIDs = new List<int>();   // ���� ���� ������ ID ���
    public Dictionary<string, int> bestScores = new Dictionary<string, int>();
    // miniGameID �� �ְ����� ����

    //ID ����� ����ϴ� ������ ���� �������� ���̺� ���� ũ�⸦ ���� �� �ְ�, ���°� �и��ؼ� ������ �� �ֱ� ����

    // (����) ���� �޼��� ����
    public void AddGold(int amount) => currentGold += amount; // ���� ȹ�� ������ ��带 ���� �� ���
    public bool SpendGold(int amount) // ���� ���� � ��� �Ҹ�� ��� (bool�� ó���� ���� ������ �� ���� ����ó��)
    {
        if (currentGold < amount) return false;
        currentGold -= amount;
        return true;
    }
    public void RecordScore(string miniGameID, int score) // �̴� ���� �÷��� �� ȣ��, ���� �ְ��������� ������ �����ϰ�, ù ����̸� ���� �߰�.
    {
        if (!bestScores.ContainsKey(miniGameID) || bestScores[miniGameID] < score)
            bestScores[miniGameID] = score;
    }
}
