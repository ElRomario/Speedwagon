using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float appearDuration = 3f; // �����, ������� ������ ����������� �������� ����� �����
    public float pathDuration = 10f; // ����� �������� �� ����
    public float pathSpeed = 3f; // �������� �������� �� ����
    public float moveSpeed = 5f; // �������� �������� � ������
    public Transform[] waypoints; // ����� ���� ��� ��������
    public Transform player; // ������ �� ������
    public float delayBeforeTargetingPlayer = 2f; // �������� ����� ��������� � ������

    private bool movingOnPath = false; // ���� ��� �������� �� ����
    private bool movingToPlayer = false; // ���� ��� �������� � ������
    private int currentWaypointIndex = 0;

    void Start()
    {
        // ����� ������
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // ��������� ��������� � ��������
        StartCoroutine(MoveThroughScreen());
    }

    IEnumerator MoveThroughScreen()
    {
        Vector3 startPosition = GetRandomAppearPosition();
        Vector3 endPosition = CalculateOffscreenPosition();

        float elapsedTime = 0f;

        while (elapsedTime < appearDuration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / appearDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;

        // ������ �������� �� ����
        StartMovingOnPath();
    }

    Vector3 GetRandomAppearPosition()
    {
        float randomX = Random.Range(-10f, 10f);
        float randomY = Random.Range(-6f, 6f);
        return new Vector3(randomX, randomY, 0f);
    }

    Vector3 CalculateOffscreenPosition()
    {
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        return new Vector3(screenBounds.x + 2f, transform.position.y, 0f);
    }

    void StartMovingOnPath()
    {
        movingOnPath = true;
        Invoke("StartTargetingPlayer", pathDuration);
    }

    void StartTargetingPlayer()
    {
        movingOnPath = false;
        movingToPlayer = true;
    }

    void Update()
    {
        if (movingOnPath)
        {
            MoveAlongPath();
        }
        else if (movingToPlayer)
        {
            MoveToPlayer();
        }
    }

    void MoveAlongPath()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, pathSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
    }

    void MoveToPlayer()
    {
        if (player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }
}
