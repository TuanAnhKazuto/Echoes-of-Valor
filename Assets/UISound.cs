using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UISound : MonoBehaviour
{
    public AudioClip clickSound; 
    private AudioSource audioSource;

    void Start()
    {

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

      
        GetComponent<Button>().onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
