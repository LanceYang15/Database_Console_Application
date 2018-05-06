using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConsoleApplication
{
    class UserInterface
    {
        public UserInterface()
        {

        }

        public string GetStringInput()
        {
            return Console.ReadLine();
        }

        public int GetIntInput()
        {

            return Convert.ToInt32(Console.ReadLine());
        }
    }
}
