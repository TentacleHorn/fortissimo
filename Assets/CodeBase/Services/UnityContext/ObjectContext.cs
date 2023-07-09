using System;
using System.Collections;
using CodeBase.Services.UnityContext.Interfaces;
using UnityEngine;

namespace CodeBase.Services.UnityContext
{
	public class ObjectContext : MonoBehaviour, ICoroutineRunner, IFixedUpdateContext, IUpdateContext, ILateUpdateContext
	{
		public event Action FixedUpdated;
		public event Action Updated;
		public event Action LateUpdated;
		
		public void RunCoroutine(IEnumerator enumerator) => StartCoroutine(enumerator);

		private void Update() => Updated?.Invoke();
		private void FixedUpdate() => FixedUpdated?.Invoke();
		private void LateUpdate() => LateUpdated?.Invoke();
	}
}