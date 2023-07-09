using System;
using CodeBase.RTS.Entities.Common;
using CodeBase.RTS.Entities.Interfaces;
using UnityEngine;

namespace CodeBase.RTS.Entities
{
	[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
	public abstract class EntityBase : MonoBehaviour, IEntity
	{
		public bool IsPlayersEntity { get; set; }
		
		public int health;
		public int damage;
		public float attackCD;
		public float speed;

		public event Action<IEntity> OnInitialization;
		public event Action<IEntity> OnDeath;

		protected float _secondsFromLastAttack;
		protected bool _isInitialized = false;
		protected bool _isDead;
		protected bool _canMove;
		protected Rigidbody2D _rb;

		private readonly float _minAltitude = -10;

		protected const string ENEMY_TAG = "Enemy";
		protected const string PLAYER_TAG = "Player";


		public virtual void Initiate(EntityData data)
		{
			if (_isInitialized) return;
			_isInitialized = true;
			
			health = data.Health;
			damage = data.Damage;
			attackCD = data.AttackCD;
			speed = data.Speed;

			_rb = GetComponent<Rigidbody2D>();
			
			_canMove = true;
			_secondsFromLastAttack = attackCD;
			
			OnInitialization?.Invoke(this);
		}

		private void Update()
		{
			_canMove = true;
			_secondsFromLastAttack += Time.deltaTime;
			Move();
			CheckAltitude();
		}

		private void CheckAltitude()
		{
			if (transform.position.y > _minAltitude) return;
			Die();
		}

		private void OnCollisionStay2D(Collision2D collision)
		{
			EntityBase entity;
			bool isEntity = collision.gameObject.TryGetComponent(out entity);

			if (!isEntity) return;
			
			// If it's a player's entity and target is player's entity => return
			if (IsPlayersEntity && entity.CompareTag(PLAYER_TAG)) return;
			if (!IsPlayersEntity && entity.CompareTag(ENEMY_TAG)) return;
			
			Attack(entity);
		}

		public virtual void Attack(IEntity damageTarget)
		{
			if (!CanAttack()) return;
			
			_canMove = false;
			_secondsFromLastAttack = 0;
		}

		public virtual void GetDamage(int amount)
		{
			health -= amount;
			if (health <= 0) Die();
		}

		protected virtual void Die()
		{
			_isDead = true;
			OnDeath?.Invoke(this);
			_isInitialized = false;
			Destroy(gameObject);
		}

		public virtual void Move()
		{
			if (!_canMove) return;
			
			transform.Translate(Vector3.right * (speed * Time.deltaTime));
		}

		protected bool CanAttack() => (_secondsFromLastAttack >= attackCD);
	}
}