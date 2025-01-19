using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.TextCore.Text;

public class AudioPlayer:MonoBehaviour {

	static ObjectPool<AudioPlayer> pool = new ObjectPool<AudioPlayer>(CreateObject);
	static GameObject prefab;
	static AudioPlayer CreateObject() {
		if(!prefab) prefab=Resources.Load<GameObject>("AudioPlayer");
		GameObject gameObject = Instantiate(prefab);
		DontDestroyOnLoad(gameObject);
		return gameObject.GetComponent<AudioPlayer>();
	}
	public static AudioPlayer PlayAudio(Vector2 position,AudioClip[] audioClips,float volume = 1) =>
		PlayAudio(position,audioClips[Random.Range(0,audioClips.Length)],volume);
	public static AudioPlayer PlayAudio(Vector2 position,AudioClip audioClip,float volume = 1) {
		pool.Get(out AudioPlayer audioPlayer);
		audioPlayer.targetAudio=audioClip;
		audioPlayer.volume=volume;
		audioPlayer.gameObject.SetActive(true);
		audioPlayer.played=false;
		if(audioPlayer.audioSource) {
			audioPlayer.OnEnable();
		}
		return audioPlayer;
	}

	float volume;
	AudioClip targetAudio;
	AudioSource audioSource;
	bool played;

	void Awake() {
		audioSource=GetComponent<AudioSource>();
	}
	void OnEnable() {
		if(played) return;
		audioSource.volume=volume;
		audioSource.PlayOneShot(targetAudio);
	}
	void Update() {
		if(!audioSource.isPlaying) {
			gameObject.SetActive(false);
			pool.Release(this);
		}
	}
}
