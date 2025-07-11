﻿using DesignGenerator.Domain.Interfaces.ImageGeneration;
using DesignGenerator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Infrastructure.ImageGeneration.StableDiffusion
{
    /// <summary>
    /// Represents a concrete implementation of IImageGenerationParams
    /// specifically tailored for Stable Diffusion generation clients.
    /// Stores a collection of parameter descriptors representing generation options.
    /// </summary>
    public class StableDiffusionParams : IImageGenerationParams
    {
        public IEnumerable<ParameterDescriptor> Parameters { get; }

        /// <summary>
        /// Initializes the default set of parameters expected by the Stable Diffusion API.
        /// </summary>
        public StableDiffusionParams()
        {
            Parameters = new List<ParameterDescriptor>
            {
                new ParameterDescriptor
                {
                    Name = "prompt",
                    DisplayName = "Prompt",
                    Type = ParameterType.Text,
                    Required = true,
                    Tooltip = "The text prompt to generate the image from."
                },
                new ParameterDescriptor
                {
                    Name = "negative_prompt",
                    DisplayName = "Negative Prompt",
                    Type = ParameterType.Text,
                    Required = false,
                    Tooltip = "Text to avoid in the generated image."
                },
                new ParameterDescriptor
                {
                    Name = "width",
                    DisplayName = "Width",
                    Type = ParameterType.Number,
                    DefaultValue = 512,
                    Required = false,
                    Tooltip = "Width of the generated image in pixels."
                },
                new ParameterDescriptor
                {
                    Name = "height",
                    DisplayName = "Height",
                    Type = ParameterType.Number,
                    DefaultValue = 512,
                    Required = false,
                    Tooltip = "Height of the generated image in pixels."
                },
                new ParameterDescriptor
                {
                    Name = "steps",
                    DisplayName = "Inference Steps",
                    Type = ParameterType.Number,
                    DefaultValue = 50,
                    Required = false,
                    Tooltip = "Number of inference steps for image generation."
                },
                new ParameterDescriptor
                {
                    Name = "guidance",
                    DisplayName = "Guidance Scale",
                    Type = ParameterType.Number,
                    DefaultValue = 7.5f,
                    Required = false,
                    Tooltip = "Scale for classifier-free guidance."
                },
                new ParameterDescriptor
                {
                    Name = "seed",
                    DisplayName = "Generation seed",
                    Type = ParameterType.Number,
                    DefaultValue = 1,
                    Required = false,
                    Tooltip = "Makes generation deterministic. Using the same seed and set of parameters will produce identical image each time."
                },
                new ParameterDescriptor
                {
                    Name = "scheduler",
                    DisplayName = "Scheduler",
                    Type = ParameterType.Dropdown,
                    DefaultValue = "euler_a",
                    Options = new List<string> { "euler_a", "euler", "lms", "ddim", "dpmsolver++" },
                    Required = false,
                    Tooltip = "Scheduler used to denoise the encoded image latents."
                },
                new ParameterDescriptor
                {
                    Name = "output_format",
                    DisplayName = "Guidance Scale",
                    Type = ParameterType.Dropdown,
                    DefaultValue = "png",
                    Options = new List<string> { "png", "jpg" },
                    Required = false,
                    Tooltip = "Scale for classifier-free guidance."
                }
            };
        }
    }
}
