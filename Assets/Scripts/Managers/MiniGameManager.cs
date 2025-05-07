using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance { get; private set; }

    [Header("UI References")]
    public GameObject instructionPanel;  // 처음에 보여줄 게임 설명 패널
    public Button startButton;           // 설명 패널 내 '시작' 버튼
    public GameObject countdownPanel;    // 3·2·1 카운트다운용 패널
    public TMP_Text countdownText;       // 카운트다운 숫자를 표시할 텍스트
    public GameObject resultPanel;       // 미니게임 결과 패널
    public TMP_Text scoreText;           // 결과 패널에 점수를 출력할 텍스트

    void Awake()
    {
        // 싱글톤 패턴 설정: 이미 Instance가 없으면 자기 자신을 등록, 있으면 중복 파괴
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 씬 로드 직후, 타이머 및 입력 막기 위해 시간 흐름을 멈추고
        Time.timeScale = 0f;

        // 설명 패널을 켜서 게임 시작 전 안내를 보여줌
        instructionPanel.SetActive(true);

        // 시작 버튼 클릭 시 동작 초기화
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(() =>
        {
            // 안내 패널 숨기고 카운트다운 루틴 시작
            instructionPanel.SetActive(false);
            StartCoroutine(CountdownAndStart());
        });
    }

    // 3초 카운트다운 후 실제 게임 플레이를 재개하는 코루틴
    private IEnumerator CountdownAndStart()
    {
        countdownPanel.SetActive(true);

        // 3, 2, 1 순으로 텍스트 변경하며 1초씩 대기 (실시간 대기)
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }

        // 카운트다운 패널 숨기고 시간 흐름 재개
        countdownPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    // 미니게임이 끝났을 때 호출할 메서드
    public void EndMiniGame(int score)
    {
        // 1) 게임 세션에 점수 저장
        GameSession.Instance.LastMiniGameScore = score;

        // 2) 시간 멈추고 결과 패널 표시, 점수 텍스트 설정
        Time.timeScale = 0f;
        resultPanel.SetActive(true);
        scoreText.text = $"Score: {score}";

        // 3) 점수 기반 골드 보상 계산 및 지급
        int goldEarned = RewardSystem.CalculateGold(score);
        GameSession.Instance.AddGold(goldEarned);

        // 4) 잠시 대기 후 자동으로 마을 씬으로 돌아감
        StartCoroutine(ReturnToTownAfterDelay());
    }

    // 결과 표시 후 2초 기다렸다가 타운 씬으로 복귀하는 코루틴
    private IEnumerator ReturnToTownAfterDelay()
    {
        yield return new WaitForSecondsRealtime(2f);

        // 복귀 전 시간 흐름 다시 활성화
        Time.timeScale = 1f;

        // 씬 전환 매니저를 통해 마을 씬으로 이동
        SceneFlowManager.Instance.ToTown();
    }
}
