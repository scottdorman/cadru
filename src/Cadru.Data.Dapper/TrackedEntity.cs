//------------------------------------------------------------------------------
// <copyright file="TrackedEntity.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2017 Scott Dorman.
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
    using System.Collections.Generic;
    using System.ComponentModel;

    public abstract class TrackedEntity : INotifyPropertyChanged, IChangeTracking
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public bool IsChanged
        {
            get; private set;
        }

        public void AcceptChanges()
        {
            this.IsChanged = false;
        }

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool Set<T>(ref T field, T newValue, string propertyName)
        {
            var propertyChanged = false;
            if (!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                propertyChanged = true;
                this.IsChanged = true;
                field = newValue;
                this.RaisePropertyChanged(propertyName);
            }

            return propertyChanged;
        }
    }
}
