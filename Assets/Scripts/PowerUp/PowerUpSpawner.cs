using System.Collections;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour, IPaused
{
    [SerializeField] private GameObject[] powerUpPrefabs;
    [SerializeField] private float spawnInterval = 27f;
    [SerializeField] private float powerUpDuration = 10f;

    private Camera mainCamera;

    public bool IsPaused { get ; set ; }

    private void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnPowerUps());
    }

    private IEnumerator SpawnPowerUps()
    {
        while (!IsPaused)
        {
            yield return new WaitForSeconds(spawnInterval);
            if (IsPaused)
            {
                continue;
            }
            Vector3 spawnPosition = GetRandomPositionInView();
            GameObject powerUpPrefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];
            GameObject powerUp = Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);

            Destroy(powerUp, powerUpDuration + 0.5f); 
        }
    }

    private Vector3 GetRandomPositionInView()
    {
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(0, 0, 0));
        plane.Raycast(ray, out distance);
        Vector3 lowerLeft = ray.GetPoint(distance);

        ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width, Screen.height, 0));
        plane.Raycast(ray, out distance);
        Vector3 upperRight = ray.GetPoint(distance);

        float minX = lowerLeft.x;
        float maxX = upperRight.x;
        float minZ = lowerLeft.z;
        float maxZ = upperRight.z;

        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);
        return new Vector3(x, 1, z); 
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
