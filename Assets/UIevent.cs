using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIevent : MonoBehaviour
{
    private bool pauseOn = false;
    private GameObject normalPanel;
    private GameObject pausePanel;

    void Awake()
    {
        normalPanel = GameObject.Find("Canvas").transform.Find("Board").gameObject;
        pausePanel = GameObject.Find("Canvas").transform.Find("stopPanel").gameObject;
    }

   public void ActivePauseBt()
    {//일시정지 버튼을 눌렀을 때 처리

        if (!pauseOn)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            //normalPanel.SetActive(false);     보드 on/off 시 보드 초기화 및 타일 덮어쓰기 에러 발생. 기능 사용x, 일시정지 패널로 덮음
        }

        else
        {
            Time.timeScale = 1.0f;
            pausePanel.SetActive(false);
            //normalPanel.SetActive(true);
        }

        pauseOn = !pauseOn;
    }

    public void RetryBt()
    {
        Debug.Log("게임 재시작.");
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("SampleScene");
    }
    
    public void QuitBt()
    {
        Debug.Log("게임 종료.");
        Application.Quit();
    }

}
