using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float fireRate = 0.2f;
    
    [Header("AI")]
    [SerializeField] bool useAI = false;
    [SerializeField] float fireRateVariance = 0f;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    bool isFiring = false;
    public void SetIsFiring(bool value) { isFiring = value; }

    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

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
            GameObject projectile = Instantiate(projectilePrefab,transform.position,Quaternion.identity);
            ProjectileMotion projectileMotion = projectile.GetComponent<ProjectileMotion>();
            if (projectileMotion != null) { projectileMotion.SetSpeed(Vector2.up * projectileSpeed); }
            Destroy(projectile,projectileLifetime);

            audioPlayer.PlayShootingClip(transform.position);

            yield return new WaitForSeconds(UnityEngine.Random.Range(fireRate- fireRateVariance,
                                                                     fireRate - fireRateVariance));
        }
    }
}
