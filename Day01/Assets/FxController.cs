using System.Linq;
using UnityEngine;

public class FxController : MonoBehaviour
{
    private void Awake()
    {
        var radius = 20f;
        for (int numSpheres = 0; numSpheres < 3; numSpheres++)
        {
            //var radius = Random.Range(5f, 20f);
            var sphereColor = Random.ColorHSV();

            for (int numStripes = 0; numStripes < radius * 3; numStripes++)
            {
                var start = radius * Random.insideUnitSphere;
                var end = radius * Random.insideUnitSphere;
                for (int i = 0; i >= 0; i--)
                {
                    var stripe = new GameObject().AddComponent<LineRenderer>();

                    var numPoints = (int)radius * 5;
                    var points = Enumerable
                        .Range(1, numPoints)
                        .Select(index =>
                        {
                            var noise = 0 * 0.2f * 2 * (Random.value - 0.5f);
                            var originalVector = Vector3.Slerp(start, end, index / (float)numPoints);
                            return originalVector + noise * originalVector.normalized;
                        })
                        .ToArray();

                    stripe.material = new Material(Shader.Find("Unlit/Wireframe"));
                    stripe.positionCount = points.Length;
                    stripe.SetPositions(points);

                    var color = new Color(sphereColor.r, sphereColor.g, sphereColor.b, 1f);
                    stripe.startColor = color;
                    stripe.endColor = color;
                    stripe.widthMultiplier = 0.3f;
                    ; ; stripe.material.SetColor("_FrontColor", color);
                }
            }

            radius = Mathf.Pow(radius, 0.75f);
            //radius /= 2f;
        }
    }
}
