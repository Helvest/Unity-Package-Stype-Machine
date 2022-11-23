using System;

public static class TypeMachineControllerExtension
{

	#region IsState

	public static bool IsState<T>(this IHoldTypeMachine hold)
	{
		return hold.TypeMachine.IsState<T>();
	}

	public static bool IsState<T>(this IHoldTypeMachine hold, T instance)
	{
		return hold.TypeMachine.IsState(instance);
	}

	#endregion

	#region GetState

	public static Type GetState(this IHoldTypeMachine hold)
	{
		return hold.TypeMachine.State;
	}

	#endregion

	#region SetState

	public static void SetState<T>(this IHoldTypeMachine hold)
	{
		hold.TypeMachine.SetState<T>();
	}

	public static void SetState<T>(this IHoldTypeMachine hold, T instance)
	{
		hold.TypeMachine.SetState(instance);
	}

	public static void SetState(this IHoldTypeMachine hold, Type type)
	{
		hold.TypeMachine.State = type;
	}

	#endregion

	#region AddState

	public static void AddState<T>(this IHoldTypeMachine hold, Action<Type, Type> enterAction = default, Action<Type, Type> exitAction = default, Action updateAction = default)
	{
		hold.TypeMachine.AddState<T>(enterAction, exitAction, updateAction);
	}

	public static void AddState<T>(this IHoldTypeMachine hold, T state, Action<Type, Type> enterAction = default, Action<Type, Type> exitAction = default, Action updateAction = default)
	{
		hold.TypeMachine.AddState(state, enterAction, exitAction, updateAction);
	}

	public static void AddState(this IHoldTypeMachine hold, Type state, Action<Type, Type> enterAction = default, Action<Type, Type> exitAction = default, Action updateAction = default)
	{
		hold.TypeMachine.AddState(state, enterAction, exitAction, updateAction);
	}

	#endregion

	#region RemoveState

	public static void RemoveState<T>(this IHoldTypeMachine hold)
	{
		hold.TypeMachine.RemoveState<T>();
	}

	public static void RemoveState<T>(this IHoldTypeMachine hold, T state)
	{
		hold.TypeMachine.RemoveState(state);
	}

	#endregion

}
