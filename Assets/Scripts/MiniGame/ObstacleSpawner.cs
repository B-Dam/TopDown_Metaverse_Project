using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("������ ��ֹ� �����յ�")]
    public GameObject[] obstacles;
    // ������ ��ֹ� �����յ��� �迭�� �Ҵ�

    [Header("�ð� ����� ���� ���� ����")]
    public float initialInterval = 2f;
    // ���� ���� ���� ù ���������� �ð�
    public float minInterval = 0.8f;
    // ���� ������ �ּ������� �پ�� �� �ִ� �Ѱ�
    public float intervalDecay = 0.01f;
    // ��� �ð� 1�� ���� ���� ������ �󸶸�ŭ �پ�� ������

    private float currentInterval;
    // ���� ���� ���� ��
    private float timeSinceStart;
    // ���� ��ƾ�� ���۵� ���� ������ �ð�

    [Header("���� ��ġ")]
    public Transform spawnPoint;
    // ��ֹ��� ������ ���� ��ǥ(Transform)

    private void Start()
    {
        // �ʱ� ����: ���� ���ݰ� ���� �ð� �ʱ�ȭ
        currentInterval = initialInterval;
        timeSinceStart = 0f;
        // ��ֹ� ���� �ڷ�ƾ ����
        StartCoroutine(SpawnRoutine());
    }

    // ��ֹ��� �ֱ������� �����ϴ� �ڷ�ƾ
    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // 1) �������� ��ֹ� ������ ����
            var prefab = obstacles[Random.Range(0, obstacles.Length)];
            // 2) ���õ� �������� spawnPoint ��ġ�� ����
            Instantiate(prefab, spawnPoint.position, Quaternion.identity);

            // 3) ���� ������ ���ݸ�ŭ ���
            yield return new WaitForSeconds(currentInterval);

            // 4) ��� �ð� ����
            timeSinceStart += currentInterval;
            // 5) ���� �ð��� ���� ���� ���� ���� (�ּҰ� ����)
            currentInterval = Mathf.Max(
                minInterval,
                initialInterval - timeSinceStart * intervalDecay
            );
        }
    }
}
