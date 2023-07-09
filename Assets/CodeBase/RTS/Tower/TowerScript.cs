using CodeBase.RTS.Entities;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.RTS.Tower
{
	[RequireComponent(typeof(Collider2D))]
	public class TowerScript : MonoBehaviour
	{
		[SerializeField] private string enemyTag;
		[SerializeField] private int maxHealth;
		[SerializeField] private int health;

		public UnityEvent OnDeath;
		
		private HealthView _view;

		private void Awake()
		{
			_view = GetComponentInChildren<HealthView>();
			_view.maxHealth = maxHealth;
			_view.health = health;
			health = maxHealth;
		}

		private void OnCollisionEnter2D(Collision2D col)
		{
			if (!col.gameObject.CompareTag(enemyTag)) return;

			EntityBase entity = col.gameObject.GetComponent<EntityBase>();
			GetDamage(entity.damage);
			entity.GetDamage(entity.health);
		}

		private void GetDamage(int entityDamage)
		{
			health -= entityDamage;
			_view.health = health;
			if (health <= 0) OnDeath?.Invoke();
		}
	}
}