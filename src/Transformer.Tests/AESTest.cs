﻿using System.IO;
using Transformer.Core.Cryptography;
using Transformer.Cryptography;
using Xunit;

namespace Transformer.Tests
{
    public class AESTest
    {
        const string Password = "test";
        const string TextToEncrypt = "blub";

        [Fact]
        public void Encrypt_From_Password()
        {
            var encryptedFromPassword = AES.Encrypt(TextToEncrypt, Password);
            var decripted = AES.Decrypt(encryptedFromPassword, Password);
            
            Assert.Equal(TextToEncrypt, decripted);
        }

        [Fact]
        public void Encrypt_From_PasswordFile()
        {
            var tempFileName = Path.GetTempFileName();

            File.WriteAllText(tempFileName, Password);
            var passwordFromFile = File.ReadAllText(tempFileName);

            var encryptedFromFile = AES.Encrypt(TextToEncrypt, passwordFromFile);
            var decrypted2 = AES.Decrypt(encryptedFromFile, passwordFromFile);
            var decrypted3 = AES.Decrypt(encryptedFromFile, passwordFromFile);
            var decrypted4 = AES.Decrypt(encryptedFromFile, Password);

            Assert.Equal(TextToEncrypt, decrypted2);
            Assert.Equal(TextToEncrypt, decrypted3);
            Assert.Equal(TextToEncrypt, decrypted4);

            Assert.Equal(Password, passwordFromFile);

            File.Delete(tempFileName);
        }
    }
}