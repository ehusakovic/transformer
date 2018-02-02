﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Transformer;
using Transformer.Model;
using Xunit;
using Environment = Transformer.Model.Environment;

namespace Transformer.Tests
{
    public class EnvironmentSerializationTests
    {
        // TODO: xml serializer
        ////[Fact]
        ////public void Serialize_Environment_Test()
        ////{
        ////    var target = new Environment();
        ////    target.Name = "Local";
        ////    target.Description = "Used for unit tests, not a real environment";

        ////    target.Variables = new List<Variable>();
        ////    target.Variables.Add(new Variable() { Name = "Name", Value = "Tobi" });
        ////    target.Variables.Add(new Variable() { Name = "Jack", Value = "Bauer" });

        ////    var xml = new StringWriter();

        ////    var serializer = new XmlSerializer(typeof(Environment));
        ////    serializer.Serialize(xml, target);

        ////    Console.WriteLine(xml);
        ////}

        // TODO XmlSerializer
        ////[Test]
        ////public void Deserialize_Environment_Test()
        ////{
        ////    var xml = @"<?xml version=""1.0""?>
        ////                <environment name=""local"" description=""Used for unit tests, not a real environment"">
        ////                  <variable name=""Name"" value=""Tobi"" />
        ////                  <variable name=""Jack"" value=""Bauer"" />
        ////                </environment>";

        ////    var serializer = new XmlSerializer(typeof(Environment));
        ////    var result = serializer.Deserialize(new XmlTextReader(new StringReader(xml))) as Environment;

        ////    Assert.AreEqual("local", result.Name);

        ////    Assert.AreEqual("Name", result.Variables[0].Name);
        ////    Assert.AreEqual("Jack", result.Variables[1].Name);
        ////    Assert.AreEqual("Tobi", result.Variables[0].Value);
        ////    Assert.AreEqual("Bauer", result.Variables[1].Value);
        ////}

        ////[Test]
        ////public void Deserialize_Environment_Urlencoded_Test()
        ////{
        ////    var xml = @"<?xml version=""1.0""?>
        ////                <environment name=""local"" description=""Used for unit tests, not a real environment"">
        ////                  <variable name=""Name1"" value=""Tobi &amp; Oli"" />
        ////                  <variable name=""Name"" value=""http://stagingreporting.ontimehuv.com/homeCustomMessages?guid={0}&amp;type={1}&amp;languageId={2}"" />
        ////                </environment>";

        ////    var serializer = new XmlSerializer(typeof(Environment));
        ////    var result = serializer.Deserialize(new XmlTextReader(new StringReader(xml))) as Environment;

        ////    Assert.AreEqual("local", result.Name);

        ////    Console.WriteLine(result.Variables[1].Value);

        ////    Assert.AreEqual("Name1", result.Variables[0].Name);
        ////    Assert.AreEqual("Tobi & Oli", result.Variables[0].Value);
        ////}

        ////[Test]
        ////[Explicit] // TODO: feature still not pushed
        ////public void Deserialize_Environment_With_Include()
        ////{
        ////    using (var dir = new TestFolder())
        ////    {
        ////        dir.AddFile("common.xml", @"<?xml version=""1.0""?>
        ////                                    <environment name=""common"" description=""just a base"">
	       ////                                     <variable name=""firstname"" value=""jack"" />
	       ////                                     <variable name=""lastname"" value=""bauer"" />
        ////                                    </environment>");
                
        ////        dir.AddFile("testenv.xml", @"<?xml version=""1.0""?>
        ////                                    <environment name=""testenv"" description=""testenv which includes common.xml"" include=""common.xml"">
	       ////                                     <variable name=""lastname"" value=""zuercher"" />
        ////                                    </environment>");;

        ////        var envprovider = new EnvironmentProvider(Path.Combine(dir.DirectoryInfo.FullName));
        ////        var result = envprovider.GetEnvironment("testenv");

        ////        Assert.AreEqual(4, result.Variables.Count);
        ////        Assert.AreEqual("TESTENV", result["ENV"].Value);
        ////        Assert.AreEqual("testenv", result["env"].Value);
        ////        Assert.AreEqual("jack", result["firstname"].Value);
        ////        Assert.AreEqual("zuercher", result["lastname"].Value);
        ////    }
        ////}
    }
}