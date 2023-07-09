using UnityEngine;

namespace CodeBase.RTS.SimpleLogic
{
	[CreateAssetMenu(fileName = "AudioData", menuName = "ScriptableObjs/AudioData", order = 0)]
	public class AudioData : ScriptableObject
	{
		public AudioClip Clip;
		public float Duration;
		public float Pitch;
		public float Volume;
	}
}