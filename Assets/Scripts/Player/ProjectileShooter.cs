using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{

    public GameObject projectilePrefab; 
    public Transform shootPoint; 
    public Vector3 spawnOffset = Vector3.zero; 
    public float projectileSpeed = 20f; 
    public float projectileLifetime = 5f;
    public float fireRate = 0.2f;
    private bool isShooting = false;
    private Coroutine shootCoroutine;

    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        shootPoint = GameObject.FindGameObjectWithTag("ShootPoint").GetComponent<Transform>();
    }

    void Update()
    {

        if (Input.GetButtonDown("Fire1")) 
        {
            isShooting = true;
            shootCoroutine = StartCoroutine(ShootContinuously());
        }
        if (Input.GetButtonUp("Fire1")) 
        {
            isShooting = false;
            if (shootCoroutine != null)
                StopCoroutine(shootCoroutine);
        }
    }

    IEnumerator ShootContinuously()
    {
        while(isShooting)
        {
            ShootProjectile();
            yield return new WaitForSeconds(fireRate);
        }
    }

    void ShootProjectile()
    {
        audioManager.PlaySFX(audioManager.laserShoot);
        if (projectilePrefab == null || shootPoint == null)
        {
            Debug.LogWarning("Не задан projectilePrefab или shootPoint!");
            return;
        }

        
        Vector3 spawnPosition = shootPoint.position + shootPoint.TransformDirection(spawnOffset);

        
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, shootPoint.rotation);

        
        ProjectileBehavioiur projectileScript = projectile.GetComponent<ProjectileBehavioiur>();
        if (projectileScript != null)
        {
            projectileScript.speed = projectileSpeed;
            projectileScript.lifetime = projectileLifetime;
            projectileScript.SetDirection(shootPoint.forward); 
        }
        else
        {
            Debug.LogWarning("У снаряда отсутствует компонент Projectile!");
        }
    }
}
