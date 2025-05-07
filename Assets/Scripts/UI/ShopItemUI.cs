using UnityEngine;
using UnityEngine.UI;
using TMPro;   // ← 추가

public class ShopItemUI : MonoBehaviour
{
    [Header("UI References")]
    public Image iconImage;

    // Legacy Text → TMP_Text 로 변경
    public TMP_Text nameText;
    public TMP_Text priceText;

    public Button buyButton;

    private ItemDataSO itemData;

    /// <summary>
    /// UI 초기화 호출 메서드
    /// </summary>
    public void Initialize(ItemDataSO item)
    {
        itemData = item;

        if (iconImage != null) iconImage.sprite = item.icon;
        if (nameText != null) nameText.text = item.itemName;
        if (priceText != null) priceText.text = item.price.ToString();

        // 구매 버튼 이벤트 연결
        if (buyButton != null)
        {
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(OnBuyClicked);
        }
    }

    /// <summary>
    /// 구매 버튼 클릭 시 처리
    /// </summary>
    private void OnBuyClicked()
    {
        if (GameSession.Instance.SpendGold(itemData.price))
        {
            GameSession.Instance.PlayerData.ownedItemIDs.Add(itemData.id);
            Debug.Log($"[{itemData.itemName}] 구매 완료!");
            // HUD 갱신은 GameSession.OnGoldChanged 이벤트로 자동 처리됩니다.
        }
        else
        {
            Debug.LogWarning("골드가 부족합니다!");
        }
    }
}
