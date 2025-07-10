using UnityEngine;

public class PlaySoundOnState : StateMachineBehaviour
{
    public AudioClip clip;
    public float maxDistance = 15f; // Khoảng cách xa nhất còn nghe thấy
    private AudioSource source;
    private Transform player;
    private bool isRunning = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        source = animator.GetComponent<AudioSource>();
        if (source != null && clip != null)
        {
            source.clip = clip;
            source.loop = true;
            source.Play();
            isRunning = true;
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!isRunning || player == null || source == null) return;

        float distance = Vector3.Distance(animator.transform.position, player.position);
        float volume = Mathf.Clamp01(1 - (distance / maxDistance));
        source.volume = volume;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (source != null && source.clip == clip)
        {
            source.Stop();
            source.loop = false;
            source.clip = null;
        }

        isRunning = false;
    }
}
