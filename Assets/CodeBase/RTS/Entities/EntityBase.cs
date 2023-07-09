using System;
using CodeBase.RTS.Entities.Common;
using CodeBase.RTS.Entities.Interfaces;
using UnityEngine;

namespace CodeBase.RTS.Entities
{
	[RequireComponent(typeof(Collider2D))]
	public abstract class EntityBase : MonoBehaviour, IEntity
	{
		public bool IsPlayersEntity { get; set; }
		
		public int health;
		public int damage;
		public float attackCD;
		public float speed;

		protected float _secondsFromLastAttack;
		protected bool _isInitialized;
		protected bool _isDead;
		protected bool _canMove;

		protected const string ENEMY_TAG = "Enemy";
		protected const string PLAYER_TAG = "Player";
		
		public event Action<IEntity> OnInitialization;
		public event Action<IEntity> OnDeath;
		
		public virtual void Initiate(EntityData data)
		{
			if (_isInitialized) return;
			_isInitialized = true;
			
			health = data.Health;
			damage = data.Damage;
			attackCD = data.AttackCD;
			speed = data.Speed;
			
			_canMove = true;
			_secondsFromLastAttack = attackCD;
			
			OnInitialization?.Invoke(this);
		}

		private void Update()
		{
			_canMove = true;
			_secondsFromLastAttack += Time.deltaTime;
			Move();
		}

		private void OnCollisionStay2D(Collision2D collision)
		{
			EntityBase entity;
			bool isEntity = collision.gameObject.TryGetComponent(out entity);
			
			// If it's a player's entity and target is player's entity => return
			if ((!entity.CompareTag(ENEMY_TAG) && IsPlayersEntity) ||
			    (!entity.CompareTag(PLAYER_TAG) && !IsPlayersEntity)) return;
			
			if (isEntity) Attack(entity);
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
		}

		public virtual void Move()
		{
			if (!_canMove) return;
			
			transform.Translate(transform.forward * speed);
		}

		protected bool CanAttack() => (_secondsFromLastAttack >= attackCD);
	}
}