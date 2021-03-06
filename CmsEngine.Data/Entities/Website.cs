using System.Collections.Generic;
using CmsEngine.Core.Constants;
using Newtonsoft.Json.Linq;

namespace CmsEngine.Data.Entities
{
    public class Website : BaseEntity
    {
        public virtual ICollection<Page> Pages { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Category> Categories { get; set; }

        public string Name { get; set; }
        public string Tagline { get; set; }
        public string Description { get; set; }
        public string HeaderImage { get; set; }
        public string Culture { get; set; }
        public string UrlFormat { get; set; } = $"{CmsEngineConstants.SiteUrl}/{CmsEngineConstants.Type}/{CmsEngineConstants.Slug}";
        public string DateFormat { get; set; } = "MM/dd/yyyy";
        public string SiteUrl { get; set; }
        public int ArticleLimit { get; set; }

        // Contact details
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string LinkedIn { get; set; }

        // Api configuration
        public string FacebookAppId { get; set; }
        public string FacebookApiVersion { get; set; }
        public string DisqusShortName { get; set; }
        public string GoogleAnalytics { get; set; }
        public string GoogleRecaptchaSiteKey { get; set; }
        public string GoogleRecaptchaSecretKey { get; set; }

        public override string ToString()
        {
            var jsonResult = new JObject(
                                        new JProperty("Id", Id),
                                        new JProperty("VanityId", VanityId),
                                        new JProperty("Name", Name),
                                        new JProperty("SiteUrl", SiteUrl),
                                        new JProperty("Tagline", Tagline)
                                    );
            return jsonResult.ToString();
        }
    }
}
