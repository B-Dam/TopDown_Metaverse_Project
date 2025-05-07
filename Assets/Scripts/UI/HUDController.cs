using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TMP_Text goldText;

    private void Awake()
    {
        if (goldText == null)
        {
            Debug.LogWarning("HUDController.awake: goldText 가 할당되지 않았습니다.", this);
            return;
        }
        if (GameSession.Instance == null)
        {
            Debug.LogWarning("HUDController.awake: GameSession.Instance 가 null 입니다.", this);
            return;
        }

        // 안전하게 초기화
        goldText.text = GameSession.Instance.Gold.ToString();
    }

    private void OnEnable()
    {
        if (GameSession.Instance != null)
            GameSession.Instance.OnGoldChanged += UpdateHUD;
    }

    private void OnDisable()
    {
        if (GameSession.Instance != null)
            GameSession.Instance.OnGoldChanged -= UpdateHUD;
    }

    private void UpdateHUD(int newGold)
    {
        if (goldText != null)
            goldText.text = newGold.ToString();
    }
}
