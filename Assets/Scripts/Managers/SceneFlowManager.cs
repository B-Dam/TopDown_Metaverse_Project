using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class SceneFlowManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static SceneFlowManager Instance { get; private set; }

    // 에디터에서 할당하는 미니게임 데이터 리스트 (각 미니게임의 ID, 씬, 설정 포함)
    public MiniGameDataSO[] miniGameDataList;

    // 다음에 로드할 미니게임의 ID를 저장
    private string nextMiniGameID;

    void Awake()
    {
        // 싱글톤 패턴: 최초 생성된 인스턴스만 유지
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // 씬이 로드될 때마다 OnSceneLoaded() 호출되도록 등록
            SceneManager.sceneLoaded += OnSceneLoaded;

            // TODO: miniGameDataList 초기화 로직 (예: Resources 폴더에서 로드)
            // miniGameDataList = Resources.LoadAll<MiniGameDataSO>("MiniGames");
        }
        else
        {
            // 중복 생성된 경우 즉시 파괴
            Destroy(gameObject);
        }
    }

    // TownScene 에서 특정 미니게임으로 이동할 때 호출
    public void ToMiniGame(string miniGameID)
    {
        // 이동할 미니게임 ID 저장
        nextMiniGameID = miniGameID;
        // 미니게임 씬 로드
        SceneManager.LoadScene("MiniGameScene");
    }

    // 씬 로드 직후 자동으로 호출되는 콜백
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 미니게임 씬에 진입했을 때 추가 처리할 게 있으면 여기에 작성
        if (scene.name == "MiniGameScene")
        {
            // MiniGameManager.Awake() 에서 이미
            // instructionPanel 활성화, Time.timeScale = 0 처리 등을 합니다.
        }
    }

    // 미니게임을 마치고 마을로 돌아갈 때 호출
    public void ToTown()
    {
        // 마을 씬 로드
        SceneManager.LoadScene("TownScene");
    }
}
