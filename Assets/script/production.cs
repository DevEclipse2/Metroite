using UnityEngine;

public static class production
{
    public const int metal      = 0;
    public const int oxygen     = 1;
    public const int nitrogen   = 2;
    public const int Hydrogen   = 3;
    public const int explosive  = 4;

    /*
     * Oxygen: Light blue
Nitrogen: white
Iron: gray
Explosive: yellow
Water: Darker blue                                                        
End products:
     */
    public static string GetMat(int mat)
    {
        switch (mat)
        {
            case 0:
                return "Metal";
            break;
            case 1:
                return "Oxygen";
            break;
            case 2:
            return "Nitrogen";
            break;
            case 4:
            return "Blast Charge";
            break;
            case 3:
            return "Hydrogen";
            break;
        }
        return "";
    }
}
