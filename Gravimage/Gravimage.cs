using System;

namespace Gravimage
{
    /// <summary>
    /// Implement https://secure.gravatar.com/site/implement/images/
    /// </summary>
    public static class Gravimage
    {
        private const string Url = "https://secure.gravatar.com/avatar.php";

        public enum RatingType
        {
            /// <summary>
            /// suitable for display on all websites with any audience type
            /// </summary>
            G,
            /// <summary>
            /// may contain rude gestures, provocatively dressed individuals, the lesser swear words, or mild violence.
            /// </summary>
            PG,
            /// <summary>
            /// may contain such things as harsh profanity, intense violence, nudity, or hard drug use.
            /// </summary>
            R,
            /// <summary>
            /// may contain hardcore sexual imagery or extremely disturbing violence.
            /// </summary>
            X
        }

        public enum DefaultImage
        {
            /// <summary>
            /// (mystery-man) a simple, cartoon-style silhouetted outline of a person (does not vary by email hash)
            /// </summary>
            MM,
            /// <summary>
            ///  a geometric pattern based on an email hash
            /// </summary>
            Identicon,
            /// <summary>
            /// a generated 'monster' with different colors, faces, etc
            /// </summary>
            Monsterid,
            /// <summary>
            /// generated faces with differing features and backgrounds
            /// </summary>
            Wavatar,
            /// <summary>
            /// awesome generated, 8-bit arcade-style pixelated faces
            /// </summary>
            Retro,
            /// <summary>
            /// a transparent PNG image (border added to HTML below for demonstration purposes)
            /// </summary>
            Blank
        }

        // http://stackoverflow.com/q/424366
        /// <summary>
        /// Gets the link to avatar by gravatar.
        /// </summary>
        /// <param name="content">The content (email).</param>
        /// <param name="size">The size of avatar.</param>
        /// <param name="ratingType">Type of the rating.</param>
        /// <param name="defaultImage">The type of default image.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">content</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">size;The image size should be between 1 and 2048</exception>
        public static string Get(string content, int size = 80, RatingType ratingType = RatingType.PG, DefaultImage defaultImage = DefaultImage.MM)
        {
            if (content == null) throw new ArgumentNullException("content");

            if (size < 1 | size > 2048)
                throw new ArgumentOutOfRangeException("size",
                    "The image size should be between 1 and 2048");

            return String.Format("{0}?gravatar_id={1}&s={2}&r={3}&d={4}",
                Url,
                MD5.GetHashString(content.ToLower()),
                size,
                ratingType.ToString("G"),
                defaultImage.ToString("G")).ToLower();
        }
    }
}
