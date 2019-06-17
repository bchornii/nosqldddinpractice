using DddInPractice.Domain.Aggregates.SnakMachineAggregate.Exceptions;
using NoSqlDddInPractice.Domain.SeedObjects;

namespace NoSqlDddInPractice.Domain.Aggregates.SnakMachineAggregate
{
    public class Slot : Entity
    {
        private int _position;
        private int _itemsQuantity;
        private decimal _itemPrice;

        public SnackType SnackType { get; private set; }

        protected Slot() { }

        public Slot(int position, int itemsQuantity, decimal itemPrice, int snackTypeId)
        {
            _position = position > 0 ? position : throw new IncorrectSlotDataException(nameof(position));

            if (itemsQuantity < 0)
            {
                throw new IncorrectSlotDataException(nameof(itemsQuantity));
            }
            _itemsQuantity = itemsQuantity;

            if (itemPrice < 0 || itemPrice % 0.01m > 0)
            {
                throw new IncorrectSlotDataException(nameof(itemPrice));
            }
            _itemPrice = itemPrice;
            
            SnackType = snackTypeId > 0 ? SnackType.FromId(snackTypeId) 
                : throw new IncorrectSlotDataException(nameof(snackTypeId));
        }

        public int GetPosition()
        {
            return _position;
        }

        public decimal GetItemPrice()
        {
            return _itemPrice;
        }

        public int GetItemsQuantity()
        {
            return _itemsQuantity;
        }

        public void SubtractOneSnak()
        {
            if (_itemsQuantity > 0)
            {
                _itemsQuantity--;
            }
        }

        public void SetSnackTypeId(int snackTypeId)
        {
            if(SnackType.Id == snackTypeId)
            {
                throw new IncorrectSlotDataException(nameof(snackTypeId));
            }            

            SnackType = SnackType.FromId(snackTypeId);
        }
    }
}
