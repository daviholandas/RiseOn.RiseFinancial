#l "./devops/infra/dev_infra.cake"
#l "./devops/infra/databasemigration.cake"

var target = Argument("target", "Database");

RunTarget(target);