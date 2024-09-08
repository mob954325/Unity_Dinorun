using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 팩토리가 생산하는 아이템 타입
/// </summary>
public enum FactoryType
{
    None,
    Cactus,
    Coin
}

public class FactoryBase : MonoBehaviour
{
    /// <summary>
    /// 팩토리 오브젝트 타입
    /// </summary>
    public FactoryType factoryType;

    /// <summary>
    /// 팩토리가 가지는 최초의 오브젝트 량
    /// </summary>
    public int amount = 2;

    /// <summary>
    /// 생성할 게임 오브젝트
    /// </summary>
    public GameObject product;

    /// <summary>
    /// 팩토리에서 관리할 오브젝트 리스트
    /// </summary>
    private List<GameObject> products;

    /// <summary>
    /// 소환할 준비가 된 오브젝트 배열
    /// </summary>
    private Queue<GameObject> readyQueue;

    // 초기화
    // 오브젝트 생성
    // 오브젝트 소환
    // 오브젝트 제거

    private void Awake()
    {
        products = new List<GameObject>();
        readyQueue = new Queue<GameObject>();

        Init();
    }

    private void Init()
    {
        products.Clear();
        readyQueue.Clear();

        for(int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(product, Vector3.zero, Quaternion.identity);

            products.Add(obj);
            readyQueue.Enqueue(obj);

            obj.transform.SetParent(this.transform);
            obj.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 팩토리에서 오브젝트를 소환하는 함수
    /// </summary>
    /// <param name="position">오브젝트 위치</param>
    /// <param name="rotate">오브젝트 회전</param>
    public void InstantiateProduct(Vector3? position = null, Quaternion? rotate = null)
    {
        CheckReadyQueue(); // 레디 큐 확인

        // 오브젝트 소환
        GameObject obj = readyQueue.Dequeue();
        obj.SetActive(true);

        obj.transform.position = position.GetValueOrDefault();
        obj.transform.rotation = rotate.GetValueOrDefault();

        IProduct curProduct = obj.GetComponent<IProduct>();

        if(curProduct != null)
        {
            curProduct.OnDeactive += () => { readyQueue.Enqueue(obj); }; // 비활성화 되면 레디큐에 추가
        }
    }

    /// <summary>
    /// 레디 큐에 오브젝트가 남아있는 지 확인하는 함수 (비어있으면 오브젝트를 현재량의 2배 생성)
    /// </summary>
    /// <returns>큐가 비어있으면 false 아니면 true</returns>
    private bool CheckReadyQueue()
    {
        bool result = false;

        if(readyQueue.Count > 0)
        {
            result = true;
        }
        else
        {
            for(int i = 0; i < amount; i++)
            {
                GameObject obj = Instantiate(product, Vector3.zero, Quaternion.identity);

                products.Add(obj);
                readyQueue.Enqueue(obj);

                obj.transform.SetParent(this.transform);
                obj.gameObject.SetActive(false);
            }

            Debug.LogWarning($"{this.gameObject.name} 용량 증가 : {amount} -> {amount * 2}");
            amount *= 2; // 용량 두 배씩 늘리기
        }

        return result;
    }
}