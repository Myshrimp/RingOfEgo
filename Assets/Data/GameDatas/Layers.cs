using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "scriptable", fileName = "layer")]
public class Layers :ScriptableObject
{
    public int id;
    public LayerMask layer_flying;
    public LayerMask layer_entity;
    public LayerMask layer_wall;
    public LayerMask layer_weapon;
    public LayerMask layer_ring;
    public LayerMask layer_all;
    public LayerMask layer_default;
}
