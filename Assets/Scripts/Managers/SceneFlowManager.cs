using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class SceneFlowManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static SceneFlowManager Instance { get; private set; }

    // �����Ϳ��� �Ҵ��ϴ� �̴ϰ��� ������ ����Ʈ (�� �̴ϰ����� ID, ��, ���� ����)
    public MiniGameDataSO[] miniGameDataList;

    // ������ �ε��� �̴ϰ����� ID�� ����
    private string nextMiniGameID;

    void Awake()
    {
        // �̱��� ����: ���� ������ �ν��Ͻ��� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // ���� �ε�� ������ OnSceneLoaded() ȣ��ǵ��� ���
            SceneManager.sceneLoaded += OnSceneLoaded;

            // TODO: miniGameDataList �ʱ�ȭ ���� (��: Resources �������� �ε�)
            // miniGameDataList = Resources.LoadAll<MiniGameDataSO>("MiniGames");
        }
        else
        {
            // �ߺ� ������ ��� ��� �ı�
            Destroy(gameObject);
        }
    }

    // TownScene ���� Ư�� �̴ϰ������� �̵��� �� ȣ��
    public void ToMiniGame(string miniGameID)
    {
        // �̵��� �̴ϰ��� ID ����
        nextMiniGameID = miniGameID;
        // �̴ϰ��� �� �ε�
        SceneManager.LoadScene("MiniGameScene");
    }

    // �� �ε� ���� �ڵ����� ȣ��Ǵ� �ݹ�
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �̴ϰ��� ���� �������� �� �߰� ó���� �� ������ ���⿡ �ۼ�
        if (scene.name == "MiniGameScene")
        {
            // MiniGameManager.Awake() ���� �̹�
            // instructionPanel Ȱ��ȭ, Time.timeScale = 0 ó�� ���� �մϴ�.
        }
    }

    // �̴ϰ����� ��ġ�� ������ ���ư� �� ȣ��
    public void ToTown()
    {
        // ���� �� �ε�
        SceneManager.LoadScene("TownScene");
    }
}
