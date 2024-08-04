using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private int bulletCount = 5;
    [SerializeField] private float spreadAngle = 10f;
    [SerializeField] private float maxDistance = 7f;

    public override void Shoot()
    {
        if (fireCooldown <= 0)
        {
            {
                float angleStep = spreadAngle / (bulletCount - 1);
                float startAngle = -spreadAngle / 2;

                for (int i = 0; i < bulletCount; i++)
                {
                    float currentAngle = startAngle + i * angleStep;
                    Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, currentAngle, 0);
                    GameObject bullet = Instantiate(weaponData.bulletPrefab, firePoint.position, rotation);
                    ShotgunBullet bulletScript = bullet.GetComponent<ShotgunBullet>();
                    bulletScript.Initialize(weaponData.damage, weaponData.bulletSpeed,maxDistance);
                }
                fireCooldown = 1f / weaponData.fireRate;
            }
        }
    }
}
