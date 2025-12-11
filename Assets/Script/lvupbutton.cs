using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum UpgradeType
{
    Attack,
    Speed,
    Heal
}

public class lvupbutton : MonoBehaviour
{
    public TMP_Text weaponname;
    public TMP_Text weapondescription;
    public Image weaponicon;

    public UpgradeType upgradeType;

    public void SelectUpgrade()
    {
        Controller.Instance.ApplyUpgrade(upgradeType);
        UIController.Instance.Leveluppannelclose();
    }
    public void SetupButton(UpgradeType type, string title, string desc)
    {
        upgradeType = type;

        // *** แก้ตรงนี้ครับ ให้ชื่อตรงกับด้านบน ***
        if (weaponname != null)
        {
            weaponname.text = title;
        }

        if (weapondescription != null)
        {
            weapondescription.text = desc;
        }
    }
}