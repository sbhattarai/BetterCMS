﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BetterCms.Core.DataAccess;
using BetterCms.Core.DataAccess.DataContext;
using BetterCms.Module.Pages.Command.Page.ClonePage;
using BetterCms.Module.Pages.Models;
using BetterCms.Module.Pages.Services;
using BetterCms.Module.Pages.ViewModels.Page;
using BetterCms.Module.Root.Models;

using Moq;

using NUnit.Framework;

namespace BetterCms.Test.Module.Pages.CommandTests.PageTests
{
    [TestFixture]
    public class ClonePageCommandTest : IntegrationTestBase
    {
        [Test]
        public void Sould_Clone_Page_With_Tags_Options_Contents_AccessRules()
        {
            RunActionInTransaction(session =>
                {
                    const string url = "/test-link";
                    var uow = new DefaultUnitOfWork(session);
                    var repository = new DefaultRepository(uow);


                    var pageToClone = TestDataProvider.CreateNewPageWithTagsContentsOptionsAndAccessRules(session, 2, 2, 2, 2);
                    
                    session.SaveOrUpdate(pageToClone);
                    session.Flush();
                    session.Clear();
                    
                    var pageService = new Mock<IPageService>();
                    pageService.Setup(f => f.ValidatePageUrl(It.IsAny<string>(), It.IsAny<Guid?>()));
                    pageService.Setup(f => f.CreatePagePermalink(It.IsAny<string>(), It.IsAny<string>())).Returns(url);

                    var urlService = new Mock<IUrlService>();
                    urlService.Setup(f => f.FixUrl(It.IsAny<string>())).Returns(url);

                    var command = new ClonePageCommand();
                    command.Repository = repository;
                    command.UnitOfWork = uow;
                    command.PageService = pageService.Object;
                    command.UrlService = urlService.Object;

                    var result = command.Execute(new ClonePageViewModel
                                        {
                                            PageId = pageToClone.Id,
                                            PageTitle = "new cloned page",
                                            PageUrl = url
                                        });

                    Assert.IsNotNull(result);
                    session.Clear();

                    var actual = repository.AsQueryable<PageProperties>().Where(f => f.Id == result.PageId).ToList().FirstOrDefault();
                    
                    Assert.IsNotNull(actual);
                    Assert.AreEqual(2, actual.AccessRules.Count(), "AccessRules");
                    Assert.AreEqual(2, actual.PageTags.Count(), "Tags");
                    Assert.AreEqual(2, actual.PageContents.Count(), "Contents");
                    Assert.AreEqual(2, actual.Options.Count(), "Options");
                });
        }
    }
}
