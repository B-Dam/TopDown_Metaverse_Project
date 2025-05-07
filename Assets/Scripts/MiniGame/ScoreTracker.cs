using TMPro;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public TMP_Text scoreText; // ȭ�鿡 ������ ǥ���� TextMeshPro �ؽ�Ʈ ������Ʈ
    private float score;       // ���� ������ �ε��Ҽ������� ���� ����

    void Update()
    {
        // �� �����Ӹ��� Time.deltaTime * 10 ��ŭ ������ ����
        score += Time.deltaTime * 10;

        // ������ ������ ��ȯ�Ͽ� �ؽ�Ʈ�� ǥ��
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    // �ܺο��� ���� ������ ������ �� �� ����ϴ� �޼���
    public int GetScore()
    {
        return Mathf.FloorToInt(score);
    }
}
