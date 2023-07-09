using UnityEngine;

namespace CodeBase.RTS.Tower
{
	public class HealthView : MonoBehaviour
	{
		public int maxHealth;
		public int health;
		public SpriteRenderer renderer;
		private Material _healthBarMat;
		private static readonly int Health = Shader.PropertyToID("_Health");

		private void Update() => UpdateView();

		private void Start()
		{
			_healthBarMat = renderer.material;
		}

		private void UpdateView()
		{
			float t = Mathf.InverseLerp(0, maxHealth, health);
			_healthBarMat.SetFloat(Health, t);
		}
	}
}