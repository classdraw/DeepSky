using System.Collections.Generic;
using UnityEngine.UI;

namespace UIGenerator
{
    [TypeBinder("text",typeof(Text))]
    public class TextBinder : TypeBinder
    {
        protected override List<DataBindingArgs> GenerateDataBindingArgsList()
        {
            List<DataBindingArgs> list = new List<DataBindingArgs>();
            
            list.Add(new DataBindingArgs
            {
                ComponentMember = "text",
                ModuleMemberSuffix = "Text",
                ModuleMemberType = typeof(string),
            });
            list.Add(new DataBindingArgs
            {
                ComponentMember = "enabled",
                ModuleMemberSuffix = "Enable",
                ModuleMemberType = typeof(bool),
            });

            return list;
        }
    }
}