using UnityEngine;

namespace CodeBase.RTS.Entities.Common
{
	[CreateAssetMenu(fileName = "Melee", menuName = "Enemies/EnemyData", order = 0)]
	public class EntityData : ScriptableObject
	{
		public GameObject Prefab;
		
		public int Health;
		public int Damage;
		public float AttackCD;
		public float Speed;
	}
}