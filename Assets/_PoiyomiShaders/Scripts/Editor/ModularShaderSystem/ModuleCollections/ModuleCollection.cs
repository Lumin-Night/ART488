using System.Collections.Generic;
using UnityEngine;

namespace Poiyomi.ModularShaderSystem.CibbiExtensions
{
    [CreateAssetMenu(fileName = "ModuleCollection", menuName = MSSConstants.CREATE_PATH + "/Module Collection", order = 0)]
    public class ModuleCollection : ShaderModule
    {
        public List<ShaderModule> Modules;
    }
}