using UnityEngine;
using System.Collections.Generic;

public class ShopSystem : MonoBehaviour
{
    // ������ �ν����Ϳ��� �Ǹ��� ������ �����͸� �Ҵ��ϴ� ����Ʈ
    [Header("�Ǹ� ������ ���")]
    public List<ItemDataSO> itemsForSale = new List<ItemDataSO>();

    // ��ȣ�ۿ� Ʈ���ſ��� ȣ��Ǹ�, ���� UI�� ����.
    public void OpenShopUI()
    {
        // �Ǹ� ������ ������ �ֿܼ� ���(����׿�)
        Debug.Log("���� ����! �Ǹ� ������ ��: " + itemsForSale.Count);
        // UIManager�� ������ ����Ʈ�� �����ؼ� ���� ȭ���� ǥ���ϵ��� ��û
        UIManager.Instance.ShowShop(itemsForSale);
    }

    // ������ ���� ������ ����(����) �޼���
    public bool PurchaseItem(int itemID)
    {
        // TODO:
        // 1) ItemDataSO���� itemID�� �´� �������� price�� ��������
        // 2) GameSession.Instance.SpendGold(price)�� ��� ���� �õ�
        // 3) SpendGold�� true�� GameSession.Instance.PlayerData.ownedItemIDs.Add(itemID) �߰�
        // 4) ���� ���� �� true, ����(��� ����) �� false ��ȯ
        return true;
    }
}
