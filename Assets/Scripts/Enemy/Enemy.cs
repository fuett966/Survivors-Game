using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable, IPaused
{
    public float speed = 2f;
    private IHealth health;
    public int scoreValue = 10;
    private Transform player;

    public bool IsPaused { get; set; }

    private void Awake()
    {
        health = GetComponent<Health>();
    }
    public void TakeDamage(float damage)
    {
        health.Decrease(damage);

    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (IsPaused)
        {
            return;
        }
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void Die()
    {
        PlayerScore.instance.AddScore(scoreValue);
        Destroy(gameObject);//pull
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

        health.Died += Die;
    }

    void OnDisable()
    {
        PauseManager.OnGamePaused -= OnPause;
        PauseManager.OnGameResumed -= OnResume;

        health.Died -= Die;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(1f);
        }
    }
}
