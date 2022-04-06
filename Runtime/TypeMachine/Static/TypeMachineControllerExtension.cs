using System;

public static class TypeMachineControllerExtension
{

	#region IsState

	public static bool IsState<T>(this IHoldTM hold)
	{
		return hold.TM.IsState<T>();
	}

	public static bool IsState<T>(this IHoldTM hold, T instance)
	{
		return hold.TM.IsState(instance);
	}

	#endregion

	#region State

	public static void SetState<T>(this IHoldTM hold)
	{
		hold.TM.SetState<T>();
	}

	public static void SetState<T>(this IHoldTM hold, T instance)
	{
		hold.TM.SetState(instance);
	}

	public static void SetState(this IHoldTM hold, Type type)
	{
		hold.TM.SetState(type);
	}

	#endregion

	#region AddState

	public static void AddState<T>(this IHoldTM hold, Action<Type, Type> enterAction = default, Action<Type, Type> exitAction = default, Action updateAction = default)
	{
		hold.TM.AddState<T>(enterAction, exitAction, updateAction);
	}

	public static void AddState<T>(this IHoldTM hold, T state, Action<Type, Type> enterAction = default, Action<Type, Type> exitAction = default, Action updateAction = default)
	{
		hold.TM.AddState(state, enterAction, exitAction, updateAction);
	}

	#endregion

	#region RemoveState

	public static void RemoveState<T>(this IHoldTM hold)
	{
		hold.TM.RemoveState<T>();
	}

	public static void RemoveState<T>(this IHoldTM hold, T state)
	{
		hold.TM.RemoveState(state);
	}

	#endregion

	#region SetDefaultState

	public static void SetDefaultState<T>(this IHoldTM hold)
	{
		hold.TM.SetDefaultState<T>();
	}

	public static void SetDefaultState<T>(this IHoldTM hold, T state)
	{
		hold.TM.SetDefaultState(state);
	}

	#endregion

}
