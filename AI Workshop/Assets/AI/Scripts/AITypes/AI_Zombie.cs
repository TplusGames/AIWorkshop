using TPlus.AI;

public class AI_Zombie : AI_Base
{
    public override bool IsHostile(AI_Base ai)
    {
        if (ai is AI_Zombie zombie)
        {
            return false;
        }

        return true;
    }
}
