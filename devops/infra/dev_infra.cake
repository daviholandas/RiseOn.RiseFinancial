#addin nuget:?package=Cake.Docker&version=1.2.0
///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////
var devComposePath = "./devops/infra/dev-docker-compose.yaml";

var dockerComposeSettings = new DockerComposeUpSettings
{
    Files = new string[] { devComposePath },
    DetachedMode = true,
    ProjectName = "risefinancial"
};

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
   // Executed BEFORE the first task.
   Information("Running tasks...");
});

Teardown(ctx =>
{
   // Executed AFTER the last task.
   Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Database")
.Does(() => {
   Information("Creating SqlServer container...");
   DockerComposeUp(
        dockerComposeSettings,
        new string[]{ "sqlserver" });
   Information("Container created!");
});
