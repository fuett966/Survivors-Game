using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class SlowZone : AbstractDangerZone
{
    public float slowAmount = 0.5f; 

    public override void ApplyEffect(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.moveSpeed *= slowAmount;
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyEffect(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.moveSpeed /= slowAmount; 
            }
        }
    }
}
