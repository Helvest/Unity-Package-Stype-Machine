using System.Collections.Generic;
using StypeMachine;
using UnityEngine;

[DefaultExecutionOrder(-9999)]
public class MonoFlypeMachine : FlypeMachineController<MonoBehaviour>
{

	#region Fields

	[Header("MonoFlypeMachine")]

	[SerializeField]
	protected List<MonoBehaviour> flags = default;

	[Header("Prefabs")]
	[SerializeField]
	private Transform _transformParentForPrefabs = default;

	[SerializeField]
	protected List<MonoBehaviour> flagPrefab = default;

	#endregion

	#region CreateTypeMachine

	protected override void CreateFlypeMachine()
	{
		base.CreateFlypeMachine();

		foreach (var state in flags)
		{
			if (state != null)
			{
				FlypeMachine.AddFlag(
					state,
					enterAction: (Flag) => state.gameObject.SetActive(true),
					exitAction: (Flag) => state.gameObject.SetActive(false)
				);

				state.gameObject.SetActive(false);
			}
		}

		foreach (var flag in flagPrefab)
		{
			if (flag == null)
			{
				continue;
			}

			var prefab = flag;
			MonoBehaviour instance = null;

			FlypeMachine.AddFlag(flag,
				enterAction: (P) =>
				{
					if (instance != null)
					{
						instance.gameObject.SetActive(true);
					}
					else if (prefab != null)
					{
						instance = Instantiate(prefab, _transformParentForPrefabs);
					}
				},
				exitAction: (P) =>
				{
					if (instance != null)
					{
						Destroy(instance.gameObject);
					}
				}
			);
		}
	}

	#endregion

}
