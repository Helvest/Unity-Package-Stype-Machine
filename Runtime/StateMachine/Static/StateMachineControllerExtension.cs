using System;

public static class StateMachineControllerExtension
{

	#region IsState

	public static bool IsState<T>(this IHoldStateMachine<T> hold, T state)
	{
		return hold.StateMachine.IsState(state);
	}

	#endregion

	#region GetState

	public static T GetState<T>(this IHoldStateMachine<T> hold)
	{
		return hold.StateMachine.State;
	}

	#endregion

	#region SetState

	public static void SetState<T>(this IHoldStateMachine<T> hold, T state)
	{
		hold.StateMachine.State = state;
	}

	#endregion

	#region AddState

	public static void AddState<T>(this IHoldStateMachine<T> hold, T state, Action<T, T> enterAction = default, Action<T, T> exitAction = default, Action updateAction = default)
	{
		hold.StateMachine.AddState(state, enterAction, exitAction, updateAction);
	}

	#endregion

	#region RemoveState

	public static void RemoveState<T>(this IHoldStateMachine<T> hold, T state)
	{
		hold.StateMachine.RemoveState(state);
	}

	#endregion

	#region DefaultState

	public static void ToDefaultState<T>(this IHoldStateMachine<T> hold)
	{
		hold.StateMachine.ToDefaultState();
	}

	public static void SetDefaultState<T>(this IHoldStateMachine<T> hold, T state)
	{
		hold.StateMachine.SetDefaultState(state);
	}

	#endregion

}
