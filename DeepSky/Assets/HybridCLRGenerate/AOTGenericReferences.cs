using System.Collections.Generic;
public class AOTGenericReferences : UnityEngine.MonoBehaviour
{

	// {{ AOT assemblies
	public static readonly IReadOnlyList<string> PatchedAOTAssemblyList = new List<string>
	{
		"mscorlib.dll",
	};
	// }}

	// {{ constraint implement type
	// }} 

	// {{ AOT generic types
	// System.Action<object>
	// }}

	public void RefMethods()
	{
	}
}