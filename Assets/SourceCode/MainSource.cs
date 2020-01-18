using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSource : MonoBehaviour
{
    public int myHP = 3000; // 내 HP
    public int enemyHP = 3000; // 적 HP

    // UI 
    public Button btnGameStart; // 게임 시작 버튼
    public Text myHPText; // 내 HP 텍스트
    public Text enemyHPText; // 적 HP 텍스트

    public Card sampleCard; // 복재용 카드 프리팹
    public Card[] myCardList = new Card[5]; // 내 카드 5개 정보
    public Card[] enemyCardList = new Card[5]; // 적 카드 5개 정보


    public void GameStart()
    {
        btnGameStart.enabled = true;
        // TODO 기존의 카드가 있다면 destory // 이건 reset 일까?
        myHP = 3000; enemyHP = 3000; // HP 초기화
        myHPText.text = "HP: " + myHP;
        enemyHPText.text = "HP: " + enemyHP;

        myCardList = SetNewCardList(true); // 내 카드 5개를 설정
        enemyCardList = SetNewCardList(false); // 적 카드 5개를 설정
    }
    public void GameOver()
    {
        btnGameStart.enabled = true;
    }

    // @Lifecycle Override
    private void Update()
    {
        
    }

    /// <summary> 새 카드 생성 메소드 </summary>
    public Card[] SetNewCardList(bool isMe)
    {
        Card[] newCardList = new Card[5]; // 반환할 카드 객체
        for(int i=0; i<newCardList.Length; i++) 
        {
            Vector3 position = new Vector3(
                -6.0f + i*3.0f, // x축: -6 지점부터 3씩 증가하면서 배치
                (isMe ? -4.0f : 4.0f), //y축: 내꺼면 아래, 적꺼면 위
            0/*z축*/); 
            newCardList[i] = Instantiate<Card>(sampleCard, position, Quaternion.identity); // 카드 복제
            // https://docs.unity3d.com/2018.4/Documentation/ScriptReference/Object.Instantiate.html
            newCardList[i].debugName = (isMe ? "내" : "적") + "카드 " + (i+1);
        }
        return newCardList;
    }
}

/* [TODO]AssetBundle
public class Resources {
    // Sprite sprite = Resources.
    public Resources()
    {
        
    }
}
*/
