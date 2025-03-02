using UnityEngine;

public class OrganSpawner : MonoBehaviour
{
    [SerializeField] private OrganInfo[] organInfos;

    public void RandomizeOrgan()
    {
        foreach (var organInfo in organInfos)
        {
            float chance = Random.Range(0.0f, 1.0f);

            if(chance <= organInfo.spawnChance)
            {
                Instantiate(organInfo.organPrefab, organInfo.organPos.position, organInfo.organPos.rotation, organInfo.organPos);
            }
        }
    }
}

[System.Serializable]
public class OrganInfo
{
    public GameObject organPrefab;
    public Transform organPos;
    [Range(0, 1)] public float spawnChance;
}
