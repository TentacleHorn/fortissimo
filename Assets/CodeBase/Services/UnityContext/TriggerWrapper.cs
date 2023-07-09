using System;
using UnityEngine;

namespace CodeBase.Services.UnityContext
{
	[RequireComponent(typeof(Collider))]
	public class TriggerWrapper : MonoBehaviour
	{
		public event Action<Collider> onTriggerEnter;
		public event Action<Collider> onTriggerStay;
		public event Action<Collision> onCollisionEnter;
		
		private void OnCollisionEnter(Collision collision) => onCollisionEnter?.Invoke(collision);

		private void OnTriggerEnter(Collider other) => onTriggerEnter?.Invoke(other);

		private void OnTriggerStay(Collider other) => onTriggerStay?.Invoke(other);
	}
}