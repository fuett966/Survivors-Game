using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBonusSpawner : MonoBehaviour, IPaused
{
    [SerializeField] private WeaponData[] weaponDataArray;
    [SerializeField] private GameObject weaponBonusPrefab;
    [SerializeField] private PlayerController player;
    [SerializeField] private float spawnInterval = 10f;
    private Camera mainCamera;

    public bool IsPaused { get; set; }

    private void Start()
    {
        mainCamera = Camera.main;
        player = FindObjectOfType<PlayerController>();
        StartCoroutine(SpawnWeaponBonuses());
    }

    private IEnumerator SpawnWeaponBonuses()
    {
        while (!IsPaused)
        {
            yield return new WaitForSeconds(spawnInterval);

            if(IsPaused)
            {
                continue;
            }
            Vector3 spawnPosition = GetRandomPositionInView();
            GameObject weaponBonus = Instantiate(weaponBonusPrefab, spawnPosition, Quaternion.identity);

            WeaponData selectedWeapon = GetRandomWeaponExcludingCurrent();
            weaponBonus.GetComponent<WeaponBonus>().weaponData = selectedWeapon;

            Destroy(weaponBonus, 10f);
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

    private WeaponData GetRandomWeaponExcludingCurrent()
    {
        List<WeaponData> availableWeapons = new List<WeaponData>(weaponDataArray);
        availableWeapons.RemoveAll(w => w.weaponName == player.CurrentWeapon.weaponData.weaponName);
        return availableWeapons[Random.Range(0, availableWeapons.Count)];
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
