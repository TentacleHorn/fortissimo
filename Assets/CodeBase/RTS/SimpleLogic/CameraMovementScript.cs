using System.ComponentModel;
using UnityEngine;

namespace CodeBase.RTS.SimpleLogic
{
	[RequireComponent(typeof(Camera))]
	public class CameraMovementScript : MonoBehaviour
	{
		[SerializeField] [ReadOnly(true)] private Vector2 _mousePosition;
		[SerializeField] [ReadOnly(true)] private int _maxResolutionWidth;
		[SerializeField] [ReadOnly(true)] private int _mouseActiveCoords;
		[SerializeField] [ReadOnly(false)] private float _cameraSpeed;

		private Camera _camera;

		private void Awake()
		{
			_camera = Camera.main;
			_maxResolutionWidth = Screen.currentResolution.width;
			_mouseActiveCoords = _maxResolutionWidth / 10;
		}

		private void FixedUpdate()
		{
			_mousePosition = Input.mousePosition;
			
			ProcessMouseInput(_camera, _mousePosition, _maxResolutionWidth, _mouseActiveCoords, _cameraSpeed);
			
			#if UNITY_EDITOR
			WriteMousePositionToDebug();
			#endif
		}

		private void ProcessMouseInput(Camera activeCamera, Vector2 mousePosition, int maximumWidth, int mouseActiveCoords, float speed)
		{
			int leftSide = mouseActiveCoords;
			int rightSide = maximumWidth - mouseActiveCoords;

			if (mousePosition.x <= leftSide)
			{
				Move(activeCamera.transform, -speed);
			}
			else if (mousePosition.x >= rightSide)
			{
				Move(activeCamera.transform, speed);
			}
		}

		private void Move(Transform activeCameraTransform, float speed)
		{
			Vector3 direction = Vector3.right;
			activeCameraTransform.Translate(direction * speed * Time.fixedDeltaTime);
		}

#if UNITY_EDITOR
		private void WriteMousePositionToDebug() => Debug.Log(_mousePosition);
		#endif
	}
}