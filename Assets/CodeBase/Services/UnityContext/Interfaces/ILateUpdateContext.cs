using System;

namespace CodeBase.Services.UnityContext.Interfaces
{
	public interface ILateUpdateContext
	{
		public event Action LateUpdated;
	}
}