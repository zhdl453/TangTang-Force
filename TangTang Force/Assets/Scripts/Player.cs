using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    Rigidbody2D rigid;
    SpriteRenderer spriter;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {//normalized: 벡터값의 크기가 1이 되도록 좌표가 수정된값. //3.위치이동 => MovePosition은 위치이동이라 현재위치도 더해주어야함
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }
    //인풋시스템으로 이용하면 업데이트 쓸필요없음. 이렇게 안에 있는 만들어진 메소드쓰면됨(슬프게도 자동완성은 안됨ㅠ)
    void OnMove(InputValue value) //InputValue 타입의 매개변수작성
    {
        inputVec = value.Get<Vector2>(); //프로필에서 설정한 컨트롤 타입T값을 가져오는 함수
    }
    //프레임이 종료 되기 전 실행되는 생명주기 함수
    void LateUpdate()
    {
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0; //bool타입으로 넣어주는거임
        }
    }


}
