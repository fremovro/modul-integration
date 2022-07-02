namespace MyISolver
{
    public interface ISolver
    {
        string GetName();
        int[] Solve(int M, int[] weight, int[] cost);
    }
}
