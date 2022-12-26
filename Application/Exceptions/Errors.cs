public class Errors
{
    /// <summary>
    /// Failed to Create Task
    /// </summary>
    /// <returns></returns>
    /// <exception cref="TodoTaskException"></exception>
    public static Exception FailedToCreateDuedateTask()
    {
        throw new TodoTaskException("Failed to create duedate task witout a date");
    }
    /// <summary>
    /// Due date cannot be smaller than today's date
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="TodoTaskException"></exception>
    public static Exception NotFound(int id)
    {
        throw new TodoTaskException($"Task with  Id: {id} not found");
    }

    /// <summary>
    /// Due date cannot be smaller than today's date
    /// </summary>
    /// <returns></returns>
    /// <exception cref="TodoTaskException"></exception>
    public static Exception DueDateException()
    {
        throw new TodoTaskException ("Due date cannot be smaller than today's date");
    }
}