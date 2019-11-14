using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class GamePoint : MonoBehaviour
{
    [SerializeField] private Mesh _mesh;
    [SerializeField] private Material _material;
    [SerializeField] private Camera _camera;
    [SerializeField] private ColorSettings _colorSettings;

    public static GamePoint Instance { get; private set; }

    private Dictionary<Color, Material> Materials = new Dictionary<Color, Material>();
    public Material GetMaterial(Color color)
    {
        if (!Materials.TryGetValue(color, out var material))
        {
            material = new Material(_material) { color = color };
        }

        return material;
    }

    public Camera Camera => _camera;
    public ColorSettings ColorSettings => _colorSettings;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        var entityManager = World.Active.EntityManager;
        var entityArchitecture = entityManager.CreateArchetype
        (
            typeof(RenderMesh),
            typeof(LocalToWorld),
            typeof(Translation),
            typeof(ClickCount)
        );

        NativeArray<Entity> entities = new NativeArray<Entity>(200, Allocator.Temp);

        entityManager.CreateEntity(entityArchitecture, entities);

        var renderMesh = new RenderMesh() { mesh = _mesh, material = _material };

        foreach (var item in entities)
        {
            entityManager.SetSharedComponentData(item, renderMesh);
            entityManager.SetComponentData(item, new Translation() { Value = UnityEngine.Random.insideUnitSphere * UnityEngine.Random.Range(2, 10) });
        }
    }
}
