using UnityEngine;

public class AdminPanelCtrl : MonoBehaviour
{
    public Cor cors;

    private void Start()
    {
        Invoke(nameof(GetComponnetWhenStart), 0.3f);
    }

    private void GetComponnetWhenStart()
    {
        cors = FindAnyObjectByType<Cor>();

    }

    public void GetCor()
    {
        cors.IncreaseCor(100000);
    }
}
