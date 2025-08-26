using UnityEngine;
using UnityEngine.UI;

namespace SlimUI.ModernMenu
{
    public class CheckMusicVolume : MonoBehaviour
    {
        public Slider musicSlider;       // gán Slider Nhạc ở Inspector
        private AudioSource audioSource; // nhạc nền

        void Start()
        {
            audioSource = GetComponent<AudioSource>();

            // Lấy volume đã lưu, nếu chưa có thì mặc định = 1
            float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);

            audioSource.volume = savedVolume;

            if (musicSlider != null)
            {
                musicSlider.value = savedVolume;
                musicSlider.onValueChanged.AddListener(UpdateVolume);
            }
        }

        public void UpdateVolume(float value)
        {
            audioSource.volume = value;
            PlayerPrefs.SetFloat("MusicVolume", value); // lưu lại
        }
    }
}
