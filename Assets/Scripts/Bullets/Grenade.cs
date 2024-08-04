using UnityEngine;

public class Grenade : Bullet
{
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private GameObject explosionPrefab;

    private Vector3 target;

    public void SetTarget(Vector3 targetPosition)
    {
        target = targetPosition;
    }

    protected override void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            Explode();
        }
    }
    protected override void OnTriggerEnter(Collider other)
    {
        
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayers);

        GameObject boom = Instantiate(explosionPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z),Quaternion.identity);
        Destroy(boom,0.4f);//pulling


        foreach (Collider nearbyObject in colliders)
        {
            IDamagable enemyHealth = nearbyObject.GetComponent<IDamagable>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
