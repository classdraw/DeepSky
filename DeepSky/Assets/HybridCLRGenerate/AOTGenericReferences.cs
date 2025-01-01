using System.Collections.Generic;
public class AOTGenericReferences : UnityEngine.MonoBehaviour
{

	// {{ AOT assemblies
	public static readonly IReadOnlyList<string> PatchedAOTAssemblyList = new List<string>
	{
		"Common.dll",
		"FrameInfo.dll",
		"System.Core.dll",
		"Unity.Netcode.Runtime.dll",
		"UnityEngine.CoreModule.dll",
		"mscorlib.dll",
	};
	// }}

	// {{ constraint implement type
	// }} 

	// {{ AOT generic types
	// System.Action<UnityEngine.Vector2Int>
	// System.Action<UnityEngine.Vector3,UnityEngine.Quaternion>
	// System.Action<object>
	// System.Action<ulong>
	// System.Collections.Generic.ArraySortHelper<UnityEngine.Vector2Int>
	// System.Collections.Generic.ArraySortHelper<object>
	// System.Collections.Generic.Comparer<UnityEngine.Vector2Int>
	// System.Collections.Generic.Comparer<object>
	// System.Collections.Generic.Dictionary.Enumerator<UnityEngine.Vector2Int,object>
	// System.Collections.Generic.Dictionary.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.Enumerator<ulong,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<UnityEngine.Vector2Int,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<ulong,object>
	// System.Collections.Generic.Dictionary.KeyCollection<UnityEngine.Vector2Int,object>
	// System.Collections.Generic.Dictionary.KeyCollection<int,object>
	// System.Collections.Generic.Dictionary.KeyCollection<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection<ulong,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<UnityEngine.Vector2Int,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<ulong,object>
	// System.Collections.Generic.Dictionary.ValueCollection<UnityEngine.Vector2Int,object>
	// System.Collections.Generic.Dictionary.ValueCollection<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection<ulong,object>
	// System.Collections.Generic.Dictionary<UnityEngine.Vector2Int,object>
	// System.Collections.Generic.Dictionary<int,object>
	// System.Collections.Generic.Dictionary<object,object>
	// System.Collections.Generic.Dictionary<ulong,object>
	// System.Collections.Generic.EqualityComparer<UnityEngine.Vector2Int>
	// System.Collections.Generic.EqualityComparer<int>
	// System.Collections.Generic.EqualityComparer<object>
	// System.Collections.Generic.EqualityComparer<ulong>
	// System.Collections.Generic.HashSet.Enumerator<ulong>
	// System.Collections.Generic.HashSet<ulong>
	// System.Collections.Generic.HashSetEqualityComparer<ulong>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<UnityEngine.Vector2Int,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<ulong,object>>
	// System.Collections.Generic.ICollection<UnityEngine.Vector2Int>
	// System.Collections.Generic.ICollection<object>
	// System.Collections.Generic.ICollection<ulong>
	// System.Collections.Generic.IComparer<UnityEngine.Vector2Int>
	// System.Collections.Generic.IComparer<object>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<UnityEngine.Vector2Int,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<ulong,object>>
	// System.Collections.Generic.IEnumerable<UnityEngine.Vector2Int>
	// System.Collections.Generic.IEnumerable<object>
	// System.Collections.Generic.IEnumerable<ulong>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<UnityEngine.Vector2Int,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<ulong,object>>
	// System.Collections.Generic.IEnumerator<UnityEngine.Vector2Int>
	// System.Collections.Generic.IEnumerator<object>
	// System.Collections.Generic.IEnumerator<ulong>
	// System.Collections.Generic.IEqualityComparer<UnityEngine.Vector2Int>
	// System.Collections.Generic.IEqualityComparer<int>
	// System.Collections.Generic.IEqualityComparer<object>
	// System.Collections.Generic.IEqualityComparer<ulong>
	// System.Collections.Generic.IList<UnityEngine.Vector2Int>
	// System.Collections.Generic.IList<object>
	// System.Collections.Generic.KeyValuePair<UnityEngine.Vector2Int,object>
	// System.Collections.Generic.KeyValuePair<int,object>
	// System.Collections.Generic.KeyValuePair<object,object>
	// System.Collections.Generic.KeyValuePair<ulong,object>
	// System.Collections.Generic.List.Enumerator<UnityEngine.Vector2Int>
	// System.Collections.Generic.List.Enumerator<object>
	// System.Collections.Generic.List<UnityEngine.Vector2Int>
	// System.Collections.Generic.List<object>
	// System.Collections.Generic.ObjectComparer<UnityEngine.Vector2Int>
	// System.Collections.Generic.ObjectComparer<object>
	// System.Collections.Generic.ObjectEqualityComparer<UnityEngine.Vector2Int>
	// System.Collections.Generic.ObjectEqualityComparer<int>
	// System.Collections.Generic.ObjectEqualityComparer<object>
	// System.Collections.Generic.ObjectEqualityComparer<ulong>
	// System.Collections.Generic.Queue.Enumerator<int>
	// System.Collections.Generic.Queue<int>
	// System.Collections.Generic.Stack.Enumerator<object>
	// System.Collections.Generic.Stack<object>
	// System.Collections.ObjectModel.ReadOnlyCollection<UnityEngine.Vector2Int>
	// System.Collections.ObjectModel.ReadOnlyCollection<object>
	// System.Comparison<UnityEngine.Vector2Int>
	// System.Comparison<object>
	// System.Predicate<UnityEngine.Vector2Int>
	// System.Predicate<object>
	// System.Predicate<ulong>
	// Unity.Netcode.FallbackSerializer<int>
	// Unity.Netcode.INetworkVariableSerializer<int>
	// Unity.Netcode.NetworkVariable.CheckExceedsDirtinessThresholdDelegate<int>
	// Unity.Netcode.NetworkVariable.OnValueChangedDelegate<int>
	// Unity.Netcode.NetworkVariable<int>
	// Unity.Netcode.NetworkVariableSerialization.EqualsDelegate<int>
	// Unity.Netcode.NetworkVariableSerialization<int>
	// Unity.Netcode.UnmanagedTypeSerializer<int>
	// Unity.Netcode.UserNetworkVariableSerialization.DuplicateValueDelegate<int>
	// Unity.Netcode.UserNetworkVariableSerialization.ReadDeltaDelegate<int>
	// Unity.Netcode.UserNetworkVariableSerialization.ReadValueDelegate<int>
	// Unity.Netcode.UserNetworkVariableSerialization.WriteDeltaDelegate<int>
	// Unity.Netcode.UserNetworkVariableSerialization.WriteValueDelegate<int>
	// UnityEngine.Events.UnityAction<object>
	// XEngine.Net.NetVaribale<int>
	// XEngine.Pool.ObjectPoolT<object>
	// XEngine.Utilities.MonoSingleton<object>
	// }}

	public void RefMethods()
	{
		// object System.Activator.CreateInstance<object>()
		// object[] System.Array.Empty<object>()
		// System.Void* Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf<int>(int&)
		// bool Unity.Netcode.NetworkVariableSerialization<int>.ValueEquals<int>(int&,int&)
		// System.Void Unity.Netcode.NetworkVariableSerializationTypes.InitializeEqualityChecker_UnmanagedValueEquals<int>()
		// System.Void Unity.Netcode.NetworkVariableSerializationTypes.InitializeSerializer_UnmanagedByMemcpy<int>()
		// object UnityEngine.Component.GetComponent<object>()
		// object UnityEngine.GameObject.AddComponent<object>()
		// object UnityEngine.GameObject.GetComponent<object>()
		// object XEngine.Pool.ObjectPoolT<object>.Get<object>()
		// object XEngine.Pool.ResHandle.GetObjT<object>()
	}
}