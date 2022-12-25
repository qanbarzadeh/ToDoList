public class Errors
{
    public static Exception FailedToCreateDuedateTask()
    {
        throw new TodoTaskException("Failed to create duedate task witout a date");
    }

    public static Exception NotFound(int id)
    {
        throw new TodoTaskException($"Task with  Id: {id} not found");
    }
}