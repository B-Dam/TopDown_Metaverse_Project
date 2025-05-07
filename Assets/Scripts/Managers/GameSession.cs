using System;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; }

    [Header("Player Data")]
    [SerializeField]
    private PlayerData playerData;     // �÷��̾��� ���� ������, ���� ���� ���� �����ϴ� ������
    public PlayerData PlayerData       // �ܺο��� ������ ��ü Ȥ�� ��ȸ�� �� �ִ� ������Ƽ
    {
        get => playerData;
        set => playerData = value;
    }

    [Header("Economy")]
    [SerializeField]
    private int gold;                  // ���� ���� ���� ���
    public int Gold => gold;           // �ܺο��� �б� �������� ��ȸ ����

    public event Action<int> OnGoldChanged;  // ��尡 ����� ������ ȣ��Ǵ� �̺�Ʈ

    public int LastMiniGameScore { get; set; }  // ������ �÷����� �̴ϰ��� ������ ����

    private void Awake()
    {
        // �̱��� �ν��Ͻ� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // �� ��ȯ �� �ı� ����
            Initialize();                   // �ʱ� ������ ����
        }
        else
        {
            Destroy(gameObject);            // �ߺ� ������ ��� ����
        }
    }

    private void Initialize()
    {
        // ���� ���� �ý����� ������ Load ������ ���⿡ ����.
        // ��: var data = SaveLoadManager.Instance?.Load();
        // �ε� ���� �� �⺻������ �ʱ�ȭ
        playerData = new PlayerData();
        gold = 0;

        // HUD �� �ʱ� UI ���ſ����� ���� ��带 �˷���.
        OnGoldChanged?.Invoke(gold);
    }

    // ��带 amount ��ŭ �ø���, UI ���� �� ���� ȣ��
    public void AddGold(int amount)
    {
        gold += amount;
        OnGoldChanged?.Invoke(gold);
        // �ʿ��ϸ� SaveGame() ȣ��
    }

    // ��带 amount ��ŭ �Һ�. �����ϸ� false ����, �Һ� ���� �� true ����
    public bool SpendGold(int amount)
    {
        if (gold < amount)
            return false;

        gold -= amount;
        OnGoldChanged?.Invoke(gold);
        // �ʿ��ϸ� SaveGame() ȣ��
        return true;
    }

    /*
    // ���� ������ �ʿ��ϸ� �� �޼��带 ����.
    private void SaveGame()
    {
        var data = new SaveData
        {
            playerData = playerData,
            gold       = gold
        };
        SaveLoadManager.Instance?.Save(data);
    }

    // SaveLoadManager�� ���� ������ Ŭ���� ����
    [Serializable]
    private class SaveData
    {
        public PlayerData playerData;
        public int        gold;
    }
    */
}