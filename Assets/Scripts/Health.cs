using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] int onDestroyScore = 0;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool applyCameraShake = false;

    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    public int GetHealth() {  return health; }

    private void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            //take damage
            audioPlayer.PlayDamageClip(transform.position);
            int damage = damageDealer.GetDamage();
            health -= damage;
            int scoreIncrement = damage;
            if (health <= 0)
            {
                scoreIncrement += onDestroyScore;
                if (gameObject.tag == "Player")
                {
                    levelManager.LoadGameOver();
                    gameObject.SetActive(false);
                }
                else
                {
                    Destroy(gameObject);
                }
                
            }

            //Increment score if not the player
            if (gameObject.tag != "Player")
            {
                scoreKeeper.IncrementScore(scoreIncrement);
            }

            //tell damage dealer that it hit something
            damageDealer.Hit();

            PlayHitEffect();
            ShakeCamera();
        }
    }

    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera()
    {
        if(cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

}
