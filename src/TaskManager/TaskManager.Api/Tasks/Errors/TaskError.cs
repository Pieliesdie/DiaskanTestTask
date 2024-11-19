namespace TaskManager.Tasks.Errors;

public static class TaskError
{
    public static Error NotFound(Guid id) =>
        Error.NotFound($"Task with Id: {id} not found");

    public static Error Conflict(Guid id) =>
        Error.Conflict($"Configuration with Id: {id} already exists");

    public static Error CreateFailure =>
        Error.Failure("Something went wrong in creating task");

    public static Error UpdateFailure =>
        Error.Failure("Something went wrong in updating task");

    public static Error DeleteFailure =>
        Error.Failure("Something went wrong in deleting task");
}