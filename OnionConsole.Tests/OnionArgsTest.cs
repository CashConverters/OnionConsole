using System;
using System.Collections.Generic;
using Xunit;

namespace OnionConsole.Tests
{
    public class OnionArgsTest
    {
        [Fact]
        public void TestEmptyArgs()
        {
            var args = new OnionArgs();
            Assert.Null(args.AssemblyName);
            Assert.Null(args.ConfigurationPath);

        }

        [Fact]
        public void BothArgsShouldBeInValid()
        {
            var args = new OnionArgs();
            string[] opts = new[] {"-c 123.xml", "-a 123.dll"};
            
            var success = args.Parser.ParseArguments(opts, args);
            Assert.False(success);
            Assert.Equal("123.dll",args.AssemblyName.Trim());
            Assert.Equal("123.xml", args.ConfigurationPath.Trim());
        }

        [Fact]
        public void ConfigurationPathShouldbeValid()
        {
            var args = new OnionArgs();
            string[] opts = { "-c 123.xml"};

            var success = args.Parser.ParseArguments(opts, args);
            Assert.True(success);
            Assert.Equal("123.xml", args.ConfigurationPath.Trim());
        }

        [Fact]
        public void AssemblyShouldbeValid()
        {
            var args = new OnionArgs();
            string[] opts = { "-a 123.dll" };

            var success = args.Parser.ParseArguments(opts, args);
            var fail = args.LastParserState;
            Assert.True(success);
            Assert.Equal("123.dll", args.AssemblyName.Trim());
        }

        [Fact]
        public void NoArgsShouldPassWithDefault()
        {
            var args = new OnionArgs();
            string[] opts = { "" };

            var success = args.Parser.ParseArguments(opts, args);
            Assert.True(success);
            Assert.Equal(".\\", args.ConfigurationPath.Trim());
        }

    }
}
