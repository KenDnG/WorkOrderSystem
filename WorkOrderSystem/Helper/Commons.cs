namespace WorkOrderSystem.Helper
{
    public class Commons
    {
        public static string ConvertToNominal(int nominal)
        {
            return Convert.ToDecimal(nominal).ToString("N0");
        }
    }
}
