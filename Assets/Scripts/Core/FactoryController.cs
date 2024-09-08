using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnType
{
    None,
    Jump
}

public class FactoryController : MonoBehaviour
{
    public FactoryBase[] cactusFactories;
    public FactoryBase coinFactroy;

    /// <summary>
    /// 지상에서 스폰할 위치 트랜스폼
    /// </summary>
    private Transform groundSpawnPoint;

    /// <summary>
    /// 공중에서 스폰할 위치 트랜스폼
    /// </summary>
    private Transform skySpawnPoint;

    /// <summary>
    /// 스폰 딜레이 시간 (1f)
    /// </summary>
    public float spawnDelay = 0.25f;

    /// <summary>
    /// 스폰 타이머
    /// </summary>
    private float spawnTimer = 0f;

    /// <summary>
    /// 스폰 타이머 접근 및 수정용 프로퍼티
    /// </summary>
    private float SpawnTimer
    {
        get => spawnTimer;
        set
        {
            spawnTimer = value;

            if(spawnTimer > spawnDelay) // 스폰 딜레이 시간이 지나면 오브젝트 생성
            {
                if(SpawnCactus())
                {
                    SpawnCoin(false);
                }
                else
                {
                    SpawnCoin(true);
                }

                spawnTimer = 0f;
            }
        }
    }

    private void Awake()
    {
        int index = 0;
        groundSpawnPoint = transform.GetChild(index++);
        skySpawnPoint = transform.GetChild(index++);

        cactusFactories = new FactoryBase[3];

        for(int i = 0; i < cactusFactories.Length; ++i)
        {
            cactusFactories[i] = transform.GetChild(index++).GetComponent<FactoryBase>();
        }

        coinFactroy = transform.GetChild(index++).GetComponent<FactoryBase>();
    }

    private void FixedUpdate()
    {
        SpawnTimer += Time.fixedDeltaTime;
    }

    /// <summary>
    /// 랜덤으로 선인장을 소환하는 함수
    /// </summary>
    /// <returns>소환했으면 true 아니면 false</returns>
    private bool SpawnCactus()
    {
        float spawn = Random.value;

        if(spawn > 0.5f) // 50% 확률로 생성
        {
            int rand = Random.Range(0, cactusFactories.Length);
            cactusFactories[rand].InstantiateProduct(groundSpawnPoint.position);
            return true;
        }

        return false;
    }

    /// <summary>
    /// 코인을 스폰하는 함수
    /// </summary>
    /// <param name="isGround">스폰지점이 땅이면 true 아니면 false</param>
    private void SpawnCoin(bool isGround)
    {
        if (isGround)
        {
            coinFactroy.InstantiateProduct(groundSpawnPoint.position);
        }
        else
        {
            coinFactroy.InstantiateProduct(skySpawnPoint.position);
        }        
    }
}