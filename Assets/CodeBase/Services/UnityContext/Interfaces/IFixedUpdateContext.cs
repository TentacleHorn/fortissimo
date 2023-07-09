using System;

namespace CodeBase.Services.UnityContext.Interfaces
{
	public interface IFixedUpdateContext
	{
		public event Action FixedUpdated;
	}
}