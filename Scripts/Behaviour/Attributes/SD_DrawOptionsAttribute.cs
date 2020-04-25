using System;

namespace FSMG
{
    /// <summary>
    /// Attribute used to force drawing a key as a property
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class SD_DrawOptionsAttribute : Attribute
    {
        private bool _canAdd = true;
        private bool _canRemove = true;
        private bool _canDrag = true;

        public bool CanAdd { get { return _canAdd; } }
        public bool CanRemove { get { return _canRemove; } }
        public bool CanDrag { get { return _canDrag; } }

        public SD_DrawOptionsAttribute(bool canAdd = true, bool canRemove = true, bool canDrag = true)
        {
            _canAdd = canAdd;
            _canRemove = canRemove;
            _canDrag = canDrag;
        }


    }
}