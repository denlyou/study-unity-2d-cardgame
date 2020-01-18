using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string debugName; // [dummy] 구분용
    public SpriteRenderer shapeSRref;
    public SpriteRenderer backgroundSRref;

    public bool isUsed = false; // 사용한 카드인가?
    public int att; // 공격력
    public int dep; // 방어력
    public int shape; // 도형
    public int bgType; // 색상

    // @Lifecycle Override
    void Start()
    {
        // 자식 오브젝트(배경 이미지, 도형 이미지용 sprite)
        Transform checkTransform = transform.GetChild(0);
        if (checkTransform != null)
            shapeSRref = checkTransform.gameObject.GetComponent<SpriteRenderer>();
        checkTransform = transform.GetChild(1);
        if (checkTransform != null)
            backgroundSRref = checkTransform.gameObject.GetComponent<SpriteRenderer>();
        checkTransform = null;

        this.att = Random.Range(300, 1000);
        this.dep = Random.Range(300, 1000);
        // 색상, 모양 설정
        shape = Random.Range(0, 5); // 0이상 5미만(이하 아님)
        shapeSRref.sprite = R.Sprites.Shapes[shape];
        bgType = Random.Range(0, 5);
        backgroundSRref.sprite = R.Sprites.Background[bgType];
    }

    // @Lifecycle Override
    void Update()
    {
        /*
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log(debugName + " 클릭됨");
        }
        */
    }

}
