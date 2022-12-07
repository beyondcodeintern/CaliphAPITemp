using Caliph.Library;

namespace CaliphConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ConvertPotentialClientToKIVIfLongerThanNDays();
        }

        static void ConvertPotentialClientToKIVIfLongerThanNDays()
        {
            var oCaliphService = new CaliphService();
            oCaliphService.ConvertPotentialClientToKIVIfLongerThanNDays();
        }
    }
}
