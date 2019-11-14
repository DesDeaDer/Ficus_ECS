using UnityEngine;
using Unity.Entities;
using Unity.Rendering;
using System.Linq;

public class ChangeColorSystem : ComponentSystem
{
    private ColorSettings ColorSettings => GamePoint.Instance.ColorSettings;
    private Material GetMaterial(Color color) => GamePoint.Instance.GetMaterial(color);

    protected override void OnUpdate()
    {
        Entities.WithAll<ChangeColorEvent>().ForEach((Entity entity, RenderMesh renderMesh, ref ClickCount clickCount) =>
        {
            renderMesh.material = GetMaterial(GetColor(clickCount.Count));
            EntityManager.SetSharedComponentData(entity, renderMesh);

            EntityManager.RemoveComponent<ChangeColorEvent>(entity);
        });
    }

    private Color GetColor(int count)
    {
        var colorSetting = ColorSettings.Values.FirstOrDefault(x => count % x.Multiple == 0);
        if (colorSetting != null)
        {
            return colorSetting.Color;
        }

        return ColorSettings.ColorDefault;
    }
}
