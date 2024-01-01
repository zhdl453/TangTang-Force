using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{//장면이 하나라서 싱글톤 로직까지 작성하진 않음. 그냥 메모리에 올릴거임
    public static GameManager instance;//static으로 선언한건 인스펙터에 안뜬다.
    //정적변수는 즉시 클래스에서 부를 수 있다는 편리함이 있다.
    public Player player;

    void Awake()
    {//Awake생명주기에서 인스턴스 변수를 자기자신 this로 초기화시킴
        instance = this;
    }
}
