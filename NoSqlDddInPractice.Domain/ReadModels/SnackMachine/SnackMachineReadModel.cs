namespace NoSqlDddInPractice.Domain.ReadModels.SnackMachine
{
    public class SlotReadModel
    {
        public SnakTypeReadModel SnakType { get; set; }
        public int Position { get; set; }
        public int ItemQuantity { get; set; }
        public decimal ItemPrice { get; set; }
    }

    public class SnackMachineReadModel
    {
        public bool HasMoneyInside { get; set; }
        public SlotReadModel[] Slots { get; set; }
    }
}
