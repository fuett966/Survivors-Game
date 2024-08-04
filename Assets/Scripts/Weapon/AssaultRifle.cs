using UnityEngine;

public class AssaultRifle : Weapon
{
    public override void Shoot()
    {
        if (fireCooldown <= 0)
        {
            GameObject bullet = Instantiate(weaponData.bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.Initialize(weaponData.damage, weaponData.bulletSpeed);
            fireCooldown = 1f / weaponData.fireRate;
        }
    }
}
