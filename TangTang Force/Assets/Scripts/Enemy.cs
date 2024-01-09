using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon; //스피드에 따라 걸음애니도 바뀌어야함
    public Rigidbody2D target;
    bool isLive;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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
    void OnEnable() //몬스터 클론이 생성되어 활성화되면! 이렇게 플레이어를 target변수에 담아줄거임(초기화해주기)
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Bullet")) return; 

        health -=collision.GetComponent<Bullet>().damage;

        if(health>0)//live, Hit Action
        {

        }
        else //Die
        {
            Dead();
        }
    }
    void Dead()
    {
        gameObject.SetActive(false);
    }
}
