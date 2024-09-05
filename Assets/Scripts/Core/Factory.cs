using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
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
        // 큐가 비어있으면 오브젝트 확장

        // 있으면 소환
        GameObject obj = readyQueue.Dequeue();
        obj.SetActive(true);

        obj.transform.position = position.Value;
        obj.transform.rotation = rotate.Value;

        IProduct curProduct = obj.GetComponent<IProduct>();

        if(curProduct != null)
        {
            curProduct.OnDeactive += () => { readyQueue.Enqueue(obj); }; // 비활성화 되면 레디큐에 추가
        }
    }
}