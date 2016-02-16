//------------------------------------------------------------------------------
// <copyright file="NetworkStatus.cs"
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

namespace Cadru.Net.NetworkInformation
{
    using System;
    using System.Net.NetworkInformation;


    /// <summary>
    /// Allows applications to receive notification when the network availability changes.
    /// </summary>
    public sealed class NetworkStatus : IDisposable
    {
        #region fields
        private static object syncRoot = new object();
        private ConnectionStatus connectionStatus;
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkStatus"/> class.
        /// </summary>
        public NetworkStatus()
        {
            bool online = NetworkInterface.GetIsNetworkAvailable();
            ConnectionStatus current = online ? ConnectionStatus.Connected : ConnectionStatus.Disconnected;

            lock (syncRoot)
            {
                if (this.connectionStatus != current)
                {
                    this.connectionStatus = current;
                }
            }

#if NET40 || NET45
            NetworkChange.NetworkAvailabilityChanged += NetworkAvailabilityChanged;
#else
            NetworkChange.NetworkAddressChanged += NetworkAddressChanged;
#endif
        }

        #endregion

        #region events
        #endregion

        #region events
        /// <summary>
        /// Represents the method that will handle the <see cref="NetworkStatus.NetworkStatusChanged"/> event.
        /// </summary>
        public event EventHandler<NetworkStatusChangedEventArgs> NetworkStatusChanged;

        #endregion

        #region properties

        #region ConnectivityStatus
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

        #region Dispose
        /// <summary>
        /// Releases all resources used by the <see cref="NetworkStatus"/>.
        /// </summary>
        public void Dispose()
        {
#if NET40 || NET45
            NetworkChange.NetworkAvailabilityChanged -= this.NetworkAvailabilityChanged;
#else
            NetworkChange.NetworkAddressChanged -= this.NetworkAddressChanged;
#endif
            GC.SuppressFinalize(this);
        }
        #endregion

#if NET40 || NET45
        #region NetworkAvailabilityChanged
        private void NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            ConnectionStatus current = e.IsAvailable ? ConnectionStatus.Connected : ConnectionStatus.Disconnected;
            ChangeNetworkStatus(current);
        }
        #endregion
#else
        private void NetworkAddressChanged(object sender, EventArgs e)
        {
            ConnectionStatus current = NetworkInterface.GetIsNetworkAvailable() ? ConnectionStatus.Connected : ConnectionStatus.Disconnected;
            ChangeNetworkStatus(current);
        }
#endif

        private void ChangeNetworkStatus(ConnectionStatus current)
        {
            bool changed = false;

            lock (syncRoot)
            {
                if (this.connectionStatus != current)
                {
                    this.connectionStatus = current;
                    changed = true;
                }
            }

            if (this.NetworkStatusChanged != null)
            {
                if (changed)
                {
                    NetworkStatusChangedEventArgs networkStatusChangedEventArgs = new NetworkStatusChangedEventArgs(current);
                    this.NetworkStatusChanged(this, networkStatusChangedEventArgs);
                }
            }
        }

        #endregion
    }
}
