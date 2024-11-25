using UnityEngine;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

public sealed class OrbSpawner : MonoBehaviour
{
    [SerializeField] GameObject _prefab = null;
    [SerializeField] float2 _extent = math.float2(10, 10);
    [SerializeField] uint2 _dimensions = math.uint2(10, 10);
    [SerializeField] Texture2D _texture = null;

    Color GetPixelFromTexture(float2 uv)
    {
        var p = math.float2(_texture.width - 1, _texture.height - 1) * uv;
        return _texture.GetPixel((int)p.x, (int)p.y);
    }

    void Start()
    {
        var origin = (float3)transform.position;

        for (var iy = 0; iy < _dimensions.y; iy++)
        {
            for (var ix = 0; ix < _dimensions.x; ix++)
            {
                var uv = math.float2(ix, iy) / (_dimensions - 1);

                var pos = origin + math.float3((uv - 0.5f) * _extent, 0).xzy;
                var go = Instantiate(_prefab, pos, quaternion.identity);

                var color = GetPixelFromTexture(uv);
                go.GetComponentInChildren<Light>().color = color;
                go.GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor", color);
            }
        }
    }
}
