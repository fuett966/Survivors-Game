using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShotgunBullet : Bullet
{
    [SerializeField]private float maxDistance = 7f;

    private Vector3 startPosition;

    public void Initialize(int _damage, float _speed,float _maxDistance)
    {
        maxDistance = _maxDistance;
        Initialize(_damage, _speed);
    }

    protected override void Start()
    {
        startPosition = transform.position;
    }

    protected override void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        IDamagable enemyHP = other.GetComponent<IDamagable>();
        if (enemyHP != null)
        {
            enemyHP.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
