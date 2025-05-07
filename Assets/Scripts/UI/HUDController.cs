using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TMP_Text goldText;

    private void Awake()
    {
        if (goldText == null)
        {
            Debug.LogWarning("HUDController.awake: goldText �� �Ҵ���� �ʾҽ��ϴ�.", this);
            return;
        }
        if (GameSession.Instance == null)
        {
            Debug.LogWarning("HUDController.awake: GameSession.Instance �� null �Դϴ�.", this);
            return;
        }

        // �����ϰ� �ʱ�ȭ
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
