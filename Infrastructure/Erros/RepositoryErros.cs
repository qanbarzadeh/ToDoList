public class RepositoryErros
{
    public static Exception NotFound(int id){
        throw new InfrastructureException($"Task with {id} not found");
    }    
}