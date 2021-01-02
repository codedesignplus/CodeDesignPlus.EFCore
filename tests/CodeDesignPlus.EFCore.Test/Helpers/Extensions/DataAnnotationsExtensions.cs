using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace CodeDesignPlus.EFCore.Test.Helpers.Extensions
{
    /// <summary>
    /// Clase que se encarga de proveer metodos de extensión para la validación de los Data Annotations
    /// </summary>
    public static class DataAnnotationsExtensions
    {
        /// <summary>
        /// Metodo de extensión encargado de validar un View Model
        /// </summary>
        /// <typeparam name="T">Tipo de objeto a validar</typeparam>
        /// <param name="data">Objeto a Validar</param>
        /// <returns>Returna una lista con los resultados de las validaciones</returns>
        public static IList<ValidationResult> Validate<T>(this T data)
        {
            var results = new List<ValidationResult>();

            var validationContext = new ValidationContext(data, null, null);

            Validator.TryValidateObject(data, validationContext, results, true);

            if (data is IValidatableObject)
                (data as IValidatableObject).Validate(validationContext);

            return results;
        }
    }
}
