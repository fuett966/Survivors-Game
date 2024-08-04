using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    public Transform firePoint;
    public float fireCooldown = 0f;


    public void SetData(WeaponData _weaponData)
    {
        weaponData = _weaponData;
    }

    public abstract void Shoot();

    protected virtual void Start()
    {
        fireCooldown = 1f / weaponData.fireRate;
    }
    protected void Update()
    {
        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }
    }
}
