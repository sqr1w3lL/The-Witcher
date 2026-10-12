using NavMeshPlus.Components;
using System.Collections;
using UnityEngine;

public class NavMeshSurfaceManagement : MonoBehaviour
{
    public static NavMeshSurfaceManagement Instance { get; private set; }

    private NavMeshSurface _navMeshSurface;
    private Coroutine _rebakeCoroutine;

    private void Awake()
    {
        Instance = this;
        _navMeshSurface = GetComponent<NavMeshSurface>();
        _navMeshSurface.hideEditorLogs = true;
    }

    public void RebakeNavMeshSurface()
    {
        _rebakeCoroutine ??= StartCoroutine(RebakeNextFrame());
    }
    
    private IEnumerator RebakeNextFrame()
    {
        yield return new WaitForEndOfFrame();

        _navMeshSurface.BuildNavMesh();
        _rebakeCoroutine = null;
    }
}
