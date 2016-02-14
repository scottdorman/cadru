//------------------------------------------------------------------------------
// <copyright file="RetryDelegatingHandler.cs"
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

namespace Cadru.Net.Http
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Cadru.Net.Extensions;

    /// <summary>
    /// An HTTP handler that allows a request to be retried.
    /// </summary>
    [CLSCompliant(false)]
    public class CompressingHttpClientHandler : HttpClientHandler
    {
        #region fields
        #endregion

        #region constructors

        #region CompressingHttpClientHandler()
        /// <summary>
        /// Initializes a new instance of the <see cref="CompressingHttpClientHandler"/> class
        /// with a specific inner handler.
        /// </summary>
        public CompressingHttpClientHandler()
            : base()
        {
            if (this.SupportsAutomaticDecompression)
            {
                this.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }
        }
        #endregion

        #endregion

        #region properties
        #endregion

        #region methods
        #endregion
    }
}