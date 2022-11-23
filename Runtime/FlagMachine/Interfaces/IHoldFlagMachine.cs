using StypeMachine;

public interface IHoldFlagMachine<T>
{
	public FlagMachine<T> FlagMachine { get; }
}
