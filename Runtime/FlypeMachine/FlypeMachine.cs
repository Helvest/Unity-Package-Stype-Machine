using System;
using System.Collections.Generic;

namespace StypeMachine
{
	[Serializable]
	public class FlypeMachine : FlagMachine<Type>
	{

		#region Constructor

		public FlypeMachine(bool acceptStateNotIncluded = true, bool canReenterSameState = false) :
			base(acceptStateNotIncluded, canReenterSameState)
		{ }

		#endregion

		#region Has Flag

		public bool HasFlag<t>()
		{
			return HasFlag(typeof(t));
		}

		public bool HasFlag<t>(t instance)
		{
			return HasFlag(instance.GetType());
		}

		#endregion

		#region Add Flags

		public void AddFlag<t>()
		{
			AddFlag(typeof(t));
		}

		public void AddFlags<t>(params t[] instances)
		{
			foreach (var instance in instances)
			{
				AddFlag(instance.GetType());
			}
		}

		public void AddFlags<t>(IEnumerable<t> instances)
		{
			foreach (var instance in instances)
			{
				AddFlag(instance.GetType());
			}
		}

		#endregion

		#region Remove Flags

		public void RemoveFlag<t>()
		{
			RemoveFlag(typeof(t));
		}

		public void RemoveFlags<t>(params t[] instances)
		{
			foreach (var instance in instances)
			{
				RemoveFlag(instance.GetType());
			}
		}

		public void RemoveFlags<t>(IEnumerable<t> instances)
		{
			foreach (var instance in instances)
			{
				RemoveFlag(instance.GetType());
			}
		}

		#endregion

		#region Replace Flags

		public void ReplaceFlags<t>(params t[] instances)
		{
			var types = new List<Type>();

			foreach (var instance in instances)
			{
				types.Add(instance.GetType());
			}

			ReplaceFlagsIntern(types);
		}

		public void ReplaceFlags<t>(IEnumerable<t> instances)
		{
			var types = new List<Type>();

			foreach (var instance in instances)
			{
				types.Add(instance.GetType());
			}

			ReplaceFlagsIntern(types);
		}

		#endregion

		#region Add State

		public void AddState<t>(Action<Type> enterAction = default, Action<Type> exitAction = default, Action updateAction = default)
		{
			base.AddState(typeof(t), enterAction, exitAction, updateAction);
		}

		public void AddState<t>(t type, Action<Type> enterAction = default, Action<Type> exitAction = default, Action updateAction = default)
		{
			base.AddState(type.GetType(), enterAction, exitAction, updateAction);
		}

		#endregion

		#region Remove State

		public void RemoveState<t>()
		{
			base.RemoveState(typeof(t));
		}

		public void RemoveState<t>(t type)
		{
			base.RemoveState(type.GetType());
		}

		#endregion

	}
}
