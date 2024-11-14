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
        if (IsPaused)
        {
            return;
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (IsPaused)
        {
            return;
        }
        if (!other.TryGetComponent<IDamagable>(out var target))
            return;
        target.TakeDamage(damage);
        

        Destroy(gameObject);
    }

    public void OnPause()
    {
        IsPaused = true;
    }

    public void OnResume()
    {
        IsPaused = false;
    }
    void OnEnable()
    {
        PauseManager.OnGamePaused += OnPause;
        PauseManager.OnGameResumed += OnResume;

    }

    void OnDisable()
    {
        PauseManager.OnGamePaused -= OnPause;
        PauseManager.OnGameResumed -= OnResume;
    }
}
