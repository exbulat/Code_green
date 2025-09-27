using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public GameObject[] trashPrefabs;
    public RectTransform spawnArea;
    public float spawnInterval = 2f;
    public Transform canvasParent;

    private float timer;
    private bool isSpawning = true;

    void Update()
    {
        if (!isSpawning) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnTrash();
            timer = 0f;
        }
    }

    void SpawnTrash()
    {
        if (trashPrefabs.Length == 0 || spawnArea == null) return;

        Vector2 randomPos = new Vector2(
            Random.Range(spawnArea.rect.xMin, spawnArea.rect.xMax),
            Random.Range(spawnArea.rect.yMin, spawnArea.rect.yMax)
        );

        GameObject trash = Instantiate(
            trashPrefabs[Random.Range(0, trashPrefabs.Length)],
            canvasParent
        );

        RectTransform rt = trash.GetComponent<RectTransform>();
        if (rt != null)
        {
            // Для Overlay режима используем anchoredPosition
            rt.anchoredPosition = spawnArea.anchoredPosition + randomPos;
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }
}