using UnityEngine;

[CreateAssetMenu(menuName = "Data/MiniGameData")]
public class MiniGameDataSO : ScriptableObject
{
    public string id;             // ���� �ĺ���
    public string sceneName;      // �ҷ��� �� �̸�
    public int targetScore;       // ��ǥ ����
    public Sprite uiSkin;         // �̴ϰ��� UI ��Ų
}