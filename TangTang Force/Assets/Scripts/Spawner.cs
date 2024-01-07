using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData; //레벨마다 데이터 하나하나가 필요하니 배열로 함
    float timer;
    int level;
    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    private void Update()
    {
        timer += Time.deltaTime; //한 프레임당 계속 시간을더함 =>게임시간이 되는거임
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1); //인덱스에러는 Min함수로 사용해서 막기 가능
        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }
    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

//직렬화(Serialization) : 객체를 저장 혹은 전송하기 위해 변환
[System.Serializable] //속성부여
public class SpawnData //소환 데이터를 담당하는 클래스 선언
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
}
