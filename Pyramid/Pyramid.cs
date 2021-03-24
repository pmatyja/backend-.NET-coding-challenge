using System;
using System.Text;

/*
 
    *
   ***
  *****
 *******
*********
ACCEPTANCE CRITERIA:
Write a script to output this pyramid on console (with leading spaces)
*/
namespace Pyramid
{
    public class Program
    {
        private static void Pyramid(int height)
        {
            var buffer = new char[height * 2 + 1];
            
            for(int i = 0; i < buffer.Length; ++i)
            {
                buffer[i] = ' ';
            }

            var offset = 1;
            var center = height - 1;
            
            buffer[center] = '*';

            while (height-- > 0)
            {
                Console.WriteLine(buffer);

                if ( offset <= center )
                {
                    buffer[center - offset] = '*';
                    buffer[center + offset] = '*';
                    offset++;
                }
            }
        }
        
        public static void Main(string[] args)
        {
            Pyramid(5);
        }
    }
}