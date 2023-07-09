using System.Collections;

namespace CodeBase.Services.UnityContext.Interfaces
{
	public interface ICoroutineRunner
	{
		public void RunCoroutine(IEnumerator enumerator);
	}
}