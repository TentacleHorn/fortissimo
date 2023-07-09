using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.RTS.SimpleLogic
{
	[RequireComponent(typeof(AudioSource))]
	public class QueueAudioPlayerScript : MonoBehaviour
	{
		private Queue<AudioData> _audio = new Queue<AudioData>();
		private AudioSource _source;

		private bool _isBusy;
		
		public void AddClip(AudioData data) => _audio.Enqueue(data);

		private void Awake()
		{
			_source = GetComponent<AudioSource>();
			_isBusy = false;
		}

		private void Update()
		{
			if (_isBusy) return;

			if (_audio.Count <= 0) return;
			
			AudioData nextAudio = _audio.Dequeue();
			if (nextAudio == null) return;

			_isBusy = true;

			_source.clip = nextAudio.Clip;
			_source.pitch = nextAudio.Pitch;
			_source.volume = nextAudio.Volume;
			_source.Play();

			StartCoroutine(StopAudioCoroutine(new WaitForSeconds(nextAudio.Duration)));
		}

		private IEnumerator StopAudioCoroutine(WaitForSeconds waitForSeconds)
		{
			yield return waitForSeconds;
			_source.Stop();
			_source.clip = null;
			_isBusy = false;
		}
	}
}