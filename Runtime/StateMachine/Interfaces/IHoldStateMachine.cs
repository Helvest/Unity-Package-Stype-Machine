using StypeMachine;

public interface IHoldStateMachine<T>
{
	public StateMachine<T> StateMachine { get; }
}
