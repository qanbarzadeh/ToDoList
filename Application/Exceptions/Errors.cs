public class Errors
{
    public static Exception FailedToCreateDuedateTask()
    {
        throw new TodoTaskException("Failed to create duedate task witout a date");
    }
}