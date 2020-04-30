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
using BH.oM.Physical.Materials;
using BH.oM.Reflection.Attributes;

namespace BH.oM.Structure.SectionProperties.Reinforcement
{
    [ToBeRemoved("3.2", "Complete refactoring of reinforcement is being done, outside current possible scope of versioning. Old reinfrcement classes will be removed.")]
    public abstract class Reinforcement : BHoMObject
    {
        /***************************************************/
        /**** Properties                                ****/
        /***************************************************/

        public virtual double Diameter { get; set; }

        public virtual int BarCount { get; set; }

        public virtual Material Material { get; set; }

        public virtual double StartLocation { get; set; } = 0;

        public virtual double EndLocation { get; set; } = 1;


        /***************************************************/
    }
}

