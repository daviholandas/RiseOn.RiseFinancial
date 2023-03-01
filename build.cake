 #addin nuget:?package=Cake.DotNetTool.Module
 #addin "nuget:?package=Cake.Sonar"
 #tool "nuget:?package=MSBuild.SonarQube.Runner.Tool"


#l "./devops/cake/infra.cake"
#l "./devops/cake/tests.cake"
#l "./devops/cake/sonar.cake"
#l "./devops/cake/service.cake"

var target = Argument("target", "RunTests");

RunTarget(target);