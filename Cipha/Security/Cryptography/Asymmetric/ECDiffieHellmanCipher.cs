﻿using Cipha.Security.Cryptography;
using Cipha.Security.Cryptography.Asymmetric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cipha.Tests.Security.Cryptography.Asymmetric
{
    public class ECDiffieHellmanCipher<T> : AsymmetricCipher<T>
        where T : ECDiffieHellman, new()
    {
        CngKeyBlobFormat blobFormat = CngKeyBlobFormat.EccPublicBlob;

        public ECDiffieHellmanCipher()
        {
            algo = new T();
        }
        public ECDiffieHellmanCipher(T referenceAlgo)
        {
            algo = referenceAlgo;
        }
        public ECDiffieHellmanCipher(int keySize)
        {
            algo = (T)Activator.CreateInstance(typeof(T), keySize);
        }
        // Evaluate need for constructor with xmlString parameter

        public ECDiffieHellmanCipher(ECDiffieHellmanAgreement agreement)
        {
            algo = new T();
            if(algo is ECDiffieHellmanCng)
            {
                ECDiffieHellmanCng currAlgo = algo as ECDiffieHellmanCng;
                currAlgo.HashAlgorithm = agreement.Algorithm;
                currAlgo.KeyDerivationFunction = agreement.Function;
                blobFormat = agreement.BlobFormat;
            }
        }
        public ECDiffieHellmanPublicKey PublicKey
        {
            get
            {
                return algo.PublicKey;
            }
        }

        public byte[] DeriveKey(ECDiffieHellmanPublicKey otherPublicKey)
        {
            return algo.DeriveKeyMaterial(otherPublicKey);
        }

        public ECDiffieHellmanAgreement Agreement
        {
            get
            {
                if(algo is ECDiffieHellmanCng)
                {
                    var curAlgo = algo as ECDiffieHellmanCng;
                    var agreeM = new ECDiffieHellmanAgreement(curAlgo.KeyDerivationFunction, curAlgo.HashAlgorithm, blobFormat);
                    return agreeM;
                    
                }

                throw new NotSupportedException();
            }
        }

        public override byte[] SignData<U>(byte[] dataToSign)
        {
            throw new NotSupportedException();
        }

        public override bool VerifyData<U>(byte[] dataToVerify, byte[] signedData)
        {
            throw new NotSupportedException();
        }

        public override byte[] SignHash(byte[] hashToSign)
        {
            throw new NotSupportedException();
        }

        public override bool VerifyHash(byte[] hashToVerify, byte[] signedHash)
        {
            throw new NotSupportedException();
        }
    }
}
