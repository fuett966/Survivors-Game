using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPaused,IDamagable
{
    public float moveSpeed = 4f;
    public float rotationSpeed = 180f;

    public Transform weaponHolder;

    private Camera mainCamera;
    private CharacterController characterController;

    public WeaponData startWeapon;
    public Weapon currentWeapon;

    private IHealth health;


    public bool IsPaused { get; set; }

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

    private void Awake()
    {
        ChangeWeapon(startWeapon);
        characterController = GetComponent<CharacterController>();
        health = GetComponent<IHealth>();    
        mainCamera = Camera.main;

    }

    private void Start()
    {

    }

    private void Update()
    {
        if (!IsPaused)
        {
            Move();
            Rotate();
            Shoot();
        }
    }

    private void Move()
    {
        Vector3 forward = mainCamera.transform.up;
        Vector3 right = mainCamera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 moveDirection = (forward * moveY + right * moveX).normalized;
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(ray, out rayLength))
        {
            Vector3 pointToLook = ray.GetPoint(rayLength);
            Vector3 direction = (pointToLook - transform.position).normalized;

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }


    private void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            currentWeapon.Shoot();
        }
    }

    public void ChangeWeapon(WeaponData _weaponData)
    {
        if (currentWeapon != null)
        {   
            currentWeapon.fireCooldown = 1f;
            Destroy(currentWeapon.gameObject);
        }
        GameObject newWeapon = Instantiate(_weaponData.weaponPrefab, weaponHolder);
        currentWeapon = newWeapon.GetComponent<Weapon>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PowerUp powerUp = other.GetComponent<PowerUp>();
        if (powerUp != null)
        {
            StartCoroutine(ApplyPowerUp(powerUp));
        }
    }

    private IEnumerator ApplyPowerUp(PowerUp powerUp)
    {
        PowerUp _powerUp = powerUp;
        _powerUp.ApplyEffect(gameObject);
        Destroy(powerUp.gameObject);
        yield return new WaitForSeconds(_powerUp.duration);
        _powerUp.RemoveEffect(gameObject);
        _powerUp = null;
    }

    public void MultiplySpeed(float speedMultiplier)
    {
        moveSpeed *= speedMultiplier;
    }

    public void DecreaseSpeed(float speedMultiplier)
    {
        moveSpeed /= speedMultiplier;
    }

    public void OnPause()
    {
        IsPaused = true;
    }

    public void OnResume()
    {
        IsPaused = false;
    }

    public void TakeDamage(float damage)
    {
        health.Decrease(damage);
    }
    private void Die()
    {
        PlayerScore.instance.FinishGame();
        Destroy(gameObject);
    }
}
