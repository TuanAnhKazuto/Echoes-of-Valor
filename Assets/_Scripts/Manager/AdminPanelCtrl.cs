using UnityEngine;

public class AdminPanelCtrl : MonoBehaviour
{
    public Cor cors;
    public PlayerExpManager expManager;

    private void Start()
    {
        Invoke(nameof(GetComponnetWhenStart), 0.3f);
    }

    private void GetComponnetWhenStart()
    {
        cors = FindAnyObjectByType<Cor>();
        expManager = FindAnyObjectByType<PlayerExpManager>();
    }

    public void GetCor(int count)
    {
        cors.IncreaseCor(count);
    }

    public void GetExp(int amount)
    {
        expManager.AddExp(amount);
    }

    public void GetLevel(int amount)
    {
        expManager.AddLevel(amount);
    }
}
