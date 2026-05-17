using FinanceTracker.Library.Models;
using FinanceTracker.Library.Enums;

namespace FinanceTracker.Tests.ModelsTests;

//Файл  містить модульні (unit) тести, які перевіряють працездатність основних моделей даних (класів) з бібліотеки ,цей файл тестує всі операції з грошима
public class MoneyTests
{
    //1. Перевіряє, чи правильно створюється об'єкт грошей, якщо передати туди тільки суму.
    [Fact]
    public void Constructor_SetsBalance()
    {
        var money = new Money(100m);

        Assert.Equal(100m, money.Balance);
        Assert.Equal(CurrencyTypes.UAH, money.Currency);
    }

    //2 .Перевіряє конструктор, де ми явно вказуємо валюту.
    [Fact]
    public void Constructor_SetsBalanceAndCurrency()
    {
        var money = new Money(200m, CurrencyTypes.USD);

        Assert.Equal(200m, money.Balance);
        Assert.Equal(CurrencyTypes.USD, money.Currency);
    }

    //3. Тестує метод додавання грошей. Незміннсть (створення нового об'єкта структури)
    [Fact]
    public void Add_IncreasesBalance()
    {
        var money = new Money(100m, CurrencyTypes.EUR);

        var result = money.Add(50m);

        Assert.Equal(150m, result.Balance);
        Assert.Equal(CurrencyTypes.EUR, result.Currency);
        Assert.Equal(100m, money.Balance);
    }

    //3. Тестує метод віднімання грошей. Незміннсть (створення нового об'єкта структури)
    [Fact]
    public void Subtract_DecreasesBalance()
    {
        var money = new Money(100m, CurrencyTypes.UAH);

        var result = money.Subtract(30m);

        Assert.Equal(70m, result.Balance);
        Assert.Equal(CurrencyTypes.UAH, result.Currency);
        Assert.Equal(100m, money.Balance);
    }
}






