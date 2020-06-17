﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Vectrosity;
using KSP.UI;

namespace SystemHeat.UI
{
  public class OverlayLine
  {
    Color lineColor;
    VectorLine line;

    public OverlayLine(Transform parent, HeatLoop loop)
    {
      Utils.Log($"[OverlayLine]: building line for loop ID {loop.ID}");

      line = new VectorLine($"SystemHeat_Loop{loop.ID}_VectorLine", new List<Vector3>(), SystemHeatSettings.OverlayBaseLineWidth, LineType.Continuous, Joins.Weld);
      line.layer = 0;
      line.material = new Material(Shader.Find("GUI/Text Shader"));
      line.material.renderQueue = 3000;
      if (HighLogic.LoadedSceneIsEditor)
        VectorLine.SetCanvasCamera(EditorLogic.fetch.editorCamera);

      if (HighLogic.LoadedSceneIsFlight)
      {
        VectorLine.SetCamera3D(FlightCamera.fetch.mainCamera);
      }
      
      lineColor = SystemHeatSettings.GetLoopColor(loop.ID);
    }
    public void UpdatePositions(List<Vector3> positions)
    {
      if (line != null)
      {
        
        if (HighLogic.LoadedSceneIsEditor)
        {
          Utils.Log("Update posistions in editor");
          VectorLine.SetCanvasCamera(EditorLogic.fetch.editorCamera);
        }
        line.points3 = positions;
        line.SetColor(lineColor);

        line.Draw3D();
      }
    }
    
    public void UpdateGlow(float currentTemp, float nominalTemp)
    {
      if (currentTemp <= nominalTemp)
      {

      }
      else
      {
      }
    }

    public void SetVisible(bool visible)
    {
      line.active = visible;
    }
    public void Destroy()
    {
      VectorLine.Destroy(ref line);
    }


  }
}
