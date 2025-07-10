using UnityEngine;

public class PlaySoundOnceWithDistance : StateMachineBehaviour
{
    [Header("Âm thanh")]
    public AudioClip clip;

    [Header("Khoảng cách tối đa nghe rõ")]
    public float maxDistance = 15f;

    [Header("Thời lượng phát (giây)")]
    public float duration = 0.75f;

    private AudioSource source;
    private Transform player;
    private float timer = 0f;
    private bool hasStarted = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        source = animator.GetComponent<AudioSource>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
            player = playerObj.transform;

        if (source != null && clip != null && player != null)
        {
            float distance = Vector3.Distance(animator.transform.position, player.position);
            float volume = Mathf.Clamp01(1 - (distance / maxDistance));

            source.clip = clip;
            source.volume = volume;
            source.loop = false;
            source.Play();
            hasStarted = true;
            timer = 0f;
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!hasStarted || source == null) return;

        timer += Time.deltaTime;
        if (timer >= duration)
        {
            source.Stop();
            hasStarted = false;
        }
    }
}
