using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    public static InteractionSystem Instance { get; private set; }

    // �÷��̾ ���� ���� �ִ� ��ȣ�ۿ� Ʈ����
    private InteractionTrigger activeTrigger;

    void Awake()
    {
        // �̱��� ���� ����: ���� �ν��Ͻ��� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // �� ��ȯ �� �ı� ����
        }
        else
        {
            Destroy(gameObject);  // �ߺ� ������ ��� ����
        }
    }

    void Update()
    {
        // ��ȣ�ۿ� ������ Ʈ���Ű� �ְ�, E Ű�� ������ ��
        if (activeTrigger != null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("[InteractionSystem] E Ű �Է� ���� �� DoInteract ȣ��");
            // ���� Ȱ��ȭ�� Ʈ������ ��ȣ�ۿ� �޼��� ����
            activeTrigger.DoInteract();
        }
    }

    // �÷��̾ Ʈ���� ������ ������ �� InteractionTrigger���� ȣ��
    public void SetActiveTrigger(InteractionTrigger trigger)
    {
        activeTrigger = trigger;
        // TODO: ȭ�鿡 'EŰ�� ���� ��ȣ�ۿ�' ���� ��Ʈ UI ǥ��
    }

    // �÷��̾ Ʈ���� ������ ��� �� InteractionTrigger���� ȣ��
    public void ClearActiveTrigger(InteractionTrigger trigger)
    {
        // �������� ��� Ʈ���Ű� ���� Ȱ��ȭ�� �Ͱ� ������ ����
        if (activeTrigger == trigger)
            activeTrigger = null;
        // TODO: ��ȣ�ۿ� ��Ʈ UI �����
    }
}
