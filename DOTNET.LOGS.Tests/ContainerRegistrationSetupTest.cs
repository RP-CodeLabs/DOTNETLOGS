using NUnit.Framework;

namespace DOTNET.LOGS.Tests
{
    [TestFixture]
    public class ContainerRegistrationSetupTest
    {
        [Test]
        public void Register_Should_Find_A_Type_To_Register_Dependencies_From_Namespaces_Provided()
        {
            var containerSetup = new ContainerRegistrationSetup();

            containerSetup.Register("");

        }
    }

    public class ContainerRegistrationSetup
    {
        public void Register(string nameSpace)
        {
            throw new System.NotImplementedException();
        }
    }
}
