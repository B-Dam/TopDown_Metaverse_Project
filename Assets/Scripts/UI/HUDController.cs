using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text goldText;  // 화면 상단에 현재 골드를 표시할 TextMeshPro 텍스트 컴포넌트

    private void Awake()
    {
        // goldText가 에디터에서 할당되지 않았다면 경고 로그 출력 후 종료
        if (goldText == null)
        {
            Debug.LogWarning("HUDController.Awake: goldText가 할당되지 않았습니다.", this);
            return;
        }

        // GameSession 싱글톤 인스턴스가 없으면 경고 로그 출력 후 종료
        if (GameSession.Instance == null)
        {
            Debug.LogWarning("HUDController.Awake: GameSession.Instance가 null입니다.", this);
            return;
        }

        // 초기 HUD 텍스트 설정: 현재 보유 중인 골드를 표시
        goldText.text = GameSession.Instance.Gold.ToString();
    }

    private void OnEnable()
    {
        // HUD가 활성화될 때 GameSession의 골드 변경 이벤트를 구독
        if (GameSession.Instance != null)
            GameSession.Instance.OnGoldChanged += UpdateHUD;
    }

    private void OnDisable()
    {
        // HUD가 비활성화될 때 이벤트 구독 해제하여 메모리 누수 방지
        if (GameSession.Instance != null)
            GameSession.Instance.OnGoldChanged -= UpdateHUD;
    }

    // GameSession에서 골드가 변경될 때마다 호출되는 콜백
    private void UpdateHUD(int newGold)
    {
        // goldText가 유효하면 화면에 새로운 골드 수치 표시
        if (goldText != null)
            goldText.text = newGold.ToString();
    }
}
