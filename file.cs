using System;

namespace HotelAccounting
{
    public class AccountingModel : ModelBase
    {
        private double price;
        private int nightsCount;
        private double discount;
        private double total;

        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Цена не может быть отрицательной.", nameof(Price));
                }

                price = value;
                UpdateTotal();
                Notify(nameof(Price));
                Notify(nameof(Total));
            }
        }

        public int NightsCount
        {
            get
            {
                return nightsCount;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Количество ночей должно быть положительным.", nameof(NightsCount));
                }

                nightsCount = value;
                UpdateTotal();
                Notify(nameof(NightsCount));
                Notify(nameof(Total));
            }
        }

        public double Discount
        {
            get
            {
                return discount;
            }
            set
            {
                if (value > 100)
                {
                    throw new ArgumentException("Скидка не может быть больше 100.", nameof(Discount));
                }

                discount = value;
                UpdateTotal();
                Notify(nameof(Discount));
                Notify(nameof(Total));
            }
        }

        public double Total
        {
            get
            {
                return total;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Сумма счёта не может быть отрицательной.", nameof(Total));
                }

                total = value;
                UpdateDiscountFromTotal();
                Notify(nameof(Total));
                Notify(nameof(Discount));
            }
        }

        private void UpdateTotal()
        {
            var baseAmount = price * nightsCount;
            var discountFactor = 1.0 - discount / 100.0;
            total = baseAmount * discountFactor;
        }

        private void UpdateDiscountFromTotal()
        {
            var baseAmount = price * nightsCount;

            if (baseAmount == 0)
            {
                discount = 0;
            }
            else
            {
                var ratio = total / baseAmount;
                discount = 100.0 * (1.0 - ratio);
            }
        }
    }
}
