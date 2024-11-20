using Xunit;

namespace TaskManager.Api.Tests;

[CollectionDefinition(nameof(ApiDefinition))]
public class ApiDefinition : ICollectionFixture<ServerFixture>;