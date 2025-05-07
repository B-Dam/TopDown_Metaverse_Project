using UnityEngine;
using UnityEngine.UI;
using TMPro;   // �� �߰�

public class ShopItemUI : MonoBehaviour
{
    [Header("UI References")]
    public Image iconImage;

    // Legacy Text �� TMP_Text �� ����
    public TMP_Text nameText;
    public TMP_Text priceText;

    public Button buyButton;

    private ItemDataSO itemData;

    /// <summary>
    /// UI �ʱ�ȭ ȣ�� �޼���
    /// </summary>
    public void Initialize(ItemDataSO item)
    {
        itemData = item;

        if (iconImage != null) iconImage.sprite = item.icon;
        if (nameText != null) nameText.text = item.itemName;
        if (priceText != null) priceText.text = item.price.ToString();

        // ���� ��ư �̺�Ʈ ����
        if (buyButton != null)
        {
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(OnBuyClicked);
        }
    }

    /// <summary>
    /// ���� ��ư Ŭ�� �� ó��
    /// </summary>
    private void OnBuyClicked()
    {
        if (GameSession.Instance.SpendGold(itemData.price))
        {
            GameSession.Instance.PlayerData.ownedItemIDs.Add(itemData.id);
            Debug.Log($"[{itemData.itemName}] ���� �Ϸ�!");
            // HUD ������ GameSession.OnGoldChanged �̺�Ʈ�� �ڵ� ó���˴ϴ�.
        }
        else
        {
            Debug.LogWarning("��尡 �����մϴ�!");
        }
    }
}
