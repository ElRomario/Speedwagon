using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract class MissileShooter : MonoBehaviour
{

    public GameObject missilePrefab; 
    public Transform[] missileSpawnPoints; 
    public float fireRate = 1f; 
    protected float nextFireTime;

   
    protected abstract List<GameObject> GetTargets();

    // Выстрел ракет
    public virtual void FireMissiles()
    {
        if (Time.time < nextFireTime) return;

        nextFireTime = Time.time + 1f / fireRate;

        List<GameObject> targets = GetTargets();

        if (targets == null || targets.Count == 0) return;
        
        
        Queue<GameObject> targetQueue = new Queue<GameObject>(targets);
            if (CompareTag("Player"))
                 {
                  MissileUIManager.Instance.MissileCount--;
                 }
        
        foreach (Transform spawnPoint in missileSpawnPoints)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.missileDropDowm);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.missleLaunch);
            GameObject missile = Instantiate(missilePrefab, spawnPoint.position, spawnPoint.rotation);

            
            GameObject target = targetQueue.Count > 0 ? targetQueue.Dequeue() : null;
            missile.GetComponent<Missile>().SetTarget(target);

            
            if (target != null) targetQueue.Enqueue(target);

           
            
        }
        
        Debug.Log("Missiles were shot!");
    }
}
