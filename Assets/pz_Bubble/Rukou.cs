using UnityEngine;

public class Rukou : MonoBehaviour
{
    public GameObject prefab;
    public Transform target;
    private Vector3 spawnPosition;

    public string prefabTag = "Points";
    private void Awake()
    {
        SpawnPrefab();
    }
    public void SpawnPrefab()
    {
        spawnPosition = target.position;

        GameObject existingObject = GameObject.FindWithTag(prefabTag);

        // 如果不存在，则生成预制体
        if (existingObject == null)
        {
            Instantiate(prefab, spawnPosition, Quaternion.identity);
            Debug.Log("预制体已生成！");
        }
        else
        {
            Debug.Log("场景中已经存在该预制体，不会重复生成！");
        }
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space)) // 按下空格键触发生成
    //    {
    //        SpawnPrefab();
    //    }
    //}
}