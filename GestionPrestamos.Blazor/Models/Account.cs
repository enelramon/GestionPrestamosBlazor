namespace GestionPrestamos.Models;

public class Account
{
    public int Number { get; set; }
    public string Name { get; set; }
    public bool Selectable { get; set; } = false;
    public bool Deletable { get; set; } = false;
    public bool IsExpanded { get; set; } = true;
    public List<Account> ChildAccounts { get; set; } = new List<Account>();
}

public static class AccountData
{
    public static List<Account> GetAccounts()
    {
        var accounts = new List<Account>()
                    {
            new Account
            {
                Number = 1,
                Name = "ACTIVOS",
                ChildAccounts = new List<Account>()
                {
                    new Account
                    {
                        Number = 11,
                        Name = "Activos Corrientes",
                        Selectable = true,
                        ChildAccounts = new List<Account>()
                        {
                            new Account { Number = 111, Name = "Caja", Selectable = true, Deletable = true },
                            new Account { Number = 112, Name = "Bancos", Selectable = true, Deletable = true },
                            new Account { Number = 113, Name = "Inversiones", Selectable = true, Deletable = true }
                        }
                    },
                    new Account { Number = 12, Name = "Activos Fijos", Selectable = true },
                    new Account { Number = 13, Name = "Activos Diferidos", Selectable = true }
                }
            },
            new Account
            {
                Number = 2,
                Name = "PASIVOS",
                ChildAccounts = new List<Account>()
                {
                    new Account { Number = 21, Name = "Pasivos Corrientes", Selectable = true },
                    new Account { Number = 22, Name = "Pasivos a Largo Plazo", Selectable = true }
                }
            },
            new Account
            {
                Number = 3,
                Name = "CAPITAL",
                ChildAccounts = new List<Account>()
                {
                    new Account { Number = 31, Name = "Capital Social", Selectable = true },
                    new Account { Number = 32, Name = "Utilidades Retenidas", Selectable = true }
                }
            }
        };
        return accounts;
    }
}
