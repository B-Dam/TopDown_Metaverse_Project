using UnityEngine;

[CreateAssetMenu(menuName = "Data/MiniGameData")]
public class MiniGameDataSO : ScriptableObject
{
    public string id;             // 고유 식별자
    public string sceneName;      // 불러올 씬 이름
    public int targetScore;       // 목표 점수
    public Sprite uiSkin;         // 미니게임 UI 스킨
}