using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance { get; private set; }

    [Header("UI References")]
    public GameObject instructionPanel;  // ó���� ������ ���� ���� �г�
    public Button startButton;           // ���� �г� �� '����' ��ư
    public GameObject countdownPanel;    // 3��2��1 ī��Ʈ�ٿ�� �г�
    public TMP_Text countdownText;       // ī��Ʈ�ٿ� ���ڸ� ǥ���� �ؽ�Ʈ
    public GameObject resultPanel;       // �̴ϰ��� ��� �г�
    public TMP_Text scoreText;           // ��� �гο� ������ ����� �ؽ�Ʈ

    void Awake()
    {
        // �̱��� ���� ����: �̹� Instance�� ������ �ڱ� �ڽ��� ���, ������ �ߺ� �ı�
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // �� �ε� ����, Ÿ�̸� �� �Է� ���� ���� �ð� �帧�� ���߰�
        Time.timeScale = 0f;

        // ���� �г��� �Ѽ� ���� ���� �� �ȳ��� ������
        instructionPanel.SetActive(true);

        // ���� ��ư Ŭ�� �� ���� �ʱ�ȭ
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(() =>
        {
            // �ȳ� �г� ����� ī��Ʈ�ٿ� ��ƾ ����
            instructionPanel.SetActive(false);
            StartCoroutine(CountdownAndStart());
        });
    }

    // 3�� ī��Ʈ�ٿ� �� ���� ���� �÷��̸� �簳�ϴ� �ڷ�ƾ
    private IEnumerator CountdownAndStart()
    {
        countdownPanel.SetActive(true);

        // 3, 2, 1 ������ �ؽ�Ʈ �����ϸ� 1�ʾ� ��� (�ǽð� ���)
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }

        // ī��Ʈ�ٿ� �г� ����� �ð� �帧 �簳
        countdownPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    // �̴ϰ����� ������ �� ȣ���� �޼���
    public void EndMiniGame(int score)
    {
        // 1) ���� ���ǿ� ���� ����
        GameSession.Instance.LastMiniGameScore = score;

        // 2) �ð� ���߰� ��� �г� ǥ��, ���� �ؽ�Ʈ ����
        Time.timeScale = 0f;
        resultPanel.SetActive(true);
        scoreText.text = $"Score: {score}";

        // 3) ���� ��� ��� ���� ��� �� ����
        int goldEarned = RewardSystem.CalculateGold(score);
        GameSession.Instance.AddGold(goldEarned);

        // 4) ��� ��� �� �ڵ����� ���� ������ ���ư�
        StartCoroutine(ReturnToTownAfterDelay());
    }

    // ��� ǥ�� �� 2�� ��ٷȴٰ� Ÿ�� ������ �����ϴ� �ڷ�ƾ
    private IEnumerator ReturnToTownAfterDelay()
    {
        yield return new WaitForSecondsRealtime(2f);

        // ���� �� �ð� �帧 �ٽ� Ȱ��ȭ
        Time.timeScale = 1f;

        // �� ��ȯ �Ŵ����� ���� ���� ������ �̵�
        SceneFlowManager.Instance.ToTown();
    }
}
