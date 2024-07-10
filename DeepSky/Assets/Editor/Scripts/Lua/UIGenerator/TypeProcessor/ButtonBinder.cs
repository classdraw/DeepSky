// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
//
// namespace UIGenerator
// {
//     [TypeBinder("btn",typeof(Button))]
//     public class ButtonBinder : TypeBinder
//     {
//         protected override List<DataBindingArgs> GenerateDataBindingArgsList()
//         {
//             List<DataBindingArgs> list = new List<DataBindingArgs>();
//             
//             list.Add(new DataBindingArgs
//             {
//                 ComponentMember = "enabled",
//                 ModuleMemberSuffix = "Enable",
//                 ModuleMemberType = typeof(bool),
//             });
//
//             return list;
//         }
//
//         protected override List<EventBindingArgs> GenerateEventBindingArgsList()
//         {
//             List<EventBindingArgs> list = new List<EventBindingArgs>();
//
//             list.Add(new EventBindingArgs
//             {
//                 ComponentEvent = "onClick",
//                 CtrlEventSuffix = "Click",
//                 CtrlEventParams = "",
//             });
//
//             return list;
//         }
//         
//         
//     }
// }