using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct ZoneData
{
    public GameObject zonePrefab;
    public int numberOfZones; 
}
public class ZoneGenerator : MonoBehaviour
{
    public List<ZoneData> zoneDataList; // Список данных о зонах
    public float minDistanceBetweenZones = 3f; // Минимальное расстояние между зонами
    public float minDistanceFromEdge = 3f; // Минимальное расстояние от краев карты

    // Границы карты
    public Vector2 minBounds;
    public Vector2 maxBounds;

    private List<Vector3> generatedZones = new List<Vector3>();

    private void Start()
    {
        GenerateZones();
    }

    private void GenerateZones()
    {
        int attempts = 0;
        foreach (ZoneData zoneData in zoneDataList)
        {
            int generatedCount = 0;
            while (generatedCount < zoneData.numberOfZones && attempts < 10000)
            {
                Vector3 randomPosition = GetRandomPosition();
                if (IsValidPosition(randomPosition))
                {
                    Instantiate(zoneData.zonePrefab, randomPosition, Quaternion.identity);
                    generatedZones.Add(randomPosition);
                    generatedCount++;
                }
                attempts++;
            }

            if (generatedCount < zoneData.numberOfZones)
            {
                Debug.LogWarning($"Не удалось сгенерировать все зоны типа {zoneData.zonePrefab.name}. Попробуйте уменьшить количество зон или увеличить размер карты.");
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(minBounds.x + minDistanceFromEdge, maxBounds.x - minDistanceFromEdge);
        float z = Random.Range(minBounds.y + minDistanceFromEdge, maxBounds.y - minDistanceFromEdge);
        return new Vector3(x, 0, z);
    }

    private bool IsValidPosition(Vector3 position)
    {
        foreach (Vector3 generatedPosition in generatedZones)
        {
            if (Vector3.Distance(position, generatedPosition) < minDistanceBetweenZones)
            {
                return false;
            }
        }
        return true;
    }
}
