using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId; //몇번째 프리팹인지 알기위해
    public float damage;
    public int count;
    public float speed;
    private void Start() {
        Init();
    }
    private void Update() {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.forward * speed*Time.deltaTime);
            break;
            default:
            break;
        }
        if(Input.GetButtonDown("Jump"))
        {
            LevelUp(20,5);
        }
    }
    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count +=count;
        //속성 변경과 도시에 근접무기의 경우 배치도 필요하니 함수 호출
        if(id==0)
            Batch();
    }
    public void Init()
    {
        switch (id)
        {
            case 0:
            speed = -150; //마이너스로 해야 반대로 돔
            Batch();
            break;
            default:
            break;
        }
    }
    //생성된 무기를 배치하는 함수 생성 및 호출
    void Batch()
    {
        for (int i = 0; i < count; i++)
        {//부모를 바꾸기 위해 Transform으로 가져옴
          Transform bullet;
          if(i<transform.childCount)
          {//기존 오브젝트를 먼저 활용하고 모자란것은 풀링에서 가져오기
            bullet = transform.GetChild(i);
          }
          else
          {
            bullet = GameManager.instance.pool.Get(prefabId).transform;
            bullet.parent = transform;
          }
          bullet.localPosition = Vector3.zero; 
          bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward*360* i /count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            //Translate(): 이 함수로 자신의 위쪽으로 이동

          bullet.GetComponent<Bullet>().Init(damage, -1); //근접무기는 무조건 관통하기 때문에 per=-1 무한으로 관통한다는 뜻으로 놓을거임
        }
    }
}
