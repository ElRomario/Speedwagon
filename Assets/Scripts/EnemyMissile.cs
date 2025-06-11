using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : Missile
{
    [SerializeField] private float detonationDistance = 100f; // ���������� �� ������ ��� ���������
    private bool isDodged = false; // ���� �������
    private bool isMovingChaotically = false; // ���� ���������� ��������
    [SerializeField] private float chaosDuration = 3f; // ������������ ���������� ��������
    [SerializeField] private float chaosDirectionChangeInterval = 0.5f; // �������� ����� �����������
    [SerializeField] private float damage = 10f;
    private float chaosTimer; // ������ ���������� ��������
    public WarningTextController warningTextController;
    public GameObject hit;

    protected override void Start()
    {


        warningTextController = GameObject.FindGameObjectWithTag("MissileInd").GetComponent<WarningTextController>();
        // ��� ��������� ����� ������������� � ���������� ��� ������ ����������
        dropTime = 0; // ����� ������� ������ ����������
        speed = 130f; // �������� ������ ����������
        turnSpeed = 4f; // �������� �������� ������ ����������

        base.Start();
        RailPlaneController.OnBarrelRoll += OnPlayerBarrelRoll;
    }

    protected override void OnHitTarget(GameObject target)
    {
        // ������������� ��������� ��� ��������� ��� ������ ����������
        Debug.Log("Enemy missile hit " + target.name);
        base.OnHitTarget(target);
    }

    protected override void Update()
    {
        base.Update();

        if (isMovingChaotically)
        {
            ChaosMovement();
        }
        else if (target != null && !isDodged)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget <= detonationDistance)
            {
                Debug.Log("Missile is close to the player. Waiting for dodge...");
                warningTextController.StartBlinking();
            }
        }
    }

    private void OnPlayerBarrelRoll()
    {
        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget <= detonationDistance)
            {
                Debug.Log("Player performed barrel roll! Missile is dodged.");
                isDodged = true;
                StartChaosMovement();
                StartCoroutine(SelfDestructAfterDelay());
            }
        }
    }

    private System.Collections.IEnumerator SelfDestructAfterDelay()
    {
        // ������ ��������� � ����� (��������, ����������)
        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.y = 0; // ���������� ���������� �� ���������
        transform.rotation = Quaternion.LookRotation(randomDirection);

        // ����� 2 ������� ������ ������������
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private void StartChaosMovement()
    {
        isMovingChaotically = true;
        chaosTimer = chaosDuration; // ������������� ������������ ���������� ��������
        InvokeRepeating(nameof(ChangeChaosDirection), 0f, chaosDirectionChangeInterval); // ��������� ������ �����������
        warningTextController.StopBlinking();
    }

    private void OnDestroy()
    {
        RailPlaneController.OnBarrelRoll -= OnPlayerBarrelRoll; // ������� �� �������
    }

    private void ChaosMovement()
    {
        // ������� ���������� ����� ���������� ��������
        chaosTimer -= Time.deltaTime;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (chaosTimer <= 0)
        {
            StopChaosMovement();
        }
    }

    private void ChangeChaosDirection()
    {
        // �������� �������� ����������� ������
        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.y = 0; // ��������� ������������ �����������
        transform.rotation = Quaternion.LookRotation(randomDirection);
    }

    private void StopChaosMovement()
    {
        isMovingChaotically = false;
        CancelInvoke(nameof(ChangeChaosDirection)); // ������������� ����� �����������
        Destroy(gameObject); // ���������� ������
    }

    public float GetDamageAmount()
    {
        return damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���������, ���� �� � ������� ��������� Health
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            AudioManager.Instance.PlayExplosions();
            Instantiate(hit, transform.position, Random.rotationUniform);
            health.TakeDamage(GetDamageAmount());
            Destroy(gameObject); // ������� ���� ����� ���������
        }
    }

}

