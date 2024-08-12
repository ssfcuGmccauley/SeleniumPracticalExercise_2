namespace SeleniumPracticalExercise.Common
{
    public class Utils
    {
        // Any methods that aren't WebDriver specific and are used by more than one page object and/or test go here

        /// <summary>
        /// Holds a static Random for use in the project.
        /// </summary>
        public static Random Rnd = new Random();

        /// <summary>
        /// Returns a randomly generated string of the desired length
        /// </summary>
        /// <param name="length">The number of characters in the random string</param>
        /// <returns>The generated string</returns>
        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            return new string(Enumerable.Repeat(chars, length).Select(s => s[Rnd.Next(s.Length)]).ToArray());
        }
    }
}