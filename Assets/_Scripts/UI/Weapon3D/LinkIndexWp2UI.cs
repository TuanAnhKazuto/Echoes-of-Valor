using UnityEngine;

public class LinkIndexWp2UI : MonoBehaviour
{
    public GameObject baseWeapon;
    public GameObject lowWeapon;
    public GameObject highWeapon;

    public void ChangeWeaponUI(int index)
    {
        switch (index)
        {
            case 1:
                baseWeapon.SetActive(true);
                lowWeapon.SetActive(false);
                highWeapon.SetActive(false);
                break;
            case 2:
                baseWeapon.SetActive(false);
                lowWeapon.SetActive(true);
                highWeapon.SetActive(false);
                break;
            case 3:
                baseWeapon.SetActive(false);
                lowWeapon.SetActive(false);
                highWeapon.SetActive(true);
                break;
        }
    }
}
