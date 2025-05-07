using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("생성할 장애물 프리팹들")]
    public GameObject[] obstacles;
    // 스폰할 장애물 프리팹들을 배열로 할당

    [Header("시간 경과에 따른 스폰 간격")]
    public float initialInterval = 2f;
    // 게임 시작 직후 첫 스폰까지의 시간
    public float minInterval = 0.8f;
    // 스폰 간격이 최소한으로 줄어들 수 있는 한계
    public float intervalDecay = 0.01f;
    // 경과 시간 1에 대해 스폰 간격이 얼마만큼 줄어들 것인지

    private float currentInterval;
    // 현재 스폰 간격 값
    private float timeSinceStart;
    // 스폰 루틴이 시작된 이후 누적된 시간

    [Header("스폰 위치")]
    public Transform spawnPoint;
    // 장애물을 생성할 월드 좌표(Transform)

    private void Start()
    {
        // 초기 설정: 스폰 간격과 누적 시간 초기화
        currentInterval = initialInterval;
        timeSinceStart = 0f;
        // 장애물 생성 코루틴 시작
        StartCoroutine(SpawnRoutine());
    }

    // 장애물을 주기적으로 생성하는 코루틴
    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // 1) 랜덤으로 장애물 프리팹 선택
            var prefab = obstacles[Random.Range(0, obstacles.Length)];
            // 2) 선택된 프리팹을 spawnPoint 위치에 생성
            Instantiate(prefab, spawnPoint.position, Quaternion.identity);

            // 3) 현재 설정된 간격만큼 대기
            yield return new WaitForSeconds(currentInterval);

            // 4) 경과 시간 누적
            timeSinceStart += currentInterval;
            // 5) 누적 시간에 따라 스폰 간격 감소 (최소값 보장)
            currentInterval = Mathf.Max(
                minInterval,
                initialInterval - timeSinceStart * intervalDecay
            );
        }
    }
}
