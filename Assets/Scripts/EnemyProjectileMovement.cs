using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    public float damage = 10f;
    public GameObject hit;

    // ������������� ����������� � ��������
    public void SetDirection(Vector3 dir, float spd)
    {
        direction = dir;
        speed = spd;
    }

    void Update()
    {
        // ������� ���� �� �����������
        transform.position += direction * speed * Time.deltaTime;
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
            Instantiate(hit, transform.position, Random.rotationUniform);
            health.TakeDamage(GetDamageAmount());
            Destroy(gameObject); // ������� ���� ����� ���������
        }
    }
}
