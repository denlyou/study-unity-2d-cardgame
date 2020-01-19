using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string debugName; // [dummy] 구분용
    public MainSource mainRef; // (주의) 순환참조
    public SpriteRenderer shapeSRref;
    public SpriteRenderer backgroundSRref;

    public bool isMyCard; // 내 카드인가?
    public int cardIndex; // 카드덱에서 index
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
        // 
    }

    private void OnMouseDown()
    {
        if( !mainRef.isMyTrun ) {
            mainRef.SetHintMessage("내 턴이 아닙니다!");
            return;
        }
        // 내 카드인가?
        if( !isMyCard ) 
        { // 아니면 return;
            mainRef.SetHintMessage("나의 카드 중에서 선택해야 합니다!");
            return;
        }
        // 이미 사용항 카드인지?
        if (this.isUsed)
        {
            mainRef.SetHintMessage("이미 사용한 카드입니다!");
            return;
        }
        else
        {
            this.transform.Translate(0, 1f, 0);
            this.isUsed = true;
            mainRef.SelectMyCard(cardIndex);
        }
    }


    public void SelectEnemy()
    {
        if ( mainRef.isMyTrun || isMyCard )
        {
            mainRef.SetHintMessage("!? 로직상 나오면 안됨");
            return;
        }

        this.transform.Translate(0, -1f, 0);
        this.isUsed = true;

        // 낼카드가 없는지 체크
        List<int> avableMyCardList = new List<int>(); // 사용가능한 카드 리스트
        for (int i = 0; i < mainRef.myCardList.Length; i++)
        {
            if (mainRef.myCardList[i].isUsed) continue; // 사용한 카드면 건너뜀
            if (mainRef.myCardList[i].bgType == this.bgType // 배경 또는
                || mainRef.myCardList[i].shape == this.shape) // 모양이 같으면
            {
                avableMyCardList.Add(i); // 리스트에 추가
            }
        }
        // 낼카드가 없다면 lose
        if (avableMyCardList.Count == 0)
        {
            mainRef.SetHintMessage("낼수 있는 카드가 없습니다. You Lose");
            return;
        }
        // 턴 변경
        mainRef.isMyTrun = true;
    }

}
