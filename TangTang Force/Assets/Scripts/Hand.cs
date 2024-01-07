using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriter;
    SpriteRenderer player;
    Vector3 rightPos = new Vector3(0.35f, -0.15f, 0);
    Vector3 rightPosReverse = new Vector3(-0.15f, -0.15f, 0);
    Quaternion leftRot = Quaternion.Euler(0, 0, -35); //Euler 왼손의 각 회전을 저장
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -135);

    private void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1]; //자기도 포함이라서 0번째 하면 안됨
    }
    private void LateUpdate()
    {
        bool isReverse = player.flipX;
        if (isLeft)//근접무기
        {//왼손 회전에는 localRotation 사용
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ? 4 : 6;
        }
        else //원거리 무기
        {//오른손 회전에는 localPosition 사용
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse ? 6 : 4;
        }
    }
}
