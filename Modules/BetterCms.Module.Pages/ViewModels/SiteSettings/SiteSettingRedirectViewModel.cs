﻿using System;
using System.ComponentModel.DataAnnotations;

using BetterCms.Core.Models;
using BetterCms.Module.Pages.Content.Resources;
using BetterCms.Module.Root.Content.Resources;
using BetterCms.Module.Root.Mvc.Grids;

namespace BetterCms.Module.Pages.ViewModels.SiteSettings
{
    public class SiteSettingRedirectViewModel : IEditableGridItem
    {
        /// <summary>
        /// Gets or sets the content id.
        /// </summary>
        /// <value>
        /// The content id.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the entity version.
        /// </summary>
        /// <value>
        /// The entity version.
        /// </value>
        public int Version { get; set; }

        /// <summary>
        /// Gets or sets the page URL.
        /// </summary>
        /// <value>
        /// The page URL.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(RootGlobalization), ErrorMessageResourceName = "Validation_RequiredAttribute_Message")]
        [RegularExpression(PagesConstants.PageUrlRegularExpression, ErrorMessageResourceType = typeof(PagesGlobalization), ErrorMessageResourceName = "PageProperties_PageUrl_InvalidMessage")]
        [StringLength(MaxLength.Url, ErrorMessageResourceType = typeof(RootGlobalization), ErrorMessageResourceName = "Validation_StringLengthAttribute_Message")]
        public string PageUrl { get; set; }

        /// <summary>
        /// Gets or sets the redirect URL.
        /// </summary>
        /// <value>
        /// The redirect URL.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(RootGlobalization), ErrorMessageResourceName = "Validation_RequiredAttribute_Message")]
        [RegularExpression(PagesConstants.PageUrlRegularExpression, ErrorMessageResourceType = typeof(PagesGlobalization), ErrorMessageResourceName = "RedirectUrl_InvalidMessage")]
        [StringLength(MaxLength.Url, ErrorMessageResourceType = typeof(RootGlobalization), ErrorMessageResourceName = "Validation_StringLengthAttribute_Message")]
        public string RedirectUrl { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Id: {0}, Version:{1}, PageUrl:{2}", Id, Version, PageUrl);
        }
    }
}