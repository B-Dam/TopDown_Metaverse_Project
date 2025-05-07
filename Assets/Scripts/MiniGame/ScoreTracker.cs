using TMPro;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public TMP_Text scoreText; // 화면에 점수를 표시할 TextMeshPro 텍스트 컴포넌트
    private float score;       // 실제 점수를 부동소수점으로 누적 저장

    void Update()
    {
        // 매 프레임마다 Time.deltaTime * 10 만큼 점수를 증가
        score += Time.deltaTime * 10;

        // 점수를 정수로 변환하여 텍스트로 표시
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    // 외부에서 현재 점수를 정수로 얻어갈 때 사용하는 메서드
    public int GetScore()
    {
        return Mathf.FloorToInt(score);
    }
}
