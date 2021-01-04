using System;

namespace PT_Task2_Data
{
    public static class ConnectionStringProvider
    {
        public static String Get()
        {
            return Properties.Settings.Default.DBConnectionString.ToString().Trim();
        }
    }
}
