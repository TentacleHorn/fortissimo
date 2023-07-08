namespace CodeBase.RTS.Factories.Interfaces
{
	public interface IFactory<T>
	{
		public T Create();
	}
}