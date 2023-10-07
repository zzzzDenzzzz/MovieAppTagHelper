using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MovieAppTagHelper.TagHelpers
{
    public class MovieRatingTagHelper : TagHelper
    {
        public int Rating { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "movie-rating");

            for (int i = 0; i < Rating; i++)
            {
                output.Content.AppendHtml("<i class=\"fa-regular fa-star fa-fade\"></i>");
            }
        }
    }
}
