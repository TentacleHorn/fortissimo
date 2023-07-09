using System;
using CodeBase.RTS.Entities.Common;

namespace CodeBase.RTS.Entities.Interfaces
{
	public interface IEntity
	{
		public bool IsPlayersEntity { get; }
		
		public event Action<IEntity> OnInitialization;
		public event Action<IEntity> OnDeath;

		public void Initiate(EntityData obj);
		public void Attack(IEntity damageTarget);
		public void GetDamage(int amount);
		public void Move();
	}
}