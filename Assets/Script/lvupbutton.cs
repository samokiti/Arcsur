using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class lvupbutton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMP_Text weaponname;
    public TMP_Text weapondescription;
    public Image weaponicon;

    private Weapon assignedweapon;

    public void Activatbutton(Weapon weapon)
    {
        weaponname.text = weapon.name;
        weapondescription.text = weapon.stats[weapon.weaponlevel].descprition;
        weaponicon.sprite = weapon.weaponimage;

        assignedweapon = weapon;
    }

    public void SelectUpgrade()
    {
        assignedweapon.Levelup();
        UIController.Instance.Leveluppannelclose();
    }
}
