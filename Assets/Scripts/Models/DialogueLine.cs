using UnityEngine;

[System.Serializable]
public struct DialogueLine
{
    public string speaker;   // 화자 이름
    [TextArea]              // 인스펙터에서 여러 줄 입력 가능
    public string text;      // 대사 내용
}