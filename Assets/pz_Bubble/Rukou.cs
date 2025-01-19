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

        // ��������ڣ�������Ԥ����
        if (existingObject == null)
        {
            Instantiate(prefab, spawnPosition, Quaternion.identity);
            Debug.Log("Ԥ���������ɣ�");
        }
        else
        {
            Debug.Log("�������Ѿ����ڸ�Ԥ���壬�����ظ����ɣ�");
        }
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space)) // ���¿ո����������
    //    {
    //        SpawnPrefab();
    //    }
    //}
}