using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public float duration = 10f;

    public abstract void ApplyEffect(GameObject player);
    public abstract void RemoveEffect(GameObject player);
}
