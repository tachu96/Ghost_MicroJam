using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SelfDestroyParticle : MonoBehaviour
{
    public AudioClip[] randomClip;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        int randomIndex = Random.Range(0, randomClip.Length);
        audioSource.clip = randomClip[randomIndex];
        audioSource.Play();

        Destroy(gameObject, 2f);
    }
}
