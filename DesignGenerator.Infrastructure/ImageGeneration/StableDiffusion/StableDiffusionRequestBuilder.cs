using DesignGenerator.Domain.Interfaces.ImageGeneration;
using DesignGenerator.Domain.Models;
using DesignGenerator.Exceptions.Infrastructure;
using System.Globalization;

namespace DesignGenerator.Infrastructure.ImageGeneration.StableDiffusion
{
    /// <summary>
    /// Builds the payload dictionary required by the Stable Diffusion API from parameters.
    /// </summary>
    public class StableDiffusionRequestBuilder : IRequestBuilder
    {
        /// <summary>
        /// Converts IImageGenerationParams into a dictionary suitable for the API.
        /// </summary>
        public Dictionary<string, object> Build(IImageGenerationParams parameters)
        {
            var dict = new Dictionary<string, object>();

            foreach (var p in parameters.Parameters)
            {
                if (string.IsNullOrWhiteSpace(p.Name)) continue;

                object? value = p.Value;

                if (AreRequiredProvided(p))
                    throw new MissingRequiredParameterException(p.Name);

                if (value != null)
                {
                    value = p.Type switch
                    {
                        ParameterType.Number => Convert.ToDouble(value, CultureInfo.InvariantCulture),
                        ParameterType.Dropdown or ParameterType.Text => value.ToString(),
                        _ => value
                    };
                    dict[p.Name] = value;
                }
            }

            return dict;
        }

        /// <summary>
        /// Ensure required parameters are provided.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool AreRequiredProvided(ParameterDescriptor parameter) 
            => parameter.Value is null && parameter.Required;
    }
}
