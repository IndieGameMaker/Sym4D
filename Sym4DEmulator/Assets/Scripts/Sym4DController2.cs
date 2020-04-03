using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sym = Sym4D.Sym4DEmulator;

public class Sym4DController2 : MonoBehaviour
{
    public int xPort; //좌석장비의 통신 포트
    public int wPort; //바람장비의 통신 포트

    private WaitForSeconds ws = new WaitForSeconds(0.5f);

    public void OnInit()
    {
        StartCoroutine(Init());
    }

    //포트 번호 초기화 및 기본 테스트
    IEnumerator Init()
    {
        //포트번호 설정
        xPort = Sym.Sym4D_X_Find();
        wPort = Sym.Sym4D_W_Find();

        //Sym4D-X100 COM Port Open  및 컨텐츠 시작을 장치에 전달
        Sym.Sym4D_X_StartContents(xPort);
        yield return ws;

        //Roll, Pitch 최대허용 각도(0 ~ 100) : 0도 ~ 10도
        Sym.Sym4D_X_SetConfig(100, 100);
        yield return ws;

        //Sym4D-X100에 모션 데이터 전달(-100 ~ +100)
        Sym.Sym4D_X_SendMosionData(0, 0);
        yield return ws;

        Sym.Sym4D_X_SendMosionData(100, 100);
        yield return ws;

        Sym.Sym4D_X_SendMosionData(-100, -100);
        yield return ws;

        Sym.Sym4D_X_SendMosionData(100, -100);
        yield return ws;

        Sym.Sym4D_X_SendMosionData(-100, 100);
        yield return ws;

        Sym.Sym4D_X_SendMosionData(0, 0);
        yield return ws;

        Sym.Sym4D_X_EndContents();
        yield return ws;


        //Wind 테스트
        Sym.Sym4D_W_StartContents(wPort);
        yield return ws;

        Sym.Sym4D_W_SendMosionData(0);
        yield return new WaitForSeconds(0);

        Sym.Sym4D_W_SendMosionData(50);
        yield return new WaitForSeconds(5);

        Sym.Sym4D_W_SendMosionData(100);
        yield return new WaitForSeconds(5);

        Sym.Sym4D_W_SendMosionData(50);
        yield return new WaitForSeconds(5);

        Sym.Sym4D_W_SendMosionData(0);
        yield return ws;

        Sym.Sym4D_W_EndContents();
    }


}
