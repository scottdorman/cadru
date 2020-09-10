//------------------------------------------------------------------------------
// <copyright file="ImageEmbedder.cs"
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Cadru.Postal
{
    /// <summary>
    /// Used by the <see cref="HtmlExtensions.EmbedImage"/> helper method. It
    /// generates the <see cref="LinkedResource"/> objects need to embed images
    /// into an email.
    /// </summary>
    public class ImageEmbedder
    {
        internal static string ViewDataKey = "Cadru.Postal.ImageEmbedder";

        private readonly Func<string, LinkedResource> createLinkedResource;

        private readonly Dictionary<string, LinkedResource> images = new Dictionary<string, LinkedResource>();

        /// <summary>
        /// Creates a new <see cref="ImageEmbedder"/>.
        /// </summary>
        public ImageEmbedder()
        {
            this.createLinkedResource = CreateLinkedResource;
        }

        /// <summary>
        /// Creates a new <see cref="ImageEmbedder"/>.
        /// </summary>
        /// <param name="createLinkedResource">
        /// A delegate that creates a <see cref="LinkedResource"/> from an image
        /// path or URL.
        /// </param>
        public ImageEmbedder(Func<string, LinkedResource> createLinkedResource)
        {
            this.createLinkedResource = createLinkedResource;
        }

        /// <summary>
        /// Gets if any images have been referenced.
        /// </summary>
        public bool HasImages => this.images.Count > 0;

        /// <summary>
        /// Creates a <see cref="LinkedResource"/> from an image path or URL.
        /// </summary>
        /// <param name="imagePathOrUrl">The image path or URL.</param>
        /// <returns>A new <see cref="LinkedResource"/></returns>
        public static LinkedResource CreateLinkedResource(string imagePathOrUrl)
        {
            if (Uri.IsWellFormedUriString(imagePathOrUrl, UriKind.Absolute))
            {
                var client = new WebClient();
                var bytes = client.DownloadData(imagePathOrUrl);
                return new LinkedResource(new MemoryStream(bytes));
            }
            else
            {
                return new LinkedResource(File.OpenRead(imagePathOrUrl));
            }
        }

        /// <summary>
        /// Adds recorded <see cref="LinkedResource"/> image references to the
        /// given <see cref="AlternateView"/>.
        /// </summary>
        public void AddImagesToView(AlternateView view)
        {
            foreach (var image in this.images)
            {
                view.LinkedResources.Add(image.Value);
            }
        }

        /// <summary>
        /// Records a reference to the given image.
        /// </summary>
        /// <param name="imagePathOrUrl">The image path or URL.</param>
        /// <param name="contentType">
        /// The content type of the image e.g. "image/png". If null, then
        /// content type is determined from the file name extension.
        /// </param>
        /// <returns>
        /// A <see cref="LinkedResource"/> representing the embedded image.
        /// </returns>
        public LinkedResource ReferenceImage(string imagePathOrUrl, string contentType = null)
        {
            LinkedResource resource;
            if (this.images.TryGetValue(imagePathOrUrl, out resource)) return resource;

            resource = this.createLinkedResource(imagePathOrUrl);

            contentType = contentType ?? this.DetermineContentType(imagePathOrUrl);
            if (contentType != null)
            {
                resource.ContentType = new ContentType(contentType);
            }

            this.images[imagePathOrUrl] = resource;
            return resource;
        }

        private string DetermineContentType(string pathOrUrl)
        {
            if (pathOrUrl == null) throw new ArgumentNullException(nameof(pathOrUrl));

            var extension = Path.GetExtension(pathOrUrl).ToLowerInvariant();
            switch (extension)
            {
                case ".png":
                    return "image/png";

                case ".jpeg":
                case ".jpg":
                    return "image/jpeg";

                case ".gif":
                    return "image/gif";

                default:
                    return null;
            }
        }
    }
}