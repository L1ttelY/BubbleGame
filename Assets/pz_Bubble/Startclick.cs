using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startclick : MonoBehaviour
{
    public int index; // ��������
    public AudioSource audioSource; // ��ƵԴ

    // ������ת�����ķ���
    public void LoadScene()
    {
        SceneManager.LoadScene(index);
    }

    // Э�̣��ȴ���Ƶ������ϣ�Ȼ����س���
    public IEnumerator WaitForAudioAndLoadScene()
    {
        // ȷ����ƵԴ����Ƶ���������ڣ�����Ƶ���ڲ���
        if (audioSource != null && audioSource.clip != null)
        {
            // ������Ƶ
            audioSource.Play();

            // �ȴ���Ƶ�������
            while (audioSource.isPlaying)
            {
                yield return null; // ÿ֡�����Ƶ�Ƿ����ڲ���
            }
        }

        // ��Ƶ���Ž�������ת����
        LoadScene();
    }

    // ��ť�������õĺ���
    public void OnStartButtonClick()
    {
        StartCoroutine(WaitForAudioAndLoadScene());
    }
    public void QuiteGame()
    { 
    Application.Quit();
    }
}