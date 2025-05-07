using UnityEngine;
using System.Collections.Generic;

public class ShopSystem : MonoBehaviour
{
    // 에디터 인스펙터에서 판매할 아이템 데이터를 할당하는 리스트
    [Header("판매 아이템 목록")]
    public List<ItemDataSO> itemsForSale = new List<ItemDataSO>();

    // 상호작용 트리거에서 호출되며, 상점 UI를 연다.
    public void OpenShopUI()
    {
        // 판매 아이템 개수를 콘솔에 출력(디버그용)
        Debug.Log("상점 오픈! 판매 아이템 수: " + itemsForSale.Count);
        // UIManager에 아이템 리스트를 전달해서 상점 화면을 표시하도록 요청
        UIManager.Instance.ShowShop(itemsForSale);
    }

    // 아이템 구매 로직의 뼈대(스텁) 메서드
    public bool PurchaseItem(int itemID)
    {
        // TODO:
        // 1) ItemDataSO에서 itemID에 맞는 아이템의 price를 가져오기
        // 2) GameSession.Instance.SpendGold(price)로 골드 차감 시도
        // 3) SpendGold가 true면 GameSession.Instance.PlayerData.ownedItemIDs.Add(itemID) 추가
        // 4) 구매 성공 시 true, 실패(골드 부족) 시 false 반환
        return true;
    }
}
