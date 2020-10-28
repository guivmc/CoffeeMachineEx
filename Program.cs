using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MaquinaCafe
{
    public class Program
    {
        public static double[] coins = new double[] { 1d, 0.50d, 0.25d, 0.10d, 0.05d, 0.01d };

        public static List<double> insertedCoins = new List<double>();

        /// <summary>
        /// Check if coin is valid.
        /// </summary>
        /// <param name="input">The inserted coin.</param>
        /// <returns>A tuple with the coin value and whether should keep reading coins.</returns>
        public static Tuple<double, bool> ReadCoin( string input )
        {
            double insertedCoin = 0;
            bool keepReadingCoins = true;

            try
            {
                //read coin.
                insertedCoin = Math.Round( double.Parse( input, CultureInfo.InvariantCulture.NumberFormat ), 2 );

                //Is valid coin?
                if( !coins.Contains( insertedCoin ) )
                    throw new Exception( "Coin of value " + insertedCoin + " not acceptable!" );
                //Should discard coin? 
                else if( insertedCoin == 0.01d || insertedCoin == 0.05d )
                    throw new Exception( "Error reading " + insertedCoin + ", try a different coin." );

            }
            catch( Exception e )
            {
                insertedCoin = 0;

                //If input is not a coin return false, to go to then next step.
                if( e is FormatException )
                {
                    keepReadingCoins = false;
                }
                //Else show me the error.
                else
                {
                    Console.WriteLine( e.Message );
                }

            }

            return new Tuple<double, bool>( insertedCoin, keepReadingCoins );
        }

        /// <summary>
        /// Calculate the coins for change.
        /// </summary>
        /// <param name="price">Coffee price.</param>
        /// <param name="total">Total money inserted.</param>
        /// <returns>A string with a list of coins necessary for change.</returns>
        public static string CalcChange(double price, double total)
        {
            double[] changeToPrint = new double[6];

            double change = total - price;

            int count = 0;

            string @return = "";

            //Get amount of coins of a value to serve as change
            for( int i = 0; i < coins.Length && change > 0; i++ )
            {
                count = (int)( change / coins[i] );
                change -= count * coins[i];

                @return += "\n" + coins[i]  + ": " + count;
            }

            return @return;
        }

        public static void Main( string[] args )
        {
            bool keepReadingCoins = true;
            double insertedCoin = 0;
            string input;

            Console.WriteLine( "A - Cappuccino R$3.50" );
            Console.WriteLine( "B - Mocha R$4.00" );
            Console.WriteLine( "C - Café com leite R$3.00" );
            Console.WriteLine( "Insert your coins, then type the letter that represents your desired coffee." );

            do
            {
                input = Console.ReadLine();

                ReadCoin( input ).Deconstruct( out insertedCoin, out keepReadingCoins );

                insertedCoins.Add( insertedCoin );

            } while( keepReadingCoins );

            double total = insertedCoins.Sum();

            switch( input.ToLower()[0])
            {
                case 'a':
                    if(total >= 3.50d)
                    {
                        Console.WriteLine( "Cappuccino purchased!" );
                        Console.WriteLine( CalcChange( 3.5d, total ) );
                    }
                    else
                        Console.WriteLine( "Sorry not enhough money for a Cappuccino." );
                    break;
                case 'b':
                    if( total >= 4d )
                    {
                        Console.WriteLine( "Mocha purchased!" );
                        Console.WriteLine( CalcChange( 4d, total ) );
                    }
                    else
                        Console.WriteLine( "Sorry not enhough money for a Mocha." );
                    break;
                case 'c':
                    if( total >= 3d )
                    {
                        Console.WriteLine( "Café com leite purchased!" );
                        Console.WriteLine( CalcChange( 3d, total ) );
                    }
                    else
                        Console.WriteLine( "Sorry not enhough money for a Café com leite." );
                    break;
            }

            Console.WriteLine( "Bye bye O/!" );
            Console.ReadLine();
        }
    }
}
