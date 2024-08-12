using System.ComponentModel;
using System.Reflection;

namespace SeleniumPracticalExercise.Common
{
    public static class ExtensionMethods
    {
        // Any extension methods that aren't already included in BasePage go here

        /// <summary>
        /// Returns the Description Attribute of the enum
        /// 
        /// http://wmwood.net/2015/12/18/quick-tip-enum-to-description-in-csharp/
        /// </summary>
        /// <typeparam name="T">Enum</typeparam>
        /// <param name="enumerationValue">The enum to get the Description from</param>
        /// <returns>The Description Attribute of the enum</returns>
        public static string GetDescription<T>(this T enumerationValue) where T : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException($"{nameof(enumerationValue)} must be of Enum type", nameof(enumerationValue));
            }
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString()!);
            if (memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return enumerationValue.ToString()!;
        }
    }
}