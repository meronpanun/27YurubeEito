using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject wallPrefab = null;
    [SerializeField, Min(0.1f)]
    float defaultMinWaitTime = 1;
    [SerializeField, Min(0.1f)]
    float defaultMaxWaitTime = 1;
    [SerializeField]
    Vector2 defaultMinSize = Vector2.one;
    [SerializeField]
    Vector2 defaultMaxSize = Vector2.one;
    bool isSpawning = false;
    float minWaitTime;
    float maxWaitTime;
    Vector2 minSize;
    Vector2 maxSize;
    Coroutine timer;
    //�O������l�������邽�߂̃v���p�e�B
    public float MinWaitTime
    {
        set
        {
            //���܂�ɂ��������l�ɂȂ�Ƃ��̂��������̏�Q������������Ă��܂��̂ŁA0.1�����ɂȂ�Ȃ��悤�ɂ���
            minWaitTime = Mathf.Max(value, 0.1f);
        }
        get
        {
            return minWaitTime;
        }
    }
    public float MaxWaitTime
    {
        set
        {
            maxWaitTime = Mathf.Max(value, 0.1f);
        }
        get
        {
            return maxWaitTime;
        }
    }
    public bool IsActive { get; set; } = true;
    public void Start()
    {
        InitSpawner();
    }
    public void Update()
    {
        if (!IsActive)
        {
            //�������Ȃ璆�f����
            if (timer != null)
            {
                StopCoroutine(timer);
                isSpawning = false;
            }
            return;
        }
        //����������Ȃ��Ȃ琶���J�n
        if (!isSpawning)
        {
            timer = StartCoroutine(nameof(SpawnTimer));
        }
    }
    //�������p���\�b�h
    public void InitSpawner()
    {
        minWaitTime = defaultMinWaitTime;
        maxWaitTime = defaultMaxWaitTime;
        minSize = defaultMinSize;
        maxSize = defaultMaxSize;
    }
    //�����������s���R���[�`��
    IEnumerator SpawnTimer()
    {
        isSpawning = true;
        
        GameObject wallObj = Instantiate(wallPrefab, transform.position, Quaternion.identity);
        Wall wall = wallObj.GetComponent<Wall>();
        float sizeX = Random.Range(minSize.x, maxSize.x);
        float sizeY = Random.Range(minSize.y, maxSize.y);
        wall.SetWall(new Vector2(sizeX, sizeY));
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(waitTime);
        isSpawning = false;
    }
}
