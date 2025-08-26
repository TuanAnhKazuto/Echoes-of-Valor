using UnityEngine;

public class AdminPanelCtrl : MonoBehaviour
{
    public Cor cors;
    public PlayerExpManager playerExpManager;

    private void Start()
    {
        Invoke(nameof(GetComponnetWhenStart), 0.3f);
    }

    private void GetComponnetWhenStart()
    {
        cors = FindAnyObjectByType<Cor>();
        playerExpManager = FindAnyObjectByType<PlayerExpManager>();

    }

    public void GetCor()
    {
        cors.IncreaseCor(100000);
    }

    public void GetExp(int amount)
    {
        playerExpManager.AddExp(amount);
    }

    public void GetLevel(int amount)
    {
        playerExpManager.AddLevel(amount);
    }
}
