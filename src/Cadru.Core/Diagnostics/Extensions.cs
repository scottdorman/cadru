//------------------------------------------------------------------------------
// <copyright file="Extensions.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2019 Scott Dorman.
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

using System;
using System.Text;

namespace Cadru.Diagnostics
{
    public static class Extensions
    {
        public static string Flatten(this Exception e, StringBuilder messages = null)
        {
            if (messages == null)
            {
                messages = new StringBuilder();
            }
            if (e != null)
            {
                messages.AppendLine(e.Message);

                if (e.InnerException != null)
                {
                    messages.AppendLine(Flatten(e.InnerException, messages));
                }
            }

            return messages.ToString();
        }
    }
}
