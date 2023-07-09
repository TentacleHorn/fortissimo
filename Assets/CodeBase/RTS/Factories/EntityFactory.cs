using CodeBase.RTS.Entities.Interfaces;
using CodeBase.RTS.Factories.Interfaces;
using UnityEngine;
using UnityEngine.Pool;

namespace CodeBase.RTS.Factories
{
	public class EntityFactory : MonoBehaviour, IEntityFactory
	{
		private ObjectPool<IEntity> _pool;
		
		public IEntity Create()
		{
			IEntity entity = _pool.Get();
			return entity;
		}

		private void Awake()
		{
		}
	}
}