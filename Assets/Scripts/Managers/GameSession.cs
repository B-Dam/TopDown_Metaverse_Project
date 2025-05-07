using System;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; }

    [Header("Player Data")]
    [SerializeField]
    private PlayerData playerData;     // 플레이어의 소유 아이템, 진행 상태 등을 저장하는 데이터
    public PlayerData PlayerData       // 외부에서 데이터 교체 혹은 조회할 수 있는 프로퍼티
    {
        get => playerData;
        set => playerData = value;
    }

    [Header("Economy")]
    [SerializeField]
    private int gold;                  // 현재 보유 중인 골드
    public int Gold => gold;           // 외부에서 읽기 전용으로 조회 가능

    public event Action<int> OnGoldChanged;  // 골드가 변경될 때마다 호출되는 이벤트

    public int LastMiniGameScore { get; set; }  // 마지막 플레이한 미니게임 점수를 저장

    private void Awake()
    {
        // 싱글톤 인스턴스 보장
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 씬 전환 시 파괴 방지
            Initialize();                   // 초기 데이터 세팅
        }
        else
        {
            Destroy(gameObject);            // 중복 생성된 경우 삭제
        }
    }

    private void Initialize()
    {
        // 실제 저장 시스템이 있으면 Load 로직을 여기에 넣음.
        // 예: var data = SaveLoadManager.Instance?.Load();
        // 로드 실패 시 기본값으로 초기화
        playerData = new PlayerData();
        gold = 0;

        // HUD 등 초기 UI 갱신용으로 현재 골드를 알려줌.
        OnGoldChanged?.Invoke(gold);
    }

    // 골드를 amount 만큼 늘리고, UI 갱신 및 저장 호출
    public void AddGold(int amount)
    {
        gold += amount;
        OnGoldChanged?.Invoke(gold);
        // 필요하면 SaveGame() 호출
    }

    // 골드를 amount 만큼 소비. 부족하면 false 리턴, 소비 성공 시 true 리턴
    public bool SpendGold(int amount)
    {
        if (gold < amount)
            return false;

        gold -= amount;
        OnGoldChanged?.Invoke(gold);
        // 필요하면 SaveGame() 호출
        return true;
    }

    /*
    // 저장 로직이 필요하면 이 메서드를 구현.
    private void SaveGame()
    {
        var data = new SaveData
        {
            playerData = playerData,
            gold       = gold
        };
        SaveLoadManager.Instance?.Save(data);
    }

    // SaveLoadManager를 위한 데이터 클래스 예시
    [Serializable]
    private class SaveData
    {
        public PlayerData playerData;
        public int        gold;
    }
    */
}