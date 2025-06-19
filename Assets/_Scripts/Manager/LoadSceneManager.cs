using UnityEngine;

public class LoadSceneManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Scene2"); 
        }
    }
}
