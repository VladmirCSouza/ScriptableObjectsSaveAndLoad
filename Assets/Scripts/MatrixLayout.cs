[System.Serializable]
public class Col
{
    public int[] rows;

    public void SetRows(int height)
    {
        rows = new int[height];
    }
}

[System.Serializable]
public class MatrixLayout
{
    public Col[] cols;

    public void SetDimension(int width, int height)
    {
        cols = new Col[width];
        for (int i = 0; i < cols.Length; i++)
        {
            Col cl = new Col();
            cl.SetRows(height);
            cols[i] = cl;
        }
       
    }
}
