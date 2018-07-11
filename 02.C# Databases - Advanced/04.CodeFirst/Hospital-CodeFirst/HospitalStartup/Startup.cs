using System;
using System.Linq;
using HospitalDatabaseInitializer.HospitalDatabaseInitializer;
using P01_HospitalDatabase.Data;

namespace HospitalStartup
{
    public class Startup
    {
        public static void Main()
        {
            using (var hospitalContext = new HospitalContext())
            {
                DatabaseInitializer.InitialSeed(hospitalContext);
            }
        }
    }
}
