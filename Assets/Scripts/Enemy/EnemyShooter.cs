using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = -6f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float fireRate = 2f;
    [SerializeField] float fireRateVariance = 0.2f;

    //[Header("Projectile Type 2")]
    //[SerializeField] GameObject type2Prefab;
    //[SerializeField] float type2Speed = 4f;
    //[SerializeField] float type2Lifetime = 2f;
    //[SerializeField] float type2FireRate = 2f;
    //[SerializeField] float type2RateVariance = 0.2f;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    // serialize field is here for testing/debug
    [SerializeField] bool isFiring = true;

    void Update()
    {
        Firing();
    }

    void Firing()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position,Quaternion.identity);
            switch (tag)
            {
                case "Enemy1":
                    ProjectileEnemy1 projMotion1 = projectile.GetComponent<ProjectileEnemy1>();
                    if (projMotion1 != null) { projMotion1.SetSpeed(projectileSpeed); }
                    break;
                case "Enemy2":
                    ProjectileEnemy2 projMotion2 = projectile.GetComponent<ProjectileEnemy2>();
                    if (projMotion2 != null) { projMotion2.SetSpeed(projectileSpeed); }
                    break;
                case "Enemy3":
                    //ProjectileEnemy3 projMotion3 = projectile.GetComponent<ProjectileEnemy3>();
                    break;
                case "Enemy4":
                    //ProjectileEnemy4 projMotion4 = projectile.GetComponent<ProjectileEnemy4>();
                    break;
            }
            

            

            Destroy(projectile,projectileLifetime);

            audioPlayer.PlayShootingClip(transform.position);

            yield return new WaitForSeconds(UnityEngine.Random.Range(fireRate - fireRateVariance,
                                                                     fireRate + fireRateVariance));
        }
    }
}
