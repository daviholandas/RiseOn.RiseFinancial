#l "./devops/infra/dev_infra.cake"

var target = Argument("target", "Database");

RunTarget(target);