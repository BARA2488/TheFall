using Godot;
using System;

public partial class Viniet : ColorRect
{
       public override void _Ready()
    {
        // Если скрипт висит на самом ColorRect
        if (Material is ShaderMaterial material)
        {
            material.SetShaderParameter("disable_glow", true);
        }
    }

}
