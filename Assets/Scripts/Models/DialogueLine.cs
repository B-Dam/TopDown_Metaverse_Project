using UnityEngine;

[System.Serializable]
public struct DialogueLine
{
    public string speaker;   // ȭ�� �̸�
    [TextArea]              // �ν����Ϳ��� ���� �� �Է� ����
    public string text;      // ��� ����
}