﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cipha.Security.Cryptography;

namespace Cipha.Tests.Security.Cryptography
{
    [TestClass]
    public class UtilitiesTests
    {
        [TestMethod]
        public void SlowEquals_CompareArrays_Pass()
        {
            byte[] arr = {
                             1,2,3,4,5,6,7,8,9,0
                         };
            byte[] finalArr = {
                                  0,0,0,0,0,0,0,0,0,0
                              };

            Utilities.SetArrayValuesZero(arr);

            Assert.IsTrue(Utilities.SlowEquals(finalArr, arr));
        }

        [TestMethod]
        public void SetArrayValuesZero_WipeIntArray_Pass()
        {
            int[] arr = {
                             1,2,3,4,5,6,7,8,9,0
                         };
            int[] finalArr = {
                                  0,0,0,0,0,0,0,0,0,0
                              };

            Utilities.SetArrayValuesZero(arr);

            CollectionAssert.AreEqual(finalArr, arr);
        }

        [TestMethod]
        public void SetArrayValuesEmpty_WipeStringArray_Pass()
        {
            string[] arr = {
                            "hello", "my", "friend"
                            };
            string[] finalArr = {
                                  "", "", ""
                              };

            Utilities.SetArrayValuesEmpty(arr);

            CollectionAssert.AreEqual(finalArr, arr);
        }
    }
}
