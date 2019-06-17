using DddInPractice.Domain.Aggregates.SnakMachineAggregate.Exceptions;
using NoSqlDddInPractice.Domain.SeedObjects;
using System;
using System.Linq;

namespace NoSqlDddInPractice.Domain.Aggregates.SnakMachineAggregate
{
    public class SnackType : Enumeration
    {
        public static SnackType None = new SnackType(0, nameof(SnackType.None));
        public static SnackType Chocolate = new SnackType(1, nameof(SnackType.Chocolate));
        public static SnackType Soda = new SnackType(2, nameof(SnackType.Soda));
        public static SnackType Gum = new SnackType(3, nameof(SnackType.Gum));

        protected SnackType() { }

        public SnackType(int id, string name) : base(id, name)
        {

        }

        public static SnackType[] List() =>
            new[] { None, Chocolate, Soda, Gum };

        public static SnackType FromName(string name)
        {
            var snak = List().SingleOrDefault(s => s.Name
                .Equals(name, StringComparison.OrdinalIgnoreCase));

            if (snak == null)
            {
                throw new SnakDoesNotExistException(nameof(name));
            }
            return snak;
        }

        public static SnackType FromId(int id)
        {
            var snak = List().SingleOrDefault(s => s.Id == id);

            if (snak == null)
            {
                throw new SnakDoesNotExistException(nameof(id));
            }
            return snak;
        }
    }
}
