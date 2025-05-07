using UnityEngine;
using UnityEngine.UI;
using TMPro;   // TextMeshPro 사용을 위한 네임스페이스

public class ShopItemUI : MonoBehaviour
{
    [Header("UI References")]
    public Image iconImage;     // 아이템 아이콘을 표시할 Image 컴포넌트
    public TMP_Text nameText;   // 아이템 이름을 표시할 TMP 텍스트
    public TMP_Text priceText;  // 아이템 가격을 표시할 TMP 텍스트
    public Button buyButton;    // 구매 버튼

    private ItemDataSO itemData;  // 이 UI가 표현할 아이템 데이터

    // 이 메서드를 호출하면 UI가 해당 아이템 정보로 초기화됩니다
    public void Initialize(ItemDataSO item)
    {
        // 전달받은 아이템 데이터를 저장
        itemData = item;

        // 아이콘 설정
        if (iconImage != null)
            iconImage.sprite = item.icon;

        // 이름 텍스트 설정
        if (nameText != null)
            nameText.text = item.itemName;

        // 가격 텍스트 설정
        if (priceText != null)
            priceText.text = item.price.ToString();

        // 구매 버튼 누를 때마다 이벤트 연결을 초기화해 중복 리스너 방지
        if (buyButton != null)
        {
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(OnBuyClicked);
        }
    }

    // 구매 버튼 클릭 시 실행되는 메서드
    private void OnBuyClicked()
    {
        // 골드가 충분한지 시도하고, 성공 시 true 반환
        if (GameSession.Instance.SpendGold(itemData.price))
        {
            // 구매 성공: 소유 아이템 목록에 추가
            GameSession.Instance.PlayerData.ownedItemIDs.Add(itemData.id);
            Debug.Log($"[{itemData.itemName}] 구매 완료!");
            // 골드 변경은 GameSession.OnGoldChanged 이벤트로 HUD에 자동 반영
        }
        else
        {
            // 골드 부족 경고
            Debug.LogWarning("골드가 부족합니다!");
        }
    }
}
