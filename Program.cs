using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace OOP9
{
    class Program
    {
        static void Main(string[] args)
        {
            CashBox cashBox = new();
            Show show = new();

            show.Statistics(cashBox);

            for (int i = 0; i < cashBox.GetHowManyPeopl(); i++)
            {
                cashBox.InviteNps();
                show.Statistics(cashBox);
                Console.ReadLine();
            }
            Console.ReadLine();
        }
    }

    class Show
    {
        internal void Statistics(CashBox cashBox)
        {
            int howManyPeopl = cashBox.GetHowManyPeopl();

            Console.WriteLine("Людей в очереди - " + howManyPeopl);
        }

        internal void NpsThinks(string name, int costGoodsNps, int money)
        {
            Console.WriteLine(name);
            Console.WriteLine();
            Console.WriteLine("Вы купили продуктов на сумму - " + costGoodsNps);
            Console.WriteLine("На вашей карте - " + money + "денег, вы решили расплатится.");
            Console.WriteLine();
        }

        internal void BuerNumber(int number)
        {
            Console.WriteLine();
            Console.Write("Покупатель №" + number + " - ");
        }
    }

    class CashBox
    {
        private int _queueLength;
        private int _mony;

        private Queue<NPS> _queueCheckout = new();
        private Random _random = new();
       
        internal CashBox()
        {
            GenerateQueue();
        }
        
        internal void InviteNps()
        {
            Show show = new();
            int buerNumber = 1;

            foreach (NPS nps in _queueCheckout)
            {
                show.BuerNumber(buerNumber);
                buerNumber++;

                bool thereIsMoney = true;

                while (thereIsMoney)
                {
                    if (EnouchMoney(nps.GetMony(), nps.CostGoodsNps()))
                    {
                        thereIsMoney = false;
                        show.NpsThinks(nps.GetName(), nps.CostGoodsNps(), nps.GetMony());
                        _mony += nps.CostGoodsNps();
                    }
                    else
                    {
                        thereIsMoney = true;
                        show.NpsThinks(nps.GetName(), nps.CostGoodsNps(), nps.GetMony());
                        Console.WriteLine("Ой у меня не достаточно денег пожалуй я, что нибудь оставлю.");
                        Console.ReadLine();
                        nps.DeleteProdukt();
                    }
                }
            }
            _queueCheckout.Clear();
            Console.WriteLine("Очередь закончилась");
            Console.WriteLine("В кассе - " + _mony + " денег");
        }

        private bool EnouchMoney(int moneyNps, int costGoodsNps)
        {
            bool enouch = true;
            int difference = moneyNps - costGoodsNps;

            if (difference < 0)
            {
                enouch = false;
            }
            return enouch;
        }

        internal int GetHowManyPeopl()
        {
            return _queueCheckout.Count;
        }

        private void GenerateQueue()
        {
            GenerateQueueLength();
            CreateQueue();
        }

        private void GenerateQueueLength()
        {
            int maximumQueue = 10;
            int minimumQueue = 2;

            _queueLength = _random.Next(minimumQueue, maximumQueue);
        }

        private void CreateQueue()
        {
            for (int i = 0; i <= _queueLength; i++)
            {
                NPS nps = new();

                _queueCheckout.Enqueue(nps);
            }
        }

    }

    class NPS
    {
        private string _name;
        private int _money;
        private int _maximumInventoryCapacity;
        private List<Produkt> _inventory = new();

        public NPS()
        {
            GenerateNps();
        }

        internal void DeleteProdukt()
        {
            int temp = int.MaxValue;
            int index = 0;

            for (int i = 0; i < _inventory.Count; i++)
            {
                int difference = _money - _inventory[i].GetPrice();

                if (difference < temp)
                {
                    temp = difference;
                    index = i;
                }
            }
            _inventory.RemoveAt(index);
        }

        internal string GetName()
        {
            return _name;
        }

        internal int GetMony()
        {
            return _money;
        }

        internal void GenerateNps()
        {
            GenerateName();
            GenerateMoney();
            GenerateInventoryCapacity();
            GenerateInventory();
        }
       
        internal int CostGoodsNps()
        {
            int money = 0;

            foreach (Produkt item in _inventory)
            {
                money += item.GetPrice();
            }
            return money;
        }

        private void GenerateInventory()
        {
            for (int i = 0; i <= _maximumInventoryCapacity; i++)
            {
                Produkt produkt = new();

                if (i != 0)
                {
                    if (IsThereProdukt(produkt))
                    {
                        _inventory.Add(produkt);
                    }
                }
                else
                {
                    _inventory.Add(produkt);
                }
            }
        }

        private bool IsThereProdukt(Produkt produkt)
        {
            bool isTherea = true;

            foreach (Produkt item in _inventory)
            {
                if (item.GetName() == produkt.GetName())
                {
                    isTherea = false;
                }
            }
            return isTherea;
        }

        private void GenerateInventoryCapacity()
        {
            int maximumCapacity = 30;
            int minimumCapacity = 10;

            _maximumInventoryCapacity = RandomNumberGenerator.GetInt32(minimumCapacity, maximumCapacity);
        }

        private void GenerateMoney()
        {
            int maximumMoney = 8000;
            int minimumMoney = 700;

            _money = RandomNumberGenerator.GetInt32(minimumMoney, maximumMoney);
        }

        private void GenerateName()
        {
            string[] npsName = { "Нестер Евгения Ильинична", "Самиров Леонид Егорович", "Рязанцев Андрей Александрович", "Фунтов Юрий Геннадьевич", "Ивойлова Ксения Марселевна", "Шестунов Алексей Романович", "Ефанов Николай Алексеевич", "Петухина Алена Никитовна", "Качковский Вадим Васильевич", "Тунеева Маргарита Вадимовна", "Точилкина Анжелика Григорьевна", "Батраков Никита Павлович", "Вязмитинова Галина Яновна", "Индейкина Оксана Романовна", "Колосюк Руслан Янович", "Четков Михаил Ильич", "Хорошилова Надежда Кирилловна", "Кадулин Павел Тимурович", "Якименко Вероника Рамилевна", "Валиулин Дмитрий Данилович", "Тельпугова Евгения Артемовна", "Биушкина Татьяна Олеговна", "Славутинский Николай Игоревич", "Давыдов Александр Петрович", "Туаева Вероника Максимовна", "Мутовкина Ирина Васильевна", "Тактаров Эдуард Ринатович", "Златовратский Борис Павлович", "Недодаева Полина Аркадьевна", "Спиридонов Роман Борисович", "Лоринова Людмила Тимуровна", "Ряхин Марат Русланович", "Юльева Екатерина Ивановна", "Шуйгин Олег Максимович", "Проклов Глеб Валентинович", "Майданов Тимофей Алексеевич", "Славянинов Артур Маратович", "Таюпова Оксана Робертовна", "Коноплич Маргарита Андреевна", "Дратцева Римма Денисовна", "Гречановская Тамара Федоровна", "Петрищева Ирина Никитовна", "Шейхаметова Раиса Артуровна", "Сумцова Анжелика Геннадьевна", "Есиповская Татьяна Робертовна", "Свиногузова Кристина Ильдаровна", "Галанина Лидия Альбертовна", "Ледяева Жанна Константиновна", "Дудник Егор Радикович", "Гаянов Григорий Алексеевич" };

            _name = npsName[RandomNumberGenerator.GetInt32(0, npsName.Length - 1)];
        }
    }

    class Produkt
    {
        private string _name;
        private int _price;

        internal Produkt()
        {
            GenerateProdukt();
        }

        internal string GetName()
        {
            return _name;
        }

        internal int GetPrice()
        {
            return _price;
        }

        private void GenerateProdukt()
        {
            GenerateName();
            GeneratePrice();
        }

        private void GenerateName()
        {

            string[] produktName = {"Морковь","Свекла","Картофель","Репа","Виноград белый","Виноград черный","Чеснок","Лук репчатый",
                                    "Лук синий","Лук зеленый","Петрушка","Укроп","Орех грецкий","Редис","Свекла","Капуста белокачанная",
                                    "Капуста броколи", "Капуста пекинская","Майонез","Клубника","Горох","Фасоль"};
            _name = produktName[RandomNumberGenerator.GetInt32(0, produktName.Length - 1)];
        }

        private void GeneratePrice()
        {
            int maximumPrice = 600;
            int minimumPrice = 10;

            _price = RandomNumberGenerator.GetInt32(minimumPrice, maximumPrice);
        }
    }
}
