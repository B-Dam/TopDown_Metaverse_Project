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
        // 싱글톤 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            // 에디터 재생 시에도 한 번 초기화
            OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        }
        else Destroy(gameObject);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "TownScene")
        {
            // DialogPanel 찾기
            var dlg = GameObject.Find("DialogPanel");
            if (dlg != null)
            {
                dialogPanel = dlg;
                dialogText = dlg.transform.Find("ContentText")
                                  .GetComponent<TMP_Text>();
            }

            // HUD_Canvas 영역에서 ShopPanel 찾기
            var hud = GameObject.Find("HUD_Canvas");
            if (hud != null)
            {
                var shopTrans = hud.transform.Find("ShopPanel");
                if (shopTrans != null)
                {
                    shopPanel = shopTrans.gameObject;

                    // Scroll View 라는 이름의 ScrollRect 먼저 찾고
                    Transform content = shopTrans.Find("Scroll View/Viewport/Content");
                    if (content == null)
                    {
                        // 없으면 ScrollRect 컴포넌트로 Content 참조
                        var scrollRect = shopPanel.GetComponentInChildren<ScrollRect>();
                        if (scrollRect != null)
                            content = scrollRect.content;
                    }
                    if (content != null)
                        shopContentParent = content;

                    // ShopPanel 내 CloseButton 이벤트 연결
                    var closeShopBtn = shopTrans.Find("CloseButton");
                    if (closeShopBtn != null)
                    {
                        var btn = closeShopBtn.GetComponent<Button>();
                        btn.onClick.RemoveAllListeners();
                        btn.onClick.AddListener(HideShop);
                    }
                }

                // RewardPopup 찾기 및 CloseButton 연결
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
            // ResultPanel 찾기
            var result = GameObject.Find("ResultPanel");
            if (result != null)
            {
                resultPanel = result;
                scoreText = result.transform.Find("ScoreText")
                                   .GetComponent<TMP_Text>();
            }
        }
    }

    // 대화창 표시
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

    // 상점창 표시
    public void ShowShop(List<ItemDataSO> itemsForSale)
    {
        // shopPanel 참조가 유효하지 않으면 다시 초기화 시도
        if (shopPanel == null || !shopPanel)
            OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        if (shopPanel == null) return;

        // shopContentParent 참조가 유효하지 않으면 다시 찾아보기
        if (shopContentParent == null)
        {
            var scrollRect = shopPanel.GetComponentInChildren<ScrollRect>();
            if (scrollRect != null)
                shopContentParent = scrollRect.content;
        }
        if (shopContentParent == null) return;

        // 상점 UI 활성화 및 아이템 리스트 생성
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

    // 미니게임 결과 표시
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

    // 보상 팝업 표시
    public void ShowRewardPopup(int gold)
    {
        // rewardPopup 참조가 유효하지 않으면 다시 초기화 시도
        if (rewardPopup == null)
            OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        if (rewardPopup == null) return;

        rewardPopup.SetActive(true);
        rewardText.text = $"골드 +{gold}";
    }

    public void HideRewardPopup()
    {
        if (rewardPopup != null)
            rewardPopup.SetActive(false);
    }
}
