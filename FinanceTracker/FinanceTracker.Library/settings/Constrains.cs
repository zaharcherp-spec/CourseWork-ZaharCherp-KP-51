
namespace FinanceTracker.Library.Settings;

public class Constrains
{
    public class AuthConstraints
    {
        public int MinPasswordLength { get; set; } = 4;
        public int MinUsernameLength { get; set; } = 3;
    }

    public class TransferConstraints
    {
        public decimal MaxTransferAmount { get; set; } = 50000;
        public decimal MinTransferAmount { get; set; } = 1;
    }
}



