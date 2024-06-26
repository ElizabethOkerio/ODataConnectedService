﻿//---------------------------------------------------------------------------------
// <copyright file="UniqueIdentifierServiceTests.cs" company=".NET Foundation">
//      Copyright (c) .NET Foundation and Contributors. All rights reserved. 
//      See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------------------

using FluentAssertions;

namespace ODataConnectedService.Tests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.OData.CodeGen.Templates;

    [TestClass]
    public class UniqueIdentifierServiceTests
    {
        private ODataT4CodeGenerator.UniqueIdentifierService uniqueIdentifierService;
        private List<string> existingIdentifiers = new List<string> { "Name" };

        [TestMethod]
        public void GetUniqueIdentifierShouldReadUniqueIdentifier()
        {
            uniqueIdentifierService = new ODataT4CodeGenerator.UniqueIdentifierService(false);
            uniqueIdentifierService.GetUniqueIdentifier("name");
            uniqueIdentifierService.GetUniqueIdentifier("id").Should().Be("id");
        }

        [TestMethod]
        public void GetUniqueIdentifierShouldReadExistingIdentifierWithCaseInsensitive()
        {
            uniqueIdentifierService = new ODataT4CodeGenerator.UniqueIdentifierService(false);
            uniqueIdentifierService.GetUniqueIdentifier("id");
            uniqueIdentifierService.GetUniqueIdentifier("id").Should().Be("id1");
        }

        [TestMethod]
        public void GetUniqueIdentifierShouldReadIndentifiersWithCaseInsensitive()
        {
            uniqueIdentifierService = new ODataT4CodeGenerator.UniqueIdentifierService(false);
            uniqueIdentifierService.GetUniqueIdentifier("id");
            uniqueIdentifierService.GetUniqueIdentifier("ID").Should().Be("ID1");
        }

        [TestMethod]
        public void GetUniqueIdentifierShouldReadMultipleTimesIndentifierWithCaseInsensitive()
        {
            uniqueIdentifierService = new ODataT4CodeGenerator.UniqueIdentifierService(false);
            uniqueIdentifierService.GetUniqueIdentifier("id");
            uniqueIdentifierService.GetUniqueIdentifier("ID");
            uniqueIdentifierService.GetUniqueIdentifier("Id").Should().Be("Id2");
        }

        [TestMethod]
        public void GetUniqueIdentifierShouldReadExistingIdentifierWithCaseSensitive()
        {
            uniqueIdentifierService = new ODataT4CodeGenerator.UniqueIdentifierService(true);
            uniqueIdentifierService.GetUniqueIdentifier("id");
            uniqueIdentifierService.GetUniqueIdentifier("id").Should().Be("id1");
        }

        [TestMethod]
        public void GetUniqueIdentifierShouldReadIdentifierWithCaseSensitive()
        {
            uniqueIdentifierService = new ODataT4CodeGenerator.UniqueIdentifierService(true);
            uniqueIdentifierService.GetUniqueIdentifier("id");
            uniqueIdentifierService.GetUniqueIdentifier("ID").Should().Be("ID");
        }

        [TestMethod]
        public void GetUniqueParameterName()
        {
            uniqueIdentifierService = new ODataT4CodeGenerator.UniqueIdentifierService(true);
            uniqueIdentifierService.GetUniqueParameterName("name").Should().Be("name");
        }

        [TestMethod]
        public void GetUniqueParameterNameShouldReadId()
        {
            uniqueIdentifierService = new ODataT4CodeGenerator.UniqueIdentifierService(true);
            uniqueIdentifierService.GetUniqueParameterName("id").Should().Be("ID");
        }

        [TestMethod]
        public void GetUniqueIdentifierWithExistingIdentifiersAndCaseSensitive()
        {
            uniqueIdentifierService = new ODataT4CodeGenerator.UniqueIdentifierService(existingIdentifiers, true);
            uniqueIdentifierService.GetUniqueIdentifier("name").Should().Be("name");
            uniqueIdentifierService.GetUniqueIdentifier("Name").Should().Be("Name1");
        }

        [TestMethod]
        public void GetUniqueIdentifierWithExistingIdentifiersAndCaseInsensitive()
        {
            uniqueIdentifierService = new ODataT4CodeGenerator.UniqueIdentifierService(existingIdentifiers, false);
            uniqueIdentifierService.GetUniqueIdentifier("name").Should().Be("name1");
            uniqueIdentifierService.GetUniqueIdentifier("Name").Should().Be("Name2");
        }
    }
}
