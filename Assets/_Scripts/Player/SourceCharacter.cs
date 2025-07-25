using UnityEngine;

public class SourceCharacter : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip runSound;
    public AudioClip walkSound;
    public AudioClip injuredSound;
    public AudioClip dieSound;
    public AudioClip jumpSound;

    public AudioClip normalKnight;
    public AudioClip skill1Knight;

    public AudioClip normalMage;
    public AudioClip skill2Mage;

    public AudioClip normalRouge;

    public void PlayRunSound()
    {
        if (audioSource.clip != runSound)
        {
            audioSource.clip = runSound;
        }

        if (!audioSource.isPlaying)
        {
            audioSource.loop = true;
            audioSource.Play();
        }
    }
    public void StopRunSound()
    {
        if (audioSource.isPlaying && audioSource.clip == runSound)
        {
            audioSource.Stop();
            audioSource.loop = false;
            audioSource.clip = null;
        }
    }

    public void PlayDieSound()
    {
        audioSource.PlayOneShot(dieSound);
    }
    public void PlayInuredSound()
    {
        audioSource.PlayOneShot(injuredSound);
    }
    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }
    // Knight
    public void PlayNomalKnght()
    {
        audioSource.PlayOneShot(normalKnight);
    }
    public void PlaySkill1Knght()
    {
        audioSource.PlayOneShot(skill1Knight);
    }
    // Mage
    public void PlayNormalMage()
    {
        audioSource.PlayOneShot(normalMage);
    }
    public void PlaySkill2Mage()
    {
        audioSource.PlayOneShot(skill2Mage);
    }

    //Rouge
    public void PlayNormalRouge()
    {
        audioSource.PlayOneShot(normalRouge);
    }
}
