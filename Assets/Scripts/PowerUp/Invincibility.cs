using UnityEngine;

public class Invincibility : PowerUp
{
    public override void ApplyEffect(GameObject player)
    {
        Health health = player.GetComponent<Health>();
        if (health != null)
        {
            health.IsInvincible = true;
        }
    }

    public override void RemoveEffect(GameObject player)
    {
        Health health = player.GetComponent<Health>();
        if (health != null)
        {
            health.IsInvincible = false;
        }
    }
}
