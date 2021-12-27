using System;
namespace BL
{
    public class PackageCustomer
    {
        public int Id { get; init; }
        public string Name { get; init; }

        public PackageCustomer(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public override string ToString()
        {
            return $"  id: {Id}\n" +
                $"  name: {Name}";
        }
    }
}
