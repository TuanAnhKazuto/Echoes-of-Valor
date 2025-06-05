using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class HitBoxController : MonoBehaviour
{
    public GameObject hitBox;
    public void OnAttack()
    {
        hitBox.SetActive(true);
    }

    public void EndAttack()
    {
        hitBox.SetActive(false);
    }
}
