using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;
    bool isLive;
    Rigidbody2D rigid;
    SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        isLive = true;
    }
    //몬스터가 주인공 따라가게 하는 로직
    void FixedUpdate()
    {
        if (!isLive) return;//몬스터가 살아있는 동안에만 따라가게

        Vector2 dirVec = target.position - rigid.position; //위치차이.그러나 방향의 크기가 1이 아니니까 normalized써야함
        //다음에 가야할 위치의 양
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; //프레임의 영향으로 결과가 달라지지 않도록 FixedDeltaTime사용
        rigid.MovePosition(rigid.position + nextVec); //플레이어의 키입력을 더한 이동 = 몬스터의 방향값을 더한 이동
        rigid.velocity = Vector2.zero; //물리속도가 이동에 영향을 주지 않도록 속도 제거 
    }
    void LateUpdate()
    {
        spriter.flipX = target.position.x < rigid.position.x;
    }
}
