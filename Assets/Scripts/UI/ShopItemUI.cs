using UnityEngine;
using UnityEngine.UI;
using TMPro;   // TextMeshPro ����� ���� ���ӽ����̽�

public class ShopItemUI : MonoBehaviour
{
    [Header("UI References")]
    public Image iconImage;     // ������ �������� ǥ���� Image ������Ʈ
    public TMP_Text nameText;   // ������ �̸��� ǥ���� TMP �ؽ�Ʈ
    public TMP_Text priceText;  // ������ ������ ǥ���� TMP �ؽ�Ʈ
    public Button buyButton;    // ���� ��ư

    private ItemDataSO itemData;  // �� UI�� ǥ���� ������ ������

    // �� �޼��带 ȣ���ϸ� UI�� �ش� ������ ������ �ʱ�ȭ�˴ϴ�
    public void Initialize(ItemDataSO item)
    {
        // ���޹��� ������ �����͸� ����
        itemData = item;

        // ������ ����
        if (iconImage != null)
            iconImage.sprite = item.icon;

        // �̸� �ؽ�Ʈ ����
        if (nameText != null)
            nameText.text = item.itemName;

        // ���� �ؽ�Ʈ ����
        if (priceText != null)
            priceText.text = item.price.ToString();

        // ���� ��ư ���� ������ �̺�Ʈ ������ �ʱ�ȭ�� �ߺ� ������ ����
        if (buyButton != null)
        {
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(OnBuyClicked);
        }
    }

    // ���� ��ư Ŭ�� �� ����Ǵ� �޼���
    private void OnBuyClicked()
    {
        // ��尡 ������� �õ��ϰ�, ���� �� true ��ȯ
        if (GameSession.Instance.SpendGold(itemData.price))
        {
            // ���� ����: ���� ������ ��Ͽ� �߰�
            GameSession.Instance.PlayerData.ownedItemIDs.Add(itemData.id);
            Debug.Log($"[{itemData.itemName}] ���� �Ϸ�!");
            // ��� ������ GameSession.OnGoldChanged �̺�Ʈ�� HUD�� �ڵ� �ݿ�
        }
        else
        {
            // ��� ���� ���
            Debug.LogWarning("��尡 �����մϴ�!");
        }
    }
}
