using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    // ボタンフェードしながらシーン遷移
   public void OnStartButton()
    {
        FadeManager.Instance.LoadScene("GameScene",1f);
    }
}
