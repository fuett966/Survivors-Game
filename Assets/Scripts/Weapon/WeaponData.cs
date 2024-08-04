using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public int damage;
    public float fireRate;
    public string description;
    public GameObject weaponPrefab;
    public GameObject bulletPrefab;
    public WeaponType weaponType;
    public float bulletSpeed;
}
public enum WeaponType
{
    GrenadeLauncher,
    Pistol,
    Rifle,
    Shotgun
}
