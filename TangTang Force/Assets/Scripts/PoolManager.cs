using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;//프리펩들을 보관할 변수
    List<GameObject>[] pools;//풀담당을 하는 리스트들

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length]; //리스트 배열만 초기화한거임 초기화
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();//안에있는 각각 리스트도 초기화 해줘야함
        }
        Debug.Log(pools.Length);
    }

    public GameObject Get(int index) //선택한 풀의 놀고(비활성화된) 있는 게임오브젝트 접근, 발견하면 select변수에 할당. 못찾으면 새롭게 생성하고 select에 할당
    {
        GameObject select = null; //초기화
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        if (!select) //select=null랑 똑같은말
        {
            select = Instantiate(prefabs[index], transform); //부모객체 밑이 아니라, 내 자신에게 한다는뜻(풀매니저객체 밑으로 생길거임)
            pools[index].Add(select);
        }
        return select;
    }
}
