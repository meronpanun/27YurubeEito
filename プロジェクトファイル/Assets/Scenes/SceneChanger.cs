using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    // �{�^���t�F�[�h���Ȃ���V�[���J��
   public void OnStartButton()
    {
        FadeManager.Instance.LoadScene("GameScene",1f);
    }
}
