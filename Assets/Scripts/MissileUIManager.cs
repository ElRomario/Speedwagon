using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileUIManager : MonoBehaviour
{

   
        public static MissileUIManager Instance { get; private set; }
        public Text missileText;

        private int missileCount;
        public int MaxMissiles = 4;
        public float ReloadTime = 2f; 

        private Coroutine reloadCoroutine;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
        if (GameManager.Instance.missilesSelected)
            MaxMissiles = 100;

            missileCount = MaxMissiles;
            UpdateMissileUI();
        }

        public int MissileCount
        {
            get { return missileCount; }
            set
            {
                missileCount = Mathf.Clamp(value, 0, MaxMissiles);
                UpdateMissileUI();

                
                if (reloadCoroutine == null && missileCount < MaxMissiles)
                {
                    reloadCoroutine = StartCoroutine(ReloadMissiles());
                }
            }
        }

        private IEnumerator ReloadMissiles()
        {
            while (missileCount < MaxMissiles)
            {
                yield return new WaitForSeconds(ReloadTime);
                missileCount++;
                UpdateMissileUI();
            }
            reloadCoroutine = null; 
        }

        private void UpdateMissileUI()
        {
            if (missileText != null)
            {
                missileText.text = missileCount.ToString();
            }
        }
    }
