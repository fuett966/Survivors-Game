using UnityEngine;

public class Bullet : MonoBehaviour, IPaused
{
    public float speed = 20f;
    public int damage = 10;
    public LayerMask enemyLayers;

    public bool IsPaused { get ; set ; }

    public virtual void Initialize(int _damage, float _speed)
    {
        speed = _speed;
        damage = _damage;
    }

    protected virtual void Start()
    {
        Destroy(gameObject,5f);
    }

    protected virtual void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<IDamagable>(out var target))
            return;
        target.TakeDamage(damage);
        

        Destroy(gameObject);
    }

    public void OnPause()
    {
        throw new System.NotImplementedException();
    }

    public void OnResume()
    {
        throw new System.NotImplementedException();
    }
}
