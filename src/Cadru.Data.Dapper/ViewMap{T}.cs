//------------------------------------------------------------------------------
// <copyright file="TableMap.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2015 Scott Dorman.
// </copyright>
//
// <license>
//    Licensed under the Microsoft Public License (Ms-PL) (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//    http://opensource.org/licenses/Ms-PL.html
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </license>
//------------------------------------------------------------------------------

namespace Cadru.Data.Dapper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public class ViewMap<T> : ObjectMap<T> where T : class
    {
        internal ViewMap() : base()
        {
            this.ObjectType = DatabaseObjectType.View;
            var viewAttribute = this.EntityType.GetCustomAttribute<ViewAttribute>(inherit: true);
            if (viewAttribute != null)
            {
                base.Schema = viewAttribute.Schema;
                this.ObjectName = viewAttribute.Name;
            }
            else
            {
                this.Schema = String.Empty;
                this.ObjectName = this.EntityType.Name;
            }

            this.FullyQualifiedObjectName = this.GetFullyQualifiedObjectName();
            this.AutoMap();
        }
    }
}
