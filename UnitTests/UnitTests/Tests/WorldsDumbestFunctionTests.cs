using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Tests
{
    public class WorldsDumbestFunctionTests
    {
        //Naming convention - Classname_methodName_ExpectedResult
        public static void WorldsDumbestFunction_ReturnsPikachuIfZero_ReturnString()
        {
            try
            {
                //Arrange - Go get Variables
                int num = 0;
                WorldsDumbestFunction worldsDumbest = new WorldsDumbestFunction();
                //Act - Execute the function
                string result = worldsDumbest.ReturnsPikachuIfZero(num);
                //Assert - is it true or false
                if(result == "PIKACHU")
                {
                    Console.WriteLine("Passed");
                }
                else
                {
                    Console.WriteLine("Failed");
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
