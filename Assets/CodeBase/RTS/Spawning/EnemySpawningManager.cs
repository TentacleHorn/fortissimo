using System.Collections;
using CodeBase.RTS.Entities;
using CodeBase.RTS.Entities.Common;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.RTS.Spawning
{
	public class EnemySpawningManager : MonoBehaviour
	{
		[SerializeField] private float _spawnCD;
		[SerializeField] private Transform _spawnpoint;
		[SerializeField] private EntityData[] _enemies;

		public bool canSpawn;
		
		private WaitForSeconds _waitFor;
		private Quaternion _initRotation = Quaternion.Euler(0, 180, 0);

		private void Start()
		{
			StartCoroutine(SpawnCoroutine());
			_waitFor = new WaitForSeconds(_spawnCD);
		}

		private IEnumerator SpawnCoroutine()
		{
			while (canSpawn)
			{
				Spawn();
				yield return _waitFor;
			}
		}

		private void Spawn()
		{
			EntityData enemyToSpawn = _enemies[Random.Range(0, _enemies.Length)];
			GameObject prefab = enemyToSpawn.Prefab;
			GameObject objOnScene = Instantiate(prefab, _spawnpoint.position, Quaternion.Euler(0, 180, 0), null);
			EntityBase entity = objOnScene.GetComponent<EntityBase>();
			entity.IsPlayersEntity = false;
			entity.Initiate(enemyToSpawn);
		}
	}
}