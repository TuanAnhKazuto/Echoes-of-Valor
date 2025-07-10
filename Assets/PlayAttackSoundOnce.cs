using UnityEngine;

public class PlayAttackSoundOnce : StateMachineBehaviour
{
    [Header("Âm thanh khi tấn công")]
    public AudioClip clip;

    [Header("Khoảng cách tối đa nghe thấy")]
    public float maxDistance = 12f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (clip == null) return;

        AudioSource source = animator.GetComponent<AudioSource>();
        if (source == null) return;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null) return;

        float distance = Vector3.Distance(animator.transform.position, playerObj.transform.position);
        float volume = Mathf.Clamp01(1 - (distance / maxDistance));

        source.PlayOneShot(clip, volume);
    }
}
