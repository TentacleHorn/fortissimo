using CodeBase.RTS.Factories.Interfaces;
using UnityEngine;
using UnityEngine.Pool;

namespace CodeBase.RTS.Factories
{
	public class EntityFactory : MonoBehaviour, IGameObjectFactory
	{
		private ObjectPool<GameObject> _pool;
		
		public GameObject Create()
		{
			return null;
		}

		private void Awake()
		{
			_pool = new(OnCreate);
		}

		private GameObject OnCreate()
		{
			return null;
		}
	}
}