using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0,1)] float shootingVolume = 0.4f;

    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField][Range(0, 1)] float damageVolume = 0.7f;

    public void PlayShootingClip(Vector3 position)
    {
        if(shootingClip != null)
        {
            AudioSource.PlayClipAtPoint(shootingClip, position, shootingVolume);
        }
    }
    public void PlayDamageClip(Vector3 position)
    {
        if (shootingClip != null)
        {
            AudioSource.PlayClipAtPoint(damageClip, position, damageVolume);
        }
    }

}
