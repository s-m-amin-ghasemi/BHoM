/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2020, the respective contributors. All rights reserved.
 *
 * Each contributor holds copyright over their respective contributions.
 * The project versioning (Git) records all such contribution source information.
 *                                           
 *                                                                              
 * The BHoM is free software: you can redistribute it and/or modify         
 * it under the terms of the GNU Lesser General Public License as published by  
 * the Free Software Foundation, either version 3.0 of the License, or          
 * (at your option) any later version.                                          
 *                                                                              
 * The BHoM is distributed in the hope that it will be useful,              
 * but WITHOUT ANY WARRANTY; without even the implied warranty of               
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the                 
 * GNU Lesser General Public License for more details.                          
 *                                                                            
 * You should have received a copy of the GNU Lesser General Public License     
 * along with this code. If not, see <https://www.gnu.org/licenses/lgpl-3.0.html>.      
 */

using BH.oM.Base;
using BH.oM.Reflection.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BH.oM.Programming
{
    [Description("A syntax node corresponding to a code loop.")]
    public class LoopNode : BHoMObject, INode, IImmutable
    {
        /***************************************************/
        /**** Properties                                ****/
        /***************************************************/

        public virtual List<INode> InternalNodes { get; } = new List<INode>();

        public virtual string Description { get; set; } = "";

        public virtual List<DataParam> Outputs { get; set; } = new List<DataParam>();

        public virtual List<ReceiverParam> Inputs { get; set; } = new List<ReceiverParam>();

        public virtual bool IsInline { get; set; } = false;

        public virtual bool IsDeclaration { get; set; } = false;


        /***************************************************/
        /**** Constructors                              ****/
        /***************************************************/

        [Description("Returns a LoopNode to be used in a BHoM Syntax Tree.")]
        [Input("content", "List of syntax nodes to include in the loop.")]
        [Input("description", "Description of what this loop is doing.")]
        public LoopNode(List<INode> content, string description = "")
        {
            List<ReceiverParam> receivers = content.SelectMany(n => n.Inputs.Where(x => x.SourceId != Guid.Empty)).ToList();
            List<DataParam> emiters = content.SelectMany(n => n.Outputs.Where(x => x.TargetIds.Count > 0)).ToList();

            List<Guid> receiverIds = receivers.Select(x => x.BHoM_Guid).ToList();
            List<Guid> emiterIds = emiters.Select(x => x.BHoM_Guid).ToList();

            Outputs = emiters.Where(x => x.TargetIds.Intersect(receiverIds).Count() == 0)
                                        .Select(x =>
                                        {
                                            DataParam copy = x.GetShallowClone() as DataParam;
                                            copy.ParentId = this.BHoM_Guid;
                                            return copy;
                                        }).ToList();

            Inputs = receivers.Where(x => !emiterIds.Contains(x.SourceId))
                                        .Select(x =>
                                        {
                                            ReceiverParam copy = x.GetShallowClone() as ReceiverParam;
                                            copy.ParentId = this.BHoM_Guid;
                                            return copy;
                                        }).ToList();

            InternalNodes = content;
            Description = description;
        }

        /***************************************************/
    }
}
