using UnityEngine;
using TMPro;
using System.Collections;

public class DialogManager : MonoBehaviour
{
    // 전역에서 접근 가능한 싱글톤 인스턴스
    public static DialogManager Instance { get; private set; }

    [Header("UI References")]
    public GameObject dialogPanel;   // 대화창 전체 패널
    public TMP_Text speakerText;     // 대화 상단에 표시할 화자 이름
    public TMP_Text contentText;     // 대사 내용을 한 글자씩 출력할 텍스트

    public float textSpeed = 0.05f;  // 한 글자당 출력 딜레이

    private DialogueLine[] dialogueLines;  // 대사 데이터 배열
    private int currentLine = 0;           // 현재 보여주고 있는 대사 인덱스
    private bool isTyping = false;         // 타이핑 코루틴이 진행 중인지

    void Awake()
    {
        // 싱글톤 패턴 설정: 이미 Instance가 있으면 중복 파괴
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // 외부에서 대화 시작 요청 시 호출
    public void StartDialog(DialogueLine[] lines)
    {
        dialogueLines = lines;     // 전달받은 대사 배열 저장
        currentLine = 0;           // 첫 번째 대사부터 시작
        dialogPanel.SetActive(true); // 대화창 패널 활성화
        ShowCurrentLine();         // 첫 대사 표시
    }

    // 현재 인덱스의 대사를 화면에 출력
    private void ShowCurrentLine()
    {
        // 화자 이름 설정
        speakerText.text = dialogueLines[currentLine].speaker;
        // 내용 텍스트는 타이핑 효과로 출력
        StopAllCoroutines();  // 혹시 남아있는 코루틴 정리
        StartCoroutine(TypeLine(dialogueLines[currentLine].text));
    }

    // 대사 문자열을 한 글자씩 contentText에 추가하는 코루틴
    private IEnumerator TypeLine(string line)
    {
        isTyping = true;      // 타이핑 중 플래그
        contentText.text = ""; // 초기화
        foreach (char c in line)
        {
            contentText.text += c;             // 한 글자 추가
            yield return new WaitForSeconds(textSpeed); // 딜레이
        }
        isTyping = false;     // 타이핑 완료
    }

    void Update()
    {
        // 대화창이 없거나 파괴된 경우 무시
        if (dialogPanel == null) return;

        // 대화창이 켜져 있고 E 키를 누르면 다음 대사 진행
        if (dialogPanel.activeSelf && Input.GetKeyDown(KeyCode.E))
            AdvanceDialog();
    }

    // 대화를 다음 줄로 진행하거나 종료
    public void AdvanceDialog()
    {
        if (isTyping)
        {
            // 타이핑 중이면 즉시 전체 텍스트 출력
            StopAllCoroutines();
            contentText.text = dialogueLines[currentLine].text;
            isTyping = false;
        }
        else
        {
            // 다음 대사로 이동
            currentLine++;
            if (currentLine < dialogueLines.Length)
                ShowCurrentLine();  // 남은 대사 있으면 계속 표시
            else
                EndDialog();        // 없으면 대화 종료
        }
    }

    // 대화 종료 시 호출
    private void EndDialog()
    {
        dialogPanel.SetActive(false); // 대화창 패널 비활성화
    }
}
