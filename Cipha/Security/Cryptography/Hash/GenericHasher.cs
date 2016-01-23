﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Cipha.Security.Cryptography;

namespace Cipha.Security.Cryptography.Hash
{
    /// <summary>
    /// GenericHasher provides an interface to
    /// interact with all classes deriving from
    /// System.Security.Cryptography.HashAlgoritm.
    /// 
    /// Possible classes include, but are not limited to:
    ///     SHA1Managed
    ///     SHA2Managed
    ///     SHA256Managed
    ///     SHA384Managed
    ///     SHA512Managed
    ///     MD5CryptoServiceProvider
    ///     
    /// located in the System.Security.Cryptography namespace.
    /// </summary>
    /// <typeparam name="T">A concrete hash algorithm deriving from HashAlgorithm</typeparam>
    public class GenericHasher<T>
        where T : System.Security.Cryptography.HashAlgorithm, new()
    {
        private Encoding encoding = Encoding.UTF8;
        /// <summary>
        /// The encoding which should be used.
        /// </summary>
        public Encoding Encoding
        {
            get { return encoding; }
            set
            {
                if (value == null)
                    throw new InvalidOperationException("encoding cannot be set to null");
                encoding = value;
            }
        }

        /// <summary>
        /// Computes a hash of the given string and
        /// returns the hash as a base64 string.
        /// </summary>
        /// <param name="stringToHash">The string to hash.</param>
        /// <returns>The hash as a base64 string.</returns>
        public string ComputeHashToString(string stringToHash)
        {
            return ComputeHashToString(encoding.GetBytes(stringToHash));
        }

        /// <summary>
        /// Computes a hash of the given byte array and
        /// returns the hash as a base64 string.
        /// </summary>
        /// <param name="bytesToHash">The data to hash.</param>
        /// <returns>The hash as a base64 string.</returns>
        public string ComputeHashToString(byte[] bytesToHash)
        {
            return Convert.ToBase64String(ComputeHash(bytesToHash));
        }

        /// <summary>
        /// Computes a hash of the given string
        /// and returns the hash as a string with
        /// the specified encoding.
        /// </summary>
        /// <param name="stringToHash">The string to hash.</param>
        /// <returns>The normal string</returns>
        public byte[] ComputeHash(string stringToHash)
        {
            return ComputeHash(encoding.GetBytes(stringToHash));
        }

        /// <summary>
        /// Computes a hash of the given byte array
        /// and returns the hash as a byte array.
        /// </summary>
        /// <param name="dataToHash">The data to hash.</param>
        /// <returns>The hashed values.</returns>
        public byte[] ComputeHash(byte[] dataToHash)
        {
            byte[] hashedValues = null;
            using(HashAlgorithm algo = new T())
            {
                hashedValues = algo.ComputeHash(dataToHash);
            }
            return hashedValues;
        }

        /// <summary>
        /// Computes 2 hashes of the strings and
        /// compare them bitwise.
        /// </summary>
        /// <param name="stringA"></param>
        /// <param name="stringB"></param>
        /// <returns></returns>
        public bool CompareHashes(string stringA, string stringB)
        {
            return CompareHashes(ComputeHash(stringA), ComputeHash(stringB));
        }

        /// <summary>
        /// Iterates through the byte arrays and comparing bitwise
        /// if the values of the arrays are equal.
        /// </summary>
        /// <param name="hashA">The first byte array.</param>
        /// <param name="hashB">The second byte array.</param>
        /// <returns></returns>
        public bool CompareHashes(byte[] hashA, byte[] hashB)
        {
            return Utilities.SlowEquals(hashA, hashB);
        }

        /// <summary>
        /// Computes a hash and transforms it to hex.
        /// </summary>
        /// <param name="stringToHash">The string to hash.</param>
        /// <param name="useLowercase">If the hex string should contain lowercase or uppercase letters.</param>
        /// <returns>The hex string.</returns>
        public string ComputeHashToHex(string stringToHash, bool useLowercase)
        {
            byte[] inputBytes = encoding.GetBytes(stringToHash);
            byte[] hash = ComputeHash(inputBytes);

            return HashToHex(hash, useLowercase);
        }

        /// <summary>
        /// Converts a given hash to a hex string.
        /// </summary>
        /// <param name="hashedBytes">The already hashed data.</param>
        /// <param name="useLowercase">If the hex string should contain lowercase or uppercase letters.</param>
        /// <returns>The hex string.</returns>
        public string HashToHex(byte[] hashedBytes, bool useLowercase)
        {
            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashedBytes.Length; i++)
            {
                if (useLowercase)
                    sb.Append(hashedBytes[i].ToString("x2"));
                else
                    sb.Append(hashedBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
