using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LockCameraY : CinemachineExtension
{

    public float m_x = 10;

    protected override void PostPipelineStageCallback(
         CinemachineVirtualCameraBase vcam,
         CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            pos.x = m_x;
            state.RawPosition = pos;
        }
    }
}
