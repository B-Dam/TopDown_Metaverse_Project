using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text goldText;  // ȭ�� ��ܿ� ���� ��带 ǥ���� TextMeshPro �ؽ�Ʈ ������Ʈ

    private void Awake()
    {
        // goldText�� �����Ϳ��� �Ҵ���� �ʾҴٸ� ��� �α� ��� �� ����
        if (goldText == null)
        {
            Debug.LogWarning("HUDController.Awake: goldText�� �Ҵ���� �ʾҽ��ϴ�.", this);
            return;
        }

        // GameSession �̱��� �ν��Ͻ��� ������ ��� �α� ��� �� ����
        if (GameSession.Instance == null)
        {
            Debug.LogWarning("HUDController.Awake: GameSession.Instance�� null�Դϴ�.", this);
            return;
        }

        // �ʱ� HUD �ؽ�Ʈ ����: ���� ���� ���� ��带 ǥ��
        goldText.text = GameSession.Instance.Gold.ToString();
    }

    private void OnEnable()
    {
        // HUD�� Ȱ��ȭ�� �� GameSession�� ��� ���� �̺�Ʈ�� ����
        if (GameSession.Instance != null)
            GameSession.Instance.OnGoldChanged += UpdateHUD;
    }

    private void OnDisable()
    {
        // HUD�� ��Ȱ��ȭ�� �� �̺�Ʈ ���� �����Ͽ� �޸� ���� ����
        if (GameSession.Instance != null)
            GameSession.Instance.OnGoldChanged -= UpdateHUD;
    }

    // GameSession���� ��尡 ����� ������ ȣ��Ǵ� �ݹ�
    private void UpdateHUD(int newGold)
    {
        // goldText�� ��ȿ�ϸ� ȭ�鿡 ���ο� ��� ��ġ ǥ��
        if (goldText != null)
            goldText.text = newGold.ToString();
    }
}
