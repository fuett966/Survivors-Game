using UnityEngine;

public class RocketLauncher : Weapon
{
    public override void Shoot()
    {
        if (fireCooldown <= 0)
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           
            target.y = 1f;

            GameObject grenade = Instantiate(weaponData.bulletPrefab, firePoint.position, firePoint.rotation);
            Grenade grenadeScript = grenade.GetComponent<Grenade>();
            grenadeScript.Initialize(weaponData.damage, weaponData.bulletSpeed);
            if (grenadeScript != null)
            {
                grenadeScript.SetTarget(target);
            }
            fireCooldown = 1f / weaponData.fireRate;
        }
    }
}
