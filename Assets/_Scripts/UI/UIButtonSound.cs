using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonSound : MonoBehaviour
{
    public AudioClip clickSfx;

    void Start()
    {
        // Lấy component Button và gắn sự kiện OnClick
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(() => {
            if (clickSfx != null && UIAudioManager.Instance != null)
            {
                UIAudioManager.Instance.PlaySound(clickSfx);
            }
        });
    }
}
