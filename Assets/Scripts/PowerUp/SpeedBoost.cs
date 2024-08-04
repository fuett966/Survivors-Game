using UnityEngine;

public class SpeedBoost : PowerUp
{
    public float speedMultiplier = 1.5f;

    public override void ApplyEffect(GameObject player)
    {
        PlayerController controller = player.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.MultiplySpeed(speedMultiplier);
        }
    }

    public override void RemoveEffect(GameObject player)
    {
        PlayerController controller = player.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.DecreaseSpeed(speedMultiplier);
        }
    }
}
