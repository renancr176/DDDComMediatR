namespace ApiMvno.Domain.Core.Extensions;

public static class LongExtensions
{
    public static bool IsValidMsisdn(this long msisdn)
    {
        try
        {
            var msisdnOnly = long.Parse(string.Join("",
                string.Join("", msisdn.ToString().Reverse())
                    .Substring(0, msisdn.ToString().Length > 9 ? 9 : msisdn.ToString().Length).Reverse()));

            if (msisdnOnly > 900000000 && msisdnOnly < 999999999)
            {
                return true;
            }
        }
        catch (Exception e)
        {
        }

        return false;
    }
}