﻿using System.Linq;

namespace NoSqlDddInPractice.Domain.Aggregates.SnakMachineAggregate
{
    public static class MoneyExtensions
    {
        private static readonly Money[] _acceptableCoinsAndNotes;

        static MoneyExtensions()
        {
            _acceptableCoinsAndNotes = new[]
            {
                Money.Cent, Money.TenCent, Money.Quarter, Money.Dollar, Money.FiveDollar
            };
        }

        public static bool IsAcceptable(this Money money) =>
            _acceptableCoinsAndNotes.Contains(money);

    }
}
