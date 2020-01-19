using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainSource : MonoBehaviour
{

    public int gameRound = 1; // 게임 라운드
    public bool isMyTrun = true; // 내 턴인가?
    public int myHP = 3000; // 내 HP
    public int enemyHP = 3000; // 적 HP

    // UI 
    public Button btnGameStart; // 게임 시작 버튼
    public Text hintMessage; // 적 HP 텍스트
    public Text myHPText; // 내 HP 텍스트
    public Text enemyHPText; // 적 HP 텍스트
    public GameObject myCharator, enemyCharactor;

    public Card sampleCard; // 복재용 카드 프리팹
    public Card[] myCardList = new Card[5]; // 내 카드 5개 정보
    public Card[] enemyCardList = new Card[5]; // 적 카드 5개 정보


    public void GameStart()
    {
        // TODO 기존의 카드가 있다면 destory // 이건 reset 일까?
        btnGameStart.gameObject.SetActive(false); // 게임 시작 버튼 숨김
        gameRound = 1;
        isMyTrun = true;

        // 캐릭터 정보 표시
        myCharator.SetActive(true);
        enemyCharactor.SetActive(true);
        // HP 초기화
        myHP = 3000; enemyHP = 3000; 
        myHPText.text = "HP: " + myHP;
        enemyHPText.text = "HP: " + enemyHP;
        myHPText.gameObject.SetActive(true);
        enemyHPText.gameObject.SetActive(true);

        // 카드 설정
        myCardList = SetNewCardList(true); // 내 카드 5개를 설정
        enemyCardList = SetNewCardList(false); // 적 카드 5개를 설정

        // 메세지 설정
        hintMessage.gameObject.SetActive(true);
        hintMessage.text = "당신의 턴입니다. 카드를 선택하세요.";

    }
    public void GameOver()
    {
        btnGameStart.enabled = true;
    }

    /// <summary> 새 카드 생성 메소드 </summary>
    public Card[] SetNewCardList(bool isMe)
    {
        Card[] newCardList = new Card[5]; // 반환할 카드 객체
        for(int i=0; i<newCardList.Length; i++) 
        {
            Vector3 position = new Vector3(
                -6.0f + i*3.0f, // x축: -6 지점부터 3씩 증가하면서 배치
                (isMe ? -5.0f : 5.0f), //y축: 내꺼면 아래, 적꺼면 위
            0/*z축*/); 
            newCardList[i] = Instantiate<Card>(sampleCard, position, Quaternion.identity); // 카드 복제
            // https://docs.unity3d.com/2018.4/Documentation/ScriptReference/Object.Instantiate.html
            newCardList[i].debugName = (isMe ? "내" : "적") + "카드 " + (i+1);
            newCardList[i].mainRef = this;
            newCardList[i].isMyCard = isMe;
            newCardList[i].cardIndex = i;
        }
        return newCardList;
    }

    // 게임 진행중 안내 메세지 설정
    public void SetHintMessage(string msg)
    {
        hintMessage.text = msg;
    }

    // 내가 선택한 카드
    public void SelectMyCard(int cardIndex)
    {
        isMyTrun = false;
        this.SetHintMessage("상대방의 턴입니다.");
        // 내가 선택한 카드와 가진 카드에서 낼수 있는 카드만 구하기
        int selectedCardBgType = myCardList[cardIndex].bgType; // 선택한 카드 색상
        int selectedCardShape = myCardList[cardIndex].shape; // 선택한 카드 모양
        
        List<int> avableEnemyCardList = new List<int>(); // 사용가능한 카드 리스트
        for (int i=0; i<enemyCardList.Length; i++)
        {
            if (enemyCardList[i].isUsed) continue; // 사용한 카드면 건너뜀
            if ( enemyCardList[i].bgType == selectedCardBgType // 배경 또는
                || enemyCardList[i].shape == selectedCardShape ) // 모양이 같으면
            {
                avableEnemyCardList.Add(i); // 리스트에 추가
            }
        }
        int[] avableEnemyCardArray = avableEnemyCardList.ToArray(); // 배열로 변환
        // 낼카드가 없다면 Win
        if (avableEnemyCardArray.Length < 1)
        {
            SetHintMessage("적이 낼수 있는 카드가 없습니다. You Win");
            return;
        }

        int selectEnemyCardIndex = avableEnemyCardArray[Random.Range(0, avableEnemyCardArray.Length)];
        Debug.Log(selectEnemyCardIndex);
        // TODO 잠시 지연후 
        enemyCardList[selectEnemyCardIndex].SelectEnemy();
    }

}