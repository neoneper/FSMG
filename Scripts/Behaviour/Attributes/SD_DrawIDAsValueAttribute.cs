using System;

namespace FSMG
{
    /// <summary>
    /// Attribute used to force drawing a key as a property
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class SD_DrawIDAsValueAttribute : Attribute { }
}