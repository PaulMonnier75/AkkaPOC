namespace Core.Models
{
    // Les valeurs les plus basses sont traitées en priorité par
    // la custom mailbox mise en place. 

    public enum CommandPriority
    {
        High,
        Medium,
        Low
    }
}