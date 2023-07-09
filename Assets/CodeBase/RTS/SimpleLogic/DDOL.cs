using UnityEngine;

namespace CodeBase.RTS.SimpleLogic
{
	public class DDOL : MonoBehaviour
	{
		private void Awake() => DontDestroyOnLoad(this);
	}
}