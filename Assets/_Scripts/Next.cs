using UnityEngine;
using UnityEngine.SceneManagement;

public class Next : MonoBehaviour
{
    public void NextBtn()
    {
        SceneManager.LoadScene("Scene1");
    }
}
