using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Gravimage
{
    // https://secure.gravatar.com/site/implement/images/
    public static class Gravimage
    {
        private const string Url = "https://secure.gravatar.com/avatar.php?gravatar_id=";

        public enum RatingType
        {
            G,
            PG,
            R,
            X
        }

        // http://stackoverflow.com/q/424366
        public static string Get(string email, int size = 80, RatingType ratingType = RatingType.PG)
        {
            if (size < 1 | size > 2048)
                throw new ArgumentOutOfRangeException("size",
                    "The image size should be between 1 and 2048");

            return Url +  MD5.GetHashString(email.ToLower()).ToLower()
                + "&s=" + size + "&r=" + Enum.GetName(typeof(RatingType), ratingType).ToLower() + "&d=mm";
        }
    }
}
