using System;
using SimpleJSON;

namespace JSONdata
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            CompanyData openMyGame = new CompanyData();
            openMyGame.ReadCompanyData(@".\CompanyData.json");
            openMyGame.PrintShortCompanyInfo();
            openMyGame.PrintEmployeesInfo();
            openMyGame.PrintFullEmployeeInfo("AA1010");
        }
    }
    
}