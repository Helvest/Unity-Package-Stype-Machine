using System;
using System.Collections.Generic;

public static class FlagMachineControllerExtension
{

	#region Has Flags

	public static bool HasFlag<T>(this IHoldFlagMachine<T> hold, T state)
	{
		return hold.FlagMachine.HasFlag(state);
	}

	public static bool HasFlags<T>(this IHoldFlagMachine<T> hold, params T[] state)
	{
		return hold.FlagMachine.HasFlags(state);
	}

	public static bool HasFlags<T>(this IHoldFlagMachine<T> hold, IEnumerable<T> state)
	{
		return hold.FlagMachine.HasFlags(state);
	}

	#endregion

	#region Get Flags

	public static List<T> GetFlags<T>(this IHoldFlagMachine<T> hold)
	{
		return hold.FlagMachine.GetFlags();
	}

	#endregion

	#region Add Flag

	public static bool AddFlag<T>(this IHoldFlagMachine<T> hold, T flag)
	{
		return hold.AddFlag(flag);
	}

	public static bool AddFlags<T>(this IHoldFlagMachine<T> hold, params T[] flags)
	{
		return hold.AddFlags(flags);
	}

	public static bool AddFlags<T>(this IHoldFlagMachine<T> hold, IEnumerable<T> flags)
	{
		return hold.AddFlags(flags);
	}

	#endregion

	#region Remove Flags
	public static bool RemoveFlag<T>(this IHoldFlagMachine<T> hold, T value)
	{
		return hold.RemoveFlag(value);
	}

	public static void RemoveAllFlags<T>(this IHoldFlagMachine<T> hold)
	{
		hold.RemoveAllFlags();
	}

	#endregion

	#region Replace Flags

	public static void ReplaceFlags<T>(this IHoldFlagMachine<T> hold, params T[] flags)
	{
		hold.ReplaceFlags(flags);
	}

	public static void ReplaceFlags<T>(this IHoldFlagMachine<T> hold, IEnumerable<T> flags)
	{
		hold.ReplaceFlags(flags);
	}

	#endregion

	#region Add State

	public static void AddState<T>(this IHoldFlagMachine<T> hold, T state, Action<T> enterAction = default, Action<T> exitAction = default, Action updateAction = default)
	{
		hold.FlagMachine.AddState(state, enterAction, exitAction, updateAction);
	}

	#endregion

	#region Remove State

	public static void RemoveState<T>(this IHoldFlagMachine<T> hold, T state)
	{
		hold.FlagMachine.RemoveState(state);
	}

	#endregion

}
