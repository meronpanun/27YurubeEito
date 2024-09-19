using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    public GameObject Plane;
    //ここでpublicと宣言することで後でInspectorビューから操作できる
    float timer = 0;
    float spowntime = 2; //2秒ごとに生成させる

    void Update()
    {
        timer += Time.deltaTime; //timerの値を1秒に1のペースで増やす
        if (timer > spowntime)
        {
            PlaneGenerate(); //PlaneGenerate関数を呼び出す。
            timer = 0; //timerを0に戻す。
        }
    }

    void PlaneGenerate()
    {
        Instantiate(Plane, new Vector3(1, 0, 0), Quaternion.identity);
        /*Planeを(1,0,0)の場所に生成する。
        Quaternion.identityは回転させないことを示す言葉*/
    }
}
