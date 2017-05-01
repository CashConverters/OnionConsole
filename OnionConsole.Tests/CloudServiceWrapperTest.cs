using System.IO;
using System.Linq;
using Xunit;

namespace OnionConsole.Tests
{
    public class CloudServiceWrapperTest
    {
        //todo find a better way
        private readonly string _pathToSomeRolesTypes = Path.Combine(Directory.GetCurrentDirectory(),
            "..\\..\\..\\OnionConsole\\bin\\", Path.GetFileName(Directory.GetCurrentDirectory()));
        [Fact]
        public void TestConstructor()
        {
            var args = new CloudServiceWrapper(Directory.GetCurrentDirectory());
            Assert.NotNull(args);
        }

        [Fact]
        public void TestFindingRoleTypes()
        {
            var args = new CloudServiceWrapper(_pathToSomeRolesTypes);
            var types = args.FindRoleEntryTypes();
            Assert.NotEmpty(types);
        }

        public void TestFindingNoRoleTypes()
        {
            var args = new CloudServiceWrapper(Directory.GetCurrentDirectory());
            var types = args.FindRoleEntryTypes();
            Assert.Empty(types);
        }


        [Fact]
        public void TestFindingRoleEntryPoint()
        {
            var args = new CloudServiceWrapper(_pathToSomeRolesTypes);
            var types = args.FindRoleEntryTypes();
            Assert.True(types.Any(t => t.Name.Equals("RoleEntryPoint")));
        }



    }
}
