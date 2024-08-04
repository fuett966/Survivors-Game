using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class PermanentDeathZone : AbstractDangerZone
{

    public override void ApplyEffect(GameObject player)
    {
        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.PermanentDeath();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyEffect(other.gameObject);
        }
    }
}
