using System;
using System.Collections.Generic;

public static class FlypeMachineControllerExtension
{

	#region Has Flag

	public static bool HasFlag<t>(this IHoldFlypeMachine hold)
	{
		return hold.FlypeMachine.HasFlag<t>();
	}

	public static bool HasFlag<t>(this IHoldFlypeMachine hold, t instance)
	{
		return hold.FlypeMachine.HasFlag(instance);
	}

	#endregion

	#region Add Flags

	public static void AddFlag<t>(this IHoldFlypeMachine hold)
	{
		hold.FlypeMachine.AddFlag<t>();
	}

	public static void AddFlags<t>(this IHoldFlypeMachine hold, params t[] instances)
	{
		hold.FlypeMachine.AddFlags(instances);
	}

	public static void AddFlags<t>(this IHoldFlypeMachine hold, IEnumerable<t> instances)
	{
		hold.FlypeMachine.AddFlags(instances);
	}

	#endregion

	#region Remove Flags

	public static void RemoveFlag<t>(this IHoldFlypeMachine hold)
	{
		hold.FlypeMachine.RemoveFlag<t>();
	}

	public static void RemoveFlags<t>(this IHoldFlypeMachine hold, params t[] instances)
	{
		hold.FlypeMachine.RemoveFlags(instances);
	}

	public static void RemoveFlags<t>(this IHoldFlypeMachine hold, IEnumerable<t> instances)
	{
		hold.FlypeMachine.RemoveFlags(instances);
	}

	#endregion

	#region Replace Flags

	public static void ReplaceFlags<t>(this IHoldFlypeMachine hold, params t[] instances)
	{
		hold.FlypeMachine.ReplaceFlags(instances);
	}

	public static void ReplaceFlags<t>(this IHoldFlypeMachine hold, IEnumerable<t> instances)
	{
		hold.FlypeMachine.ReplaceFlags(instances);
	}

	#endregion

	#region AddState

	public static void AddState<T>(this IHoldFlypeMachine hold, Action<Type> enterAction = default, Action<Type> exitAction = default, Action updateAction = default)
	{
		hold.FlypeMachine.AddState<T>(enterAction, exitAction, updateAction);
	}

	public static void AddState<T>(this IHoldFlypeMachine hold, T state, Action<Type> enterAction = default, Action<Type> exitAction = default, Action updateAction = default)
	{
		hold.FlypeMachine.AddState(state, enterAction, exitAction, updateAction);
	}

	public static void AddState(this IHoldFlypeMachine hold, Type state, Action<Type> enterAction = default, Action<Type> exitAction = default, Action updateAction = default)
	{
		hold.FlypeMachine.AddState(state, enterAction, exitAction, updateAction);
	}

	#endregion

	#region RemoveState

	public static void RemoveState<T>(this IHoldFlypeMachine hold)
	{
		hold.FlypeMachine.RemoveState<T>();
	}

	public static void RemoveState<T>(this IHoldFlypeMachine hold, T state)
	{
		hold.FlypeMachine.RemoveState(state);
	}

	#endregion

}