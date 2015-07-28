using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mime;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gravimage.Tests
{
    [TestClass]
    public class GravimageTest
    {
        [TestMethod]
        public void GetDefault()
        {
            Gravimage.Get("test@test.com").Should().NotBeEmpty();
        }

        [TestMethod]
        public void GetWithSize()
        {
            Gravimage.Get("test@test.com", 140).Should().Contain("s=140");
        }

        [TestMethod]
        public void GetWithRatingType()
        {
            Gravimage.Get("test@test.com", 80, Gravimage.RatingType.R).Should().Contain("r=r");
        }

        [TestMethod]
        public void GetWithDefaultImage()
        {
            Gravimage.Get("test@test.com", 80, Gravimage.RatingType.R, Gravimage.DefaultImage.Monsterid).Should().Contain("d=monsterid");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetWithNull()
        {
            Gravimage.Get(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetWithBigSize()
        {
            Gravimage.Get("test", 2050);
        }

        [TestMethod]
        public void CheckImageFromGravatar()
        {
            using (var client = new WebClient())
            {
                using (var strm = new MemoryStream(client.DownloadData(Gravimage.Get("test@test.com"))))
                {
                    var img = Image.FromStream(strm);
                    img.Width.Should().Be(80);
                    img.Height.Should().Be(80);
                }
            }
        }
    }
}
