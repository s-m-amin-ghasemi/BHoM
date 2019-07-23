﻿/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2019, the respective contributors. All rights reserved.
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.oM.Diffing
{
    public class Delta : IObject
    {
        /***************************************************/
        /**** Properties                                ****/
        /***************************************************/

        public List<IBHoMObject> ToCreate { get; private set; }
        public List<string> ToCreate_hashes { get; private set; }

        public List<IBHoMObject> ToDelete { get; private set; }
        public List<string> ToDelete_hashes { get; private set; }

        public List<IBHoMObject> ToUpdate { get; private set; }
        public List<string> ToUpdate_hashes { get; private set; }

        public DiffProjFragment DiffingProject { get; private set;}

        public long Timestamp { get; private set; }
        public string Author { get; private set; }

        /***************************************************/

        /***************************************************/
        /**** Constructors                              ****/
        /***************************************************/     

        public Delta(DiffProjFragment diffingProject, List<IBHoMObject> toCreate)
        {
            ToCreate = toCreate;

            DiffingProject = diffingProject;
            DiffingProject.Revision += 1;

            Timestamp = DateTime.UtcNow.Ticks;
            Author = Environment.UserDomainName + "/" + Environment.UserName;

            if (ToCreate_hashes == null)
                ToCreate_hashes = GetHashes(toCreate);
        }

        public Delta(DiffProjFragment diffingProject, List<IBHoMObject> toCreate, List<IBHoMObject> toDelete, List<IBHoMObject> toUpdate)
            : this(diffingProject, toCreate)
        {
            ToDelete = toDelete;
            ToUpdate = toUpdate;

            if (ToDelete_hashes == null && toDelete != null)
                ToDelete_hashes = GetHashes(toDelete);
            if (ToUpdate_hashes == null && ToUpdate != null)
                ToUpdate_hashes = GetHashes(toUpdate);
        }

        public Delta(DiffProjFragment diffingProject, List<IBHoMObject> toCreate, List<string> toCreate_hashes, List<IBHoMObject> toDelete, List<string> toDelete_hashes, List<IBHoMObject> toUpdate, List<string> toUpdate_hashes)
            : this(diffingProject, toCreate, toDelete, toUpdate)
        {
            ToCreate_hashes = toCreate_hashes;
            ToDelete_hashes = toDelete_hashes;
            ToUpdate_hashes = toUpdate_hashes;
        }

        /***************************************************/


        /***************************************************/
        /**** Private methods                           ****/
        /***************************************************/

        private List<string> GetHashes(List<IBHoMObject> objs)
        {
            return objs.Select(obj => obj.Fragments.OfType<DiffHashFragment>().First().Hash).ToList();
        }

        /***************************************************/

    }
}
