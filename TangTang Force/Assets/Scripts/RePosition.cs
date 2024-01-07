using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 이쁜 코딩을 위해 플레이어에 대한 정보는 게임매니저 클래스를 통해 여기로 전달해줄거임
public class RePosition : MonoBehaviour
{
    Collider2D coll;
    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) return;
        //거리를 구하기 위해 플레이어 위치와 타일맵 위치를 미리저장
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        //대각선일때는 Normalized때문에 1보다 작은값이 되어버림.
        Vector3 playerDir = GameManager.instance.player.inputVec;
        //대각선일때는 Normalized때문에 1보다 작은값이 되어버림.(Normalized없으면 밑에처럼 식 안써도됨)
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY) //플레이어가 상하보다 좌우로 더 많이 움직였을경우
                {
                    transform.Translate(Vector3.right * dirX * 40); //타일맵의 크기도 곱해주는거임(타일한개크기:20) 두개니까 40
                }
                else if (diffX < diffY)//플레이어가 좌우보다 상하로 더 많이 움직였을경우
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;
            case "Enemy":
                if (coll.enabled)
                {
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0)); //플레이어의 방향 * 맵의 조금 멀리떨어진 카메라에서 안보이게 조금만
                }
                break;
        }
    }
}
