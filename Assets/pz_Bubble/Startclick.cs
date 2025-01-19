using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startclick : MonoBehaviour
{
    public int index; // 场景索引
    public AudioSource audioSource; // 音频源

    // 用于跳转场景的方法
    public void LoadScene()
    {
        SceneManager.LoadScene(index);
    }

    // 协程：等待音频播放完毕，然后加载场景
    public IEnumerator WaitForAudioAndLoadScene()
    {
        // 确保音频源和音频剪辑都存在，并音频正在播放
        if (audioSource != null && audioSource.clip != null)
        {
            // 播放音频
            audioSource.Play();

            // 等待音频播放完成
            while (audioSource.isPlaying)
            {
                yield return null; // 每帧检查音频是否仍在播放
            }
        }

        // 音频播放结束后跳转场景
        LoadScene();
    }

    // 按钮触发调用的函数
    public void OnStartButtonClick()
    {
        StartCoroutine(WaitForAudioAndLoadScene());
    }
    public void QuiteGame()
    { 
    Application.Quit();
    }
}