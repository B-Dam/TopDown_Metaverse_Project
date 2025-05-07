using UnityEngine;
using TMPro;
using System.Collections;

public class DialogManager : MonoBehaviour
{
    // �������� ���� ������ �̱��� �ν��Ͻ�
    public static DialogManager Instance { get; private set; }

    [Header("UI References")]
    public GameObject dialogPanel;   // ��ȭâ ��ü �г�
    public TMP_Text speakerText;     // ��ȭ ��ܿ� ǥ���� ȭ�� �̸�
    public TMP_Text contentText;     // ��� ������ �� ���ھ� ����� �ؽ�Ʈ

    public float textSpeed = 0.05f;  // �� ���ڴ� ��� ������

    private DialogueLine[] dialogueLines;  // ��� ������ �迭
    private int currentLine = 0;           // ���� �����ְ� �ִ� ��� �ε���
    private bool isTyping = false;         // Ÿ���� �ڷ�ƾ�� ���� ������

    void Awake()
    {
        // �̱��� ���� ����: �̹� Instance�� ������ �ߺ� �ı�
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // �ܺο��� ��ȭ ���� ��û �� ȣ��
    public void StartDialog(DialogueLine[] lines)
    {
        dialogueLines = lines;     // ���޹��� ��� �迭 ����
        currentLine = 0;           // ù ��° ������ ����
        dialogPanel.SetActive(true); // ��ȭâ �г� Ȱ��ȭ
        ShowCurrentLine();         // ù ��� ǥ��
    }

    // ���� �ε����� ��縦 ȭ�鿡 ���
    private void ShowCurrentLine()
    {
        // ȭ�� �̸� ����
        speakerText.text = dialogueLines[currentLine].speaker;
        // ���� �ؽ�Ʈ�� Ÿ���� ȿ���� ���
        StopAllCoroutines();  // Ȥ�� �����ִ� �ڷ�ƾ ����
        StartCoroutine(TypeLine(dialogueLines[currentLine].text));
    }

    // ��� ���ڿ��� �� ���ھ� contentText�� �߰��ϴ� �ڷ�ƾ
    private IEnumerator TypeLine(string line)
    {
        isTyping = true;      // Ÿ���� �� �÷���
        contentText.text = ""; // �ʱ�ȭ
        foreach (char c in line)
        {
            contentText.text += c;             // �� ���� �߰�
            yield return new WaitForSeconds(textSpeed); // ������
        }
        isTyping = false;     // Ÿ���� �Ϸ�
    }

    void Update()
    {
        // ��ȭâ�� ���ų� �ı��� ��� ����
        if (dialogPanel == null) return;

        // ��ȭâ�� ���� �ְ� E Ű�� ������ ���� ��� ����
        if (dialogPanel.activeSelf && Input.GetKeyDown(KeyCode.E))
            AdvanceDialog();
    }

    // ��ȭ�� ���� �ٷ� �����ϰų� ����
    public void AdvanceDialog()
    {
        if (isTyping)
        {
            // Ÿ���� ���̸� ��� ��ü �ؽ�Ʈ ���
            StopAllCoroutines();
            contentText.text = dialogueLines[currentLine].text;
            isTyping = false;
        }
        else
        {
            // ���� ���� �̵�
            currentLine++;
            if (currentLine < dialogueLines.Length)
                ShowCurrentLine();  // ���� ��� ������ ��� ǥ��
            else
                EndDialog();        // ������ ��ȭ ����
        }
    }

    // ��ȭ ���� �� ȣ��
    private void EndDialog()
    {
        dialogPanel.SetActive(false); // ��ȭâ �г� ��Ȱ��ȭ
    }
}
