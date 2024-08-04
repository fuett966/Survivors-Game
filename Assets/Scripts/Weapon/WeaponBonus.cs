using UnityEngine;

public class WeaponBonus : MonoBehaviour
{
    public WeaponData weaponData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.ChangeWeapon(weaponData);
            Destroy(gameObject);
        }
    }
}
