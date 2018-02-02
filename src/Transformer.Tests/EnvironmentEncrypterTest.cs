﻿using Moq;
using Transformer;
using Transformer.Core;
using Transformer.Model;
using Xunit;

namespace Transformer.Tests
{
    public class EnvironmentEncrypterTest
    {
        [Fact]
        public void Encrypt_Environment()
        {
            var env = new Environment();
            env.Variables.Add(new Variable() { DoEncrypt = true, Name = "encrypted-var", Value = "secret" });
            env.Variables.Add(new Variable() { DoEncrypt = false, Name = "unsecure", Value = "unsecure" });

            var environmentProviderMock = new Mock<IEnvironmentProvider>();
            environmentProviderMock.Setup(prov => prov.GetEnvironment(It.IsAny<string>())).Returns(env);

            var encrypter = new EnvironmentEncrypter(environmentProviderMock.Object, "password");
            encrypter.EncryptAllEnvironments();
        }

        [Fact]
        public void Encrypt_Variable()
        {
            var env = new Environment();
            env.Variables.Add(new Variable() { DoEncrypt = true, Name = "encrypted-var", Value = "secret" });
            env.Variables.Add(new Variable() { DoEncrypt = true, Name = "another-secret-pw", Value = "secret" });
            env.Variables.Add(new Variable() { DoEncrypt = false, Name = "unsecure", Value = "unsecure" });

            env.EncryptVariables("gugus");

            Assert.NotEqual("secret", env["encrypted-var"].Value);
            Assert.NotEqual("secret", env["another-secret-pw"].Value);
        }

        [Fact]
        public void Decrypt_Variable()
        {
            var env = new Environment();
            env.Variables.Add(new Variable() { DoEncrypt = true, Name = "one", Value = "1" });
            env.Variables.Add(new Variable() { DoEncrypt = true, Name = "two", Value = "2" });
            env.EncryptVariables("super-duper-secret-pw!");

            Assert.NotEqual("1", env["one"].Value);
            Assert.NotEqual("2", env["two"].Value);

            env.DecryptVariables("super-duper-secret-pw!");

            Assert.Equal("1", env["one"].Value);
            Assert.Equal("2", env["two"].Value);
        }

        [Fact]
        public void Decrypt_Variable_WrongPassowrd()
        {
            var env = new Environment();
            env.Variables.Add(new Variable { DoEncrypt = true, Name = "firstname", Value = "Raymond" });
            env.Variables.Add(new Variable { DoEncrypt = true, Name = "lastname", Value = "Redington" });
            env.EncryptVariables("super-duper-secret-pw!");

            env.DecryptVariables("wrong-pw!");

            Assert.NotEqual("Raymond", env["firstname"].Value);
            Assert.NotEqual("Redington", env["lastname"].Value);
        }

        [Fact]
        public void Change_Password()
        {
            // arrange
            var env = new Environment();
            env.Variables.Add(new Variable { DoEncrypt = true, Name = "firstname", Value = "tobi" });
            env.EncryptVariables("first-password!");

            // act
            env.ChangePassword("first-password!", "2nd-password!");

            // assert
            env.DecryptVariables("2nd-password!");
            Assert.Equal(env["firstname"].Value, "tobi");
        }
    }
}