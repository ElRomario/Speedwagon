using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Missile : MonoBehaviour
{
    [SerializeField] protected float dropTime; // �����, ���� ������ ����� "������"
    [SerializeField] protected float speed; // �������� ������
    [SerializeField] protected float turnSpeed; // �������� �������� � ����

    protected GameObject target; // ���� ������
    private bool isTracking = false; // ������ �� ������ ������������ ����
    private float dropTimer;
    public TrailRenderer trailRenderer;

    protected AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    protected virtual void Start()
    {
        dropTimer = dropTime;
    }

    protected virtual void Update()
    {
        if (dropTimer > 0)
        {
            
            // ������ "�������" ������
            dropTimer -= Time.deltaTime;
            trailRenderer.enabled = false;
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        else
        {
            isTracking = true;
        }

        if (isTracking && target != null)
        {
            TrackTarget();
        }
        else if (isTracking)
        {
            MoveForward();
        }
    }

    protected virtual void TrackTarget()
    {
        // ����������� � ����
        Vector3 direction = (target.transform.position - transform.position).normalized;
        // ������� � ����
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        // �������� �����
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        trailRenderer.enabled = true;
    }

    protected virtual void MoveForward()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���� ������ �������� � ����
        if (other.gameObject == target && other.gameObject.tag != "Player")
        {
            OnHitTarget(other.gameObject);
        }

    }

    protected virtual void OnHitTarget(GameObject target)
    {

        if (target.CompareTag("Turret"))
        {
            Destroy(target.transform.parent.gameObject);
        }
        else
        { 
            Destroy(target);
            Destroy(gameObject);
        }
    }
}