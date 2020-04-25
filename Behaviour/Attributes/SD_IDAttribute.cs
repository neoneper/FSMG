using System;

namespace FSMG
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SD_IDAttribute : Attribute
    {
        private string _id;

        public string Id
        {
            get { return _id; }
        }

        /// <summary>
        /// Serializable field name for the property id
        /// </summary>
        /// <param name="id">Field name</param>
        public SD_IDAttribute(string id)
        {
            _id = id;
        }
    }
}