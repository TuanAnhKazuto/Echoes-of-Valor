using UnityEngine;

public class MageSource : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip attackSound;
    public AudioClip injuredSound;

    public void PlayAttackSound()
    {
        audioSource.PlayOneShot(attackSound);
    }

    public void PlayInuredSound()
    {
        audioSource.PlayOneShot(injuredSound);
    }
}
