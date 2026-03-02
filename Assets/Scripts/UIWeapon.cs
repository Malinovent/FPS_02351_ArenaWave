using UnityEngine;
using TMPro;

public class UIWeapon : MonoBehaviour
{
    [SerializeField] TMP_Text weaponText;
    [SerializeField] TMP_Text ammoRemainingText;
    [SerializeField] TMP_Text magazineRemainingText;
    [SerializeField] TMP_Text ammoMaxText;

    private void OnEnable()
    {
        WeaponUpdater.onWeaponUpdated += UpdateUI;
    }

    private void OnDisable()
    {
        WeaponUpdater.onWeaponUpdated -= UpdateUI;
    }

    private void UpdateUI(WeaponInfo weaponInfo)
    {
        this.weaponText.SetText(weaponInfo.weaponName);
        this.ammoMaxText.SetText(weaponInfo.ammoMax);
        this.ammoRemainingText.SetText(weaponInfo.ammoRemaining);
        this.magazineRemainingText.SetText(weaponInfo.magazineRemaining);
    }

    private void UpdateUI(string ammoRemaining, string ammoMax, string magazineRemaining, string weaponText)
    {
        this.weaponText.SetText(weaponText);
        this.ammoRemainingText.SetText(ammoRemaining);
        this.ammoMaxText.SetText(ammoMax);
        this.magazineRemainingText.SetText(magazineRemaining);
    }
}

public struct WeaponInfo
{
    public string weaponName;
    public string ammoRemaining;
    public string ammoMax;
    public string magazineRemaining;

    public WeaponInfo(string ammoRemaining, string ammoMax, string magazineRemaining, string weaponText)
    {
        this.weaponName = weaponText;
        this.ammoRemaining = ammoRemaining;
        this.ammoMax = ammoMax;
        this.magazineRemaining = magazineRemaining;
    }

}
