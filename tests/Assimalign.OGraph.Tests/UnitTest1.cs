namespace Assimalign.OGraph.Tests
{

    public class User
    {
        
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var builder = new OGraphModelBuilder();

            builder.AddEntity<User>(entity =>
            {
                entity.HasLabel("");
                entity.Prop
            });
        }
    }
}