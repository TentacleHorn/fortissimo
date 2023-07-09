using CodeBase.RTS.Entities;
using CodeBase.RTS.Entities.Common;
using UnityEngine;

namespace CodeBase.RTS.Spawning
{
	public class PlayerSpawningManager : MonoBehaviour
	{
		public void Spawn(Vector3 position, EntityData data)
		{
			GameObject prefab = data.Prefab;
			GameObject objOnScene = Instantiate(prefab, position, Quaternion.identity, null);
			EntityBase entity = objOnScene.GetComponent<EntityBase>();
			entity.IsPlayersEntity = false;
			entity.Initiate(data);
		}
	}
}