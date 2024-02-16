namespace SpecflowSelenium.Support
{
    /// <summary>
    /// Classe responsável por retornar a descrição de um enumerador.
    /// </summary>
    public static class Enumerator
    {
        /// <summary>
        /// Obtém a descrição do atribudo do enumerador.
        /// </summary>
        /// <param name="enumValue">Valor do enumerador</param>
        /// <returns>Descrição do enumerador</returns>
        public static string GetDescription(this Enum enumValue)
        {
            var attribute = enumValue.GetAttribute<DescriptionAttribute>();
            return attribute == null ? enumValue.ToString() : attribute.Description;
        }

        /// <summary>
        /// Obtém a descrição do atributo customizado do enumerador.
        /// </summary>
        /// <param name="enumValue">Valor do enumerador</param>
        /// <returns>Descrição do atributo customizado do enumerador</returns>
        public static string GetEnumDescription(this Enum enumValue)
        {
            var fi = enumValue.GetType().GetField(enumValue.ToString());

            var attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;

            return enumValue.ToString();
        }

        private static T GetAttribute<T>(this System.Enum enumValue) where T : Attribute
        {
            var type = enumValue.GetType();
            var memberValues = type.GetMember(enumValue.ToString());
            var attributes = memberValues[0].GetCustomAttributes(typeof(T), false);

            return (T)attributes[0];
        }
    }
}
