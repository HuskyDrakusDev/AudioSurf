﻿// Copyright (C) 2015 Jaroslav Stehlik - All Rights Reserved
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SVGImporter 
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(ISVGRenderer))]
    [AddComponentMenu("Rendering/SVG Modifiers/Twirl Modifier", 22)]
    public class SVGTwirlModifier : SVGModifier {

        public Transform center;
        public float radius;
        public float intensity;

        protected override void PrepareForRendering (SVGLayer[] layers, SVGAsset svgAsset, bool force) 
        {
            if(center == null) return;
            Vector2 position = center.position;
            Vector2 direction, directionNormalized;
            float distance;

            position = transform.InverseTransformPoint(position);

            float vortexIntensity = intensity / (Mathf.PI * 2f);
                
            if(layers == null) return;
            int totalLayers = layers.Length;
            if(!useSelection)
            {
                for(int i = 0; i < totalLayers; i++)
                {
                    if(layers[i].shapes == null) continue;
                    int shapesLength = layers[i].shapes.Length;
                    for(int j = 0; j < shapesLength; j++)
                    {
                        int vertexCount = layers[i].shapes[j].vertexCount;
                        for(int k = 0; k < vertexCount; k++)
                        {
                            direction.x = layers[i].shapes[j].vertices[k].x - position.x;
                            direction.y = layers[i].shapes[j].vertices[k].y - position.y;
                            distance = Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y);

                            float theta = Mathf.Atan2(direction.y, direction.x);
                            theta += vortexIntensity * (1f - Mathf.Clamp01(distance / radius));
                            directionNormalized.x = Mathf.Cos(theta) * distance + position.x;
                            directionNormalized.y = Mathf.Sin(theta) * distance + position.y;
                            
                            layers[i].shapes[j].vertices[k] = directionNormalized;
                        }
                    }
                }
            } else {
                for(int i = 0; i < totalLayers; i++)
                {
                    if(layers[i].shapes == null) continue;
                    if(!layerSelection.Contains(i)) continue;

                    int shapesLength = layers[i].shapes.Length;
                    for(int j = 0; j < shapesLength; j++)
                    {
                        int vertexCount = layers[i].shapes[j].vertexCount;
                        for(int k = 0; k < vertexCount; k++)
                        {
                            direction.x = layers[i].shapes[j].vertices[k].x - position.x;
                            direction.y = layers[i].shapes[j].vertices[k].y - position.y;
                            distance = Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y);
                            
                            float theta = Mathf.Atan2(direction.y, direction.x);
                            theta += vortexIntensity * (1f - Mathf.Clamp01(distance / radius));
                            directionNormalized.x = Mathf.Cos(theta) * distance + position.x;
                            directionNormalized.y = Mathf.Sin(theta) * distance + position.y;
                            
                            layers[i].shapes[j].vertices[k] = directionNormalized;
                        }
                    }
                }
            }
        }
    }
}
