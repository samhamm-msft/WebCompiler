﻿using System;
using System.IO;
using System.Linq;
using WebCompiler;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebCompilerTest
{
    [TestClass]
    public class LessTest
    {
        private ConfigFileProcessor _processor;

        [TestInitialize]
        public void Setup()
        {
            _processor = new ConfigFileProcessor();
        }

        [TestCleanup]
        public void Cleanup()
        {
            File.Delete("../../artifacts/less/test.css");
            File.Delete("../../artifacts/less/test.min.css");
        }

        [TestMethod, TestCategory("LESS")]
        public void CompileLess()
        {
            var result = _processor.Process("../../artifacts/compilerconfig.json");
            Assert.IsTrue(File.Exists("../../artifacts/less/test.css"));
        }
    }
}