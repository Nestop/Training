namespace JSONdata
{
    public class EmployeeData
    {
        public string Name;
        public string Position;
        public string WorkplaceNumber;
        public string Phone;

        public EmployeeData(string name, string position, string workplaceNumber, string phone)
        {
            Name = name;
            Position = position;
            WorkplaceNumber = workplaceNumber;
            Phone = phone;
        }
    }
}