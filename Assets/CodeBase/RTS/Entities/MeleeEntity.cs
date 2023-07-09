using CodeBase.RTS.Entities.Interfaces;

namespace CodeBase.RTS.Entities
{
	public class MeleeEntity : EntityBase
	{
		public override void Attack(IEntity damageTarget)
		{
			if (!CanAttack()) return;
			base.Attack(damageTarget);
			damageTarget.GetDamage(damage);
		}
	}
}