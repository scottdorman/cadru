//------------------------------------------------------------------------------
// <copyright file="NetworkStatusChangedEventArgs.cs" 
//  company="Scott Dorman" 
//  library="Cadru">
//    Copyright (C) 2001-2013 Scott Dorman.
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

namespace Cadru.Networking
{
    using System;

    /// <summary>
    /// Provides event data for the <see cref="NetworkStatus.NetworkStatusChanged"/> event.
    /// </summary>
    public class NetworkStatusChangedEventArgs : EventArgs
    {
        #region fields
        private ConnectionStatus connectionStatus;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkStatusChangedEventArgs"/> class.
        /// </summary>
        /// <param name="status">The event data representing the current connection status.</param>
        public NetworkStatusChangedEventArgs(ConnectionStatus status)
        {
            this.connectionStatus = status;
        }
        #endregion

        #region events
        #endregion

        #region properties

        #region ConnectionStatus
        /// <summary>
        /// Gets the current network connection status.
        /// </summary>
        /// <value>The current network connection status.</value>
        public ConnectionStatus ConnectionStatus
        {
            get
            {
                return this.connectionStatus;
            }
        }
        #endregion

        #endregion

        #region methods

        #endregion
    }
}
