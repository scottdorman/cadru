//------------------------------------------------------------------------------
// <copyright file="TrackedEntity.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2020 Scott Dorman.
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

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Cadru.Data.Dapper
{
    /// <summary>
    /// Provides access to change tracking information and operations for an entity.
    /// </summary>
    public abstract class TrackedEntity : INotifyPropertyChanged, INotifyPropertyChanging, IChangeTracking
    {
        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <inheritdoc/>
        public event PropertyChangingEventHandler? PropertyChanging;

        /// <inheritdoc/>
        public bool IsChanged
        {
            get; private set;
        }

        /// <inheritdoc/>
        public virtual void AcceptChanges()
        {
            this.IsChanged = false;
        }

        /// <summary>
        /// Raises the <see cref="INotifyPropertyChanged.PropertyChanged" /> event.
        /// </summary>
        /// <param name="propertyName">The name of the property which changed.</param>
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the <see cref="INotifyPropertyChanging.PropertyChanging" /> event.
        /// </summary>
        /// <param name="propertyName">The name of the property which changed.</param>
        protected void OnPropertyChanging([CallerMemberName] string? propertyName = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        /// Sets the property value and raises the <see cref="INotifyPropertyChanged.PropertyChanged" /> event
        /// if the value was changed.
        /// </summary>
        /// <typeparam name="T">The data type of the field.</typeparam>
        /// <param name="field">A reference to the field which will be changed.</param>
        /// <param name="value">The new value of the field.</param>
        /// <param name="propertyName">The name of the property being changed.</param>
        /// <returns><see langword="true" /> if the property value was changed; otherwise, <see langword="false" />.</returns>
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            var propertyChanged = false;
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                this.OnPropertyChanging(propertyName);
                field = value;
                this.IsChanged = propertyChanged = true;
                this.OnPropertyChanged(propertyName);
            }

            return propertyChanged;
        }
    }
}