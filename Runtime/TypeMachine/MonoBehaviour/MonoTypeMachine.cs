using System.Collections.Generic;
using StypeMachine;
using UnityEngine;

[DefaultExecutionOrder(-9999)]
public class MonoTypeMachine : TypeMachineController<MonoBehaviour>
{

	#region Fields

	[Header("MonoTypeMachine")]

	[SerializeField]
	protected List<MonoBehaviour> states = default;

	[Header("Prefabs")]
	[SerializeField]
	private Transform _transformParentForPrefabs = default;

	[SerializeField]
	protected List<MonoBehaviour> statesPrefab = default;

	#endregion

	#region CreateTypeMachine

	protected override void CreateTypeMachine()
	{
		base.CreateTypeMachine();

		foreach (var state in states)
		{
			if (state != null)
			{
				state.gameObject.SetActive(false);

				TypeMachine.AddState(
					state,
					enterAction: (P, N) => state.gameObject.SetActive(true),
					exitAction: (P, N) => state.gameObject.SetActive(false)
				);
			}
		}

		foreach (var state in statesPrefab)
		{
			if (state == null)
			{
				continue;
			}

			var prefab = state;
			MonoBehaviour instance = null;

			TypeMachine.AddState(state,
				enterAction: (P, N) =>
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
				exitAction: (P, N) =>
				{
					if (instance == null)
					{
						return;
					}

					if (P == N)
					{
						//Just desactivate because the instance is going to be re-use
						instance.gameObject.SetActive(false);
					}
					else
					{
						Destroy(instance.gameObject);
					}
				}
			);
		}
	}

	#endregion

}
