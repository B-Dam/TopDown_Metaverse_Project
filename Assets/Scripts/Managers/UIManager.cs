using UnityEngine;
using UnityEngine.UI;          // ScrollRect, Button
using TMPro;                   // TMP_Text
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Dialog UI")]
    public GameObject dialogPanel;
    public TMP_Text dialogText;

    [Header("Shop UI")]
    public GameObject shopPanel;
    public Transform shopContentParent;
    public GameObject shopItemUIPrefab;

    [Header("Result UI")]
    public GameObject resultPanel;
    public TMP_Text scoreText;

    [Header("Reward UI")]
    public GameObject rewardPopup;
    public TMP_Text rewardText;

    private void Awake()
    {
        // �̱��� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            // ������ ��� �ÿ��� �� �� �ʱ�ȭ
            OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        }
        else Destroy(gameObject);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "TownScene")
        {
            // DialogPanel ã��
            var dlg = GameObject.Find("DialogPanel");
            if (dlg != null)
            {
                dialogPanel = dlg;
                dialogText = dlg.transform.Find("ContentText")
                                  .GetComponent<TMP_Text>();
            }

            // HUD_Canvas �������� ShopPanel ã��
            var hud = GameObject.Find("HUD_Canvas");
            if (hud != null)
            {
                var shopTrans = hud.transform.Find("ShopPanel");
                if (shopTrans != null)
                {
                    shopPanel = shopTrans.gameObject;

                    // Scroll View ��� �̸��� ScrollRect ���� ã��
                    Transform content = shopTrans.Find("Scroll View/Viewport/Content");
                    if (content == null)
                    {
                        // ������ ScrollRect ������Ʈ�� Content ����
                        var scrollRect = shopPanel.GetComponentInChildren<ScrollRect>();
                        if (scrollRect != null)
                            content = scrollRect.content;
                    }
                    if (content != null)
                        shopContentParent = content;

                    // ShopPanel �� CloseButton �̺�Ʈ ����
                    var closeShopBtn = shopTrans.Find("CloseButton");
                    if (closeShopBtn != null)
                    {
                        var btn = closeShopBtn.GetComponent<Button>();
                        btn.onClick.RemoveAllListeners();
                        btn.onClick.AddListener(HideShop);
                    }
                }

                // RewardPopup ã�� �� CloseButton ����
                var popupTrans = hud.transform.Find("RewardPopup");
                if (popupTrans != null)
                {
                    rewardPopup = popupTrans.gameObject;
                    rewardText = popupTrans.Find("RewardText")
                                     .GetComponent<TMP_Text>();
                    var closeRewardBtn = popupTrans.Find("CloseButton");
                    if (closeRewardBtn != null)
                    {
                        var btn = closeRewardBtn.GetComponent<Button>();
                        btn.onClick.RemoveAllListeners();
                        btn.onClick.AddListener(HideRewardPopup);
                    }
                }
            }
            return;
        }

        if (scene.name == "MiniGameScene")
        {
            // ResultPanel ã��
            var result = GameObject.Find("ResultPanel");
            if (result != null)
            {
                resultPanel = result;
                scoreText = result.transform.Find("ScoreText")
                                   .GetComponent<TMP_Text>();
            }
        }
    }

    // ��ȭâ ǥ��
    public void ShowDialog(string[] lines)
    {
        if (dialogPanel == null) return;
        dialogPanel.SetActive(true);
        dialogText.text = lines.Length > 0 ? lines[0] : "";
    }

    public void HideDialog()
    {
        if (dialogPanel != null)
            dialogPanel.SetActive(false);
    }

    // ����â ǥ��
    public void ShowShop(List<ItemDataSO> itemsForSale)
    {
        // shopPanel ������ ��ȿ���� ������ �ٽ� �ʱ�ȭ �õ�
        if (shopPanel == null || !shopPanel)
            OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        if (shopPanel == null) return;

        // shopContentParent ������ ��ȿ���� ������ �ٽ� ã�ƺ���
        if (shopContentParent == null)
        {
            var scrollRect = shopPanel.GetComponentInChildren<ScrollRect>();
            if (scrollRect != null)
                shopContentParent = scrollRect.content;
        }
        if (shopContentParent == null) return;

        // ���� UI Ȱ��ȭ �� ������ ����Ʈ ����
        shopPanel.SetActive(true);
        foreach (Transform t in shopContentParent)
            Destroy(t.gameObject);
        foreach (var item in itemsForSale)
        {
            var go = Instantiate(shopItemUIPrefab, shopContentParent);
            go.GetComponent<ShopItemUI>().Initialize(item);
        }
    }

    public void HideShop()
    {
        if (shopPanel != null)
            shopPanel.SetActive(false);
    }

    // �̴ϰ��� ��� ǥ��
    public void ShowResult(int score)
    {
        if (resultPanel == null) return;
        resultPanel.SetActive(true);
        scoreText.text = $"Score: {score}";
    }

    public void HideResultPanel()
    {
        if (resultPanel != null)
            resultPanel.SetActive(false);
    }

    public void OnCloseResult()
    {
        HideResultPanel();
        SceneFlowManager.Instance.ToTown();
    }

    // ���� �˾� ǥ��
    public void ShowRewardPopup(int gold)
    {
        // rewardPopup ������ ��ȿ���� ������ �ٽ� �ʱ�ȭ �õ�
        if (rewardPopup == null)
            OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        if (rewardPopup == null) return;

        rewardPopup.SetActive(true);
        rewardText.text = $"��� +{gold}";
    }

    public void HideRewardPopup()
    {
        if (rewardPopup != null)
            rewardPopup.SetActive(false);
    }
}
